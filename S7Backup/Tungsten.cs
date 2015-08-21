using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Snap7;

namespace Tungsten
{
    public partial class Tungsten : Form
    {

        private wCpu MyCpu;
        private bool plcConnected = false;
        private string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\"
                                        + About.AssemblyCompany + @"\" + About.AssemblyProduct + @"\";

        /// <summary>
        /// Main Constructor for the Tungsten Form Class
        /// </summary>
        public Tungsten()
        {
            InitializeComponent();
            
            if (!System.IO.Directory.Exists(appDataPath))
            {
                System.IO.Directory.CreateDirectory(appDataPath);
            }

            MyCpu = new wCpu();
            loadPlcBookmarks();
        }

        private void enableControls()
        {
            btnConnect.Text = "Disconnect";
            grpPlcInformation.Enabled = true;
            copyRAMToROMToolStripMenuItem.Enabled = true;
            compressMemoryToolStripMenuItem.Enabled = true;
            eraseMemoryToolStripMenuItem.Enabled = true;
            downloadToPlcToolStripMenuItem.Enabled = true;
            saveToDiskToolStripMenuItem.Enabled = true;
            runToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;
            viewDiagnosticBufferToolStripMenuItem.Enabled = true;
        }

        private void disableControls()
        {
            btnConnect.Text = "Connect";
            grpPlcInformation.Enabled = false;
            copyRAMToROMToolStripMenuItem.Enabled = false;
            compressMemoryToolStripMenuItem.Enabled = false;
            eraseMemoryToolStripMenuItem.Enabled = false;
            downloadToPlcToolStripMenuItem.Enabled = false;
            saveToDiskToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = false;
            viewDiagnosticBufferToolStripMenuItem.Enabled = false;
        }

        private void printCpuInfo(wCpu cpu)
        {
            List<string> s = new List<string>();
            s.Add(cpu.orderCode);
            s.Add("CPU Serial Number:\t" + cpu.serialNumber);
            s.Add("OB Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.OB));
            s.Add("FC Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.FC));
            s.Add("FB Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.FB));
            s.Add("DB Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.DB));
            s.Add("SFC Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.SFC));
            s.Add("SFB Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.SFB));
            s.Add("SDB Count:\t" + MyCpu.blocks.Count(x => x.blockType == wBlockType.SDB));
            foreach (string i in s)
            {
                //txtCpuInfo.AppendText(i + System.Environment.NewLine);
            }
        }

        private void showErrorForException(wPlcException ex)
        {
            int clientErrorCode = (ex.errorCode >> 20) & 0xFFF;
            string clientError = String.Empty;
            if (clientErrorCode != 0)
                clientError = clientErrorCodes[clientErrorCode] + System.Environment.NewLine;

            int isoErrorCode = (ex.errorCode >> 16) & 0xF;
            string isoError = String.Empty;
            if (isoErrorCode != 0)
                isoError = isoTcpErrorCodes[isoErrorCode] + System.Environment.NewLine;

            int tcpErrorCode = (ex.errorCode) & 0xFFFF;
            string tcpError = String.Empty;
            System.Net.Sockets.SocketError se;
            if (tcpErrorCode != 0)
            {
                se = (System.Net.Sockets.SocketError)tcpErrorCode;
                tcpError = se.ToString() + System.Environment.NewLine;
            }

            string error = ex.Message + System.Environment.NewLine;
            error = error + clientError + isoError + tcpError;

            Console.WriteLine(error + ex.errorCode.ToString("X8") + System.Environment.NewLine);

            //TODO Center this dialog in parent
            MessageBox.Show(error);
        }

        /*
         * File Methods
         */

        private void saveWld(wCpu cpu)
        {
            wldFile w = new wldFile(cpu);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "WLD files (*.wld)|*.wld|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.wld";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fs;
                if ((fs = dialog.OpenFile()) != null)
                {
                    fs.Write(w.data, 0, w.data.Length);
                    fs.Close();
                }
            }

        }

        //TODO Implement openWldFile Method
        private wCpu openWld()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "WLD files (*.WLD)|*.WLD|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.wld";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fs;
                fs = dialog.OpenFile();

                int b = fs.ReadByte();

                List<byte> bytes = new List<byte>();

                while (b != -1)
                {
                    bytes.Add((byte)b);
                    b = fs.ReadByte();
                }

                decodeWld(bytes);
            }
            return new wCpu();
        }

        private void decodeWld(List<byte> bytes)
        {
            const int BLOCK_TYPE_OFFSET = 5;
            const int BLOCK_NUMBER_OFFSET_HIGH = 6;
            const int BLOCK_NUMBER_OFFSET_LOW = 7;
            const int BLOCK_LENGTH_OFFSET_HIGH = 10;
            const int BLOCK_LENGTH_OFFSET_LOW = 11;

            int currentOffset = 0;

            while (currentOffset < bytes.Count)
            {
                wSubBlockType blockType = (wSubBlockType)bytes[currentOffset + BLOCK_TYPE_OFFSET];
                int blockNumber = 256 * bytes[currentOffset + BLOCK_NUMBER_OFFSET_HIGH] +
                                    bytes[currentOffset + BLOCK_NUMBER_OFFSET_LOW];
                int blockLength = 256 * bytes[currentOffset + BLOCK_LENGTH_OFFSET_HIGH] +
                                    bytes[currentOffset + BLOCK_LENGTH_OFFSET_LOW];
               

                if (blockType == wSubBlockType.OB)
                {
                    try
                    {
                        MyCpu.downloadBlock(bytes.GetRange(currentOffset, blockLength));
                    }
                    catch (wPlcException ex)
                    {
                        showErrorForException(ex);
                    }
                   
                }

                Console.WriteLine("Found " + blockType + blockNumber + " with length " + blockLength);
                currentOffset += blockLength;
            }
        }

        private void saveVs7(wCpu cpu)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(wCpu));
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "VS7 files (*.VS7)|*.VS7|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.VS7";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(dialog.OpenFile());
                writer.Serialize(file, MyCpu);
                file.Close();
            }
        }

        private wCpu openVs7()
        {
            wCpu cpu;
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(wCpu));

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "VS7 files (*.VS7)|*.VS7|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.VS7";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(dialog.OpenFile());
                cpu = (wCpu)reader.Deserialize(file);
                file.Close();
                return cpu;
            }
            else
            {
                return null;
            }
        }

        /*
         * Menu Strip Event Methods
         */

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyCpu = new wCpu();
            //txtCpuInfo.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyCpu = openVs7();
            printCpuInfo(MyCpu);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveVs7(MyCpu);
        }

        private void saveToDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveWld(MyCpu);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutTungstenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        /*
         * Button Event Methods
         */

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (plcConnected == false && cmbPlc.SelectedIndex >= 0)
            {
                try
                {
                    MyCpu.connect(plcListing[cmbPlc.SelectedIndex].ipAddress);
                    wCpuRunMode rm = MyCpu.getCpuRunMode();
                    plcConnected = true;
                    progressBar.Visible = true;
                    Thread uploadThread = new Thread(new ThreadStart(MyCpu.upload));
                    uploadThread.Start();
                    //TODO: The UI is being blocked here and the progress bar wont animate :(
                    uploadThread.Join();
                    enableControls();
                    lblModel.Text = ("Model\n" + MyCpu.orderCode);
                    lblSerialNumber.Text = ("Serial Number\n" + MyCpu.serialNumber);
                    lblModuleTypeName.Text = ("Module Type Name\n" + MyCpu.moduleTypeName);
                    lblModuleName.Text = ("Module Name\n" + MyCpu.moduleName);
                    populateBlockList(MyCpu);

                    if (rm == wCpuRunMode.Run)
                    {
                        runToolStripMenuItem.Enabled = false;
                    }
                    else if (rm == wCpuRunMode.Stop)
                    {
                        stopToolStripMenuItem.Enabled = false;
                    }

                }
                catch (wPlcException ex)
                {
                    showErrorForException(ex);
                    if (plcConnected == true)
                        MyCpu.disconnect();
                    disableControls();
                }
                finally
                {
                    progressBar.Visible = false;
                }
            }
            else
            {
                disableControls();
                if (plcConnected == true)
                    MyCpu.disconnect();
                plcConnected = false;
                reset();
            }

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.setCpuRunMode(wCpuRunMode.Run);
                stopToolStripMenuItem.Enabled = true;
                runToolStripMenuItem.Enabled = false;
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.setCpuRunMode(wCpuRunMode.Stop);
                runToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }

        }

        private void reset()
        {
            this.lblModel.Text = "Model";
            this.lblModuleName.Text = "Module Name";
            this.lblModuleTypeName.Text = "Module Type Name";
            this.lblSerialNumber.Text = "Serial Number";
            this.lstBlockList.Items.Clear();
            MyCpu = new wCpu();
        }

        /*
         * Error Codes Dictionaries
         */

        private static Dictionary<int, string> isoTcpErrorCodes = new Dictionary<int, string>
        {
            {0x01, "ISO Connection Error"},
            {0x02, "ISO Disconnection Error"},
            {0x03, "Malformed PDU supplied"},
            {0x04, "Bad data size supplied to Send/Recieve function"},
            {0x05, "Null pointer supplied"},
            {0x06, "Short packet recieved"},
            {0x07, "Too many packets without EoT flag"},
            {0x08, "The sum of fragmented data exceeds the maximum packet size"},
            {0x09, "A send error occured"},
            {0x0A, "A recieve error occured"},
            {0x0B, "Invalid TSAP parameters supplied"},
            {0x0C, "Reserved"},
            {0x0D, "Reserved"},
            {0x0E, "Reserved"},
            {0x0F, "Reserved"}
        };

        private static Dictionary<int, string> clientErrorCodes = new Dictionary<int, string>
        {
            {0x01, "Error during PDU negogiation"},
            {0x02, "Invalid parameter supplied to the current function"},
            {0x03, "A job is pending, there is an asynchronous function in progress"},
            {0x04, "More than 20 items were passed to a multi read/write function"},
            {0x05, "Invalid word length parameter supplied to the current function"},
            {0x06, "Partial data was written, the target area is smaller than the data size supplied"},
            {0x07, "A multi read/write functions has a data size that exceeds the PDU size"},
            {0x08, "Invalid answer from the PLC"},
            {0x09, "An out of range address was specified"},
            {0x0A, "Invalid transport size parameter supplied to the current function"},
            {0x0B, "Invalid data size parameter supplied to the current fucntion"},
            {0x0C, "Item requested was not found in the PLC"},
            {0x0D, "Invalid value supplied to the current function"},
            {0x0E, "PLC cannot be started"},
            {0x0F, "PLC is already in Run"},
            {0x10, "PLC cannot be stopped"},
            {0x11, "Cannot copy RAM to ROM. The PLC is running or doesn't support this function"},
            {0x12, "Cannot compress memory. The PLC is running or doesn't support this function"},
            {0x13, "PLC is already in Stop"},
            {0x14, "Function not available"},
            {0x15, "Block upload sequence failed"},
            {0x16, "Invalid data size recieved from the PLC"},
            {0x17, "Invalid block type suppplied to the current function"},
            {0x18, "Invalid block supplied to the current function"},
            {0x19, "Invalid block size supplied to the current function"},
            {0x1A, "Block download sequence failed"},
            {0x1B, "Block Insert command refused"},
            {0x1C, "Block Delete command refused"},
            {0x1D, "This operation is password protected"},
            {0x1E, "Invalid password supplied"},
            {0x1F, "There is no password to set or clear"},
            {0x20, "Job timeout"},
            {0x21, "Partial data was read, the souce areas is greater than the data size supplied"},
            {0x22, "The buffer supplied is too small"},
            {0x23, "Function refused by PLC"},
            {0x24, "Invalid parameter value supplied to Get/Set parameter"},
            {0x25, "Cannot perform. The client is destroying"},
            {0x26, "Cannot change parameter while connected"}
        };

        private List<plcBookmark> plcListing = new List<plcBookmark>();

        private void cmbPlc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlc.SelectedIndex == cmbPlc.Items.Count -1)
            {
                AddPlc addPlc = new AddPlc();
                DialogResult dr = addPlc.ShowDialog();

                string title = addPlc.ipAddress;
                if (addPlc.bookmarkName != "")
                {
                    title = addPlc.bookmarkName + " (" + title + ")";
                }
                cmbPlc.Items.Insert(cmbPlc.Items.Count - 1, title);
                cmbPlc.SelectedIndex = cmbPlc.Items.Count - 2;
                plcBookmark bp = new plcBookmark(cmbPlc.SelectedIndex, addPlc.bookmarkName, addPlc.ipAddress);
                plcListing.Add(bp);
                savePlcBookmarks();
            }
        }

        private void populateBlockList(wCpu cpu)
        {
            foreach(wCpuBlock b in cpu.blocks)
            {
                ListViewItem li = new ListViewItem();
                li.Text = b.ToString();
                li.SubItems.Add(b.name);
                li.SubItems.Add(b.author);
                li.SubItems.Add(b.loadSize.ToString());
                li.SubItems.Add(b.codeDate);
                li.SubItems.Add(b.interfaceDate);
                li.Group = lstBlockList.Groups[b.blockType.ToString().ToLower()];
                lstBlockList.Items.Add(li);
                
            }
        }

        private void blockList_ItemActivate(object sender, EventArgs e)
        {
            string blockName = lstBlockList.SelectedItems[0].Text;
            wCpuBlock block = MyCpu.blocks.Find(b => b.ToString() == blockName);
            BlockInfo bi = new BlockInfo(block);
            bi.Show();
        }

        private void copyRAMToROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.copyRamToRom();
                MessageBox.Show("RAM successfully copied to ROM");
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
            }
        }

        private void compressMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int memoryOld = 0;
                foreach (wCpuBlock b in MyCpu.blocks)
                {
                    if (b.blockType != wBlockType.SFC && b.blockType != wBlockType.SFB)
                    {
                        memoryOld += b.loadSize;
                    }
                }
                
                MyCpu.compressMemory();

                int memoryNew = 0;
                foreach (wCpuBlock b in MyCpu.blocks)
                {
                    if (b.blockType != wBlockType.SFC && b.blockType != wBlockType.SFB)
                    {
                        memoryNew += b.loadSize;
                    }
                }

                int bytesSaved = memoryOld - memoryNew;
                string message;

                if (bytesSaved > 0)
                {
                    message = string.Format("PLC Load Memory was compressed. Saved {0} bytes.", memoryOld - memoryNew);
                }
                else
                {
                    message = "PLC Load Memory is already fully compressed";
                }

                MessageBox.Show(message);
                
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
            }
        }

        private void savePlcBookmarks()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<plcBookmark>));

            System.IO.StreamWriter file = new System.IO.StreamWriter(appDataPath + "bookmarks.config");
            SaveFileDialog dialog = new SaveFileDialog();
            writer.Serialize(file, plcListing);
            file.Close();
        }

        private void loadPlcBookmarks()
        {
            List<plcBookmark> list = new List<plcBookmark>();
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<plcBookmark>));
            
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(appDataPath + "bookmarks.config");

                try
                {
                    list = (List<plcBookmark>)reader.Deserialize(file);
                    file.Close();
                    plcListing = list;
                    foreach (plcBookmark b in plcListing)
                    {
                        string title = b.ipAddress;
                        if (b.bookmarkName != "")
                        {
                            title = b.bookmarkName + " (" + title + ")";
                        }
                        cmbPlc.Items.Insert(b.index, title);
                    }
                }
                catch
                {
                    file.Close();
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {

            }
        }

        private void eraseMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.erase();
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }
        }

        private void downloadToPLCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "Erase PLC?";
            string message = "Would you like to erase the existing PLC program before download?";
            DialogResult dr = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            bool eraseCpu = false;
            if (dr == DialogResult.Yes)
            {
                eraseCpu = true;
            }

            //Todo: fix this broken download method
            //MyCpu.download(plcListing[cmbPlc.SelectedIndex].ipAddress);
        }

        private void downloadToPlcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openWld();
        }

    }

    [Serializable]
    public class plcBookmark
    {
        public plcBookmark() { }
        public plcBookmark(int index, string bookmarkName, string ipAddress)
        {
            this.index = index;
            this.bookmarkName = bookmarkName;
            this.ipAddress = ipAddress;
        }
        public int index;
        public string bookmarkName;
        public string ipAddress;
    }

}
