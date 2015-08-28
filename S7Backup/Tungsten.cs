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
                clientError = wDictionary.clientErrorCodes[clientErrorCode] + System.Environment.NewLine;

            int isoErrorCode = (ex.errorCode >> 16) & 0xF;
            string isoError = String.Empty;
            if (isoErrorCode != 0)
                isoError = wDictionary.isoTcpErrorCodes[isoErrorCode] + System.Environment.NewLine;

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

        private void refreshCpuInformation()
        {
            wCpuRunMode rm = MyCpu.getCpuRunMode();
            lblModel.Text = ("Model\n" + MyCpu.orderCode);
            lblSerialNumber.Text = ("Serial Number\n" + MyCpu.serialNumber);
            lblModuleTypeName.Text = ("Module Type Name\n" + MyCpu.moduleTypeName);
            lblModuleName.Text = ("Module Name\n" + MyCpu.moduleName);
            
            populateBlockList(MyCpu);

            if (rm == wCpuRunMode.Run)
            {
                runToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
                lblPlcMode.Text = "PLC Mode: Run";
            }
            else if (rm == wCpuRunMode.Stop)
            {
                runToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
                lblPlcMode.Text = "PLC Mode: Stop";
            }
            else
            {
                runToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = true;
                lblPlcMode.Text = "PLC Mode: Unknown";
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (plcConnected == false && cmbPlc.SelectedIndex >= 0)
            {
                try
                {
                    MyCpu.connect(plcListing[cmbPlc.SelectedIndex].ipAddress);         
                    plcConnected = true;
                    MyCpu.upload();
                    refreshCpuInformation();
                    enableControls();
                }
                catch (wPlcException ex)
                {
                    showErrorForException(ex);
                    if (plcConnected == true)
                        MyCpu.disconnect();
                    disableControls();
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

        private void reset()
        {
            lblModel.Text = "Model";
            lblModuleName.Text = "Module Name";
            lblModuleTypeName.Text = "Module Type Name";
            lblSerialNumber.Text = "Serial Number";
            lblPlcMode.Text = "PLC Mode:";
            lstBlockList.Items.Clear();
        }

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
                wPlcBookmark bp = new wPlcBookmark(cmbPlc.SelectedIndex, addPlc.bookmarkName, addPlc.ipAddress);
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
            MyCpu = vs7File.open();
            printCpuInfo(MyCpu);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vs7File.save(MyCpu);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void eraseMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to erase the PLC program?", "Erase PLC?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    MyCpu.erase();
                    MessageBox.Show("PLC program has been erased");
                    reset();
                    MyCpu.upload();
                    refreshCpuInformation();
                }
                catch (wPlcException ex)
                {
                    showErrorForException(ex);
                    MyCpu.disconnect();
                    disableControls();
                }
                
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.setCpuRunMode(wCpuRunMode.Run);
                refreshCpuInformation();
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
                refreshCpuInformation();
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }

        }

        private void downloadToPlcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.erase();
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
            }
            
            wldFile w = new wldFile();
            bool result = w.openFromFile();
            
            if (result == true)
            {
                List<Tuple<wSubBlockType, int, List<byte>>> blockList = w.decode();
                try
                {
                    foreach (Tuple<wSubBlockType, int, List<byte>> b in blockList)
                    {
                        MyCpu.downloadBlock(b.Item3);
                    }
                }
                catch (wPlcException ex)
                {
                    showErrorForException(ex);
                }
                MessageBox.Show("Program sucessfullly downloaded to PLC");
            }
            else
            {
                MessageBox.Show("Error opening WLD file");
            }
            refreshCpuInformation();
        }

        private void saveToDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wldFile w = new wldFile(MyCpu);
            w.save();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tungsten-app.xyz/document/manual/");
        }

        private void aboutTungstenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private List<wPlcBookmark> plcListing = new List<wPlcBookmark>();

        private void savePlcBookmarks()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<wPlcBookmark>));

            System.IO.StreamWriter file = new System.IO.StreamWriter(appDataPath + "bookmarks.config");
            SaveFileDialog dialog = new SaveFileDialog();
            writer.Serialize(file, plcListing);
            file.Close();
        }

        private void loadPlcBookmarks()
        {
            List<wPlcBookmark> list = new List<wPlcBookmark>();
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<wPlcBookmark>));

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(appDataPath + "bookmarks.config");

                try
                {
                    list = (List<wPlcBookmark>)reader.Deserialize(file);
                    file.Close();
                    plcListing = list;
                    foreach (wPlcBookmark b in plcListing)
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

    }

}
