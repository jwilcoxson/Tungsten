using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snap7;

namespace Tungsten
{
    public partial class Tungsten : Form
    {

        private wCpu MyCpu;
        private bool plcConnected = false;

        /// <summary>
        /// Main Constructor for the Tungsten Form Class
        /// </summary>
        public Tungsten()
        {
            InitializeComponent();
            MyCpu = new wCpu();
        }

        private void enableControls()
        {
            btnConnect.Text = "Disconnect";
            btnErase.Enabled = true;
            btnDownload.Enabled = true;
            btnStartPlc.Enabled = true;
            btnStopPlc.Enabled = true;
            btnGetRunMode.Enabled = true;
            grpPlcInformation.Enabled = true;
        }

        private void disableControls()
        {
            btnConnect.Text = "Connect";
            btnErase.Enabled = false;
            btnDownload.Enabled = false;
            btnStartPlc.Enabled = false;
            btnStopPlc.Enabled = false;
            btnGetRunMode.Enabled = false;
            grpPlcInformation.Enabled = false;
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
            return new wCpu();
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MyCpu.connect(plcListing[cmbPlc.SelectedIndex]);
                    wCpuRunMode rm = MyCpu.getCpuRunMode();
                    plcConnected = true;
                    MyCpu.upload();
                    enableControls();
                    populateBlockList(MyCpu);
                    
                    if (rm == wCpuRunMode.Run)
                    {
                        btnStartPlc.Enabled = false;
                    }
                    else if (rm == wCpuRunMode.Stop)
                    {
                        btnStopPlc.Enabled = false;
                    }

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
            }

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }
        }

        private void btnErase_Click(object sender, EventArgs e)
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

        private void btnStartCpu_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.setCpuRunMode(wCpuRunMode.Run);
                btnStopPlc.Enabled = true;
                btnStartPlc.Enabled = false;
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }

        }

        private void btnStopCpu_Click(object sender, EventArgs e)
        {
            try
            {
                MyCpu.setCpuRunMode(wCpuRunMode.Stop);
                btnStartPlc.Enabled = true;
                btnStopPlc.Enabled = false;
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }

        }

        private void btnGetRunMode_Click(object sender, EventArgs e)
        {
            try
            {
                wCpuRunMode rm = MyCpu.getCpuRunMode();
                if (rm == wCpuRunMode.Run)
                {
                    btnStartPlc.Enabled = false;
                }
                else if (rm == wCpuRunMode.Stop)
                {
                    btnStopPlc.Enabled = false;
                }
            }
            catch (wPlcException ex)
            {
                showErrorForException(ex);
                MyCpu.disconnect();
                disableControls();
            }

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

        private Dictionary<int, string> plcListing = new Dictionary<int,string>();

        private void cmbPlc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlc.SelectedIndex == cmbPlc.Items.Count -1)
            {
                AddPlc addPlc = new AddPlc();
                DialogResult dr = addPlc.ShowDialog();
                cmbPlc.Items.Insert(cmbPlc.Items.Count - 1, addPlc.bookmarkName + " - " + addPlc.ipAddress);
                cmbPlc.SelectedIndex = cmbPlc.Items.Count - 2;

                //TODO Adding and removing from this dictionary should really be done as an event on the Combo Box 
                plcListing.Add(cmbPlc.Items.Count - 2, addPlc.ipAddress);
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
                li.Group = blockList.Groups[b.blockType.ToString().ToLower()];

                blockList.Items.Add(li);
                
            }
        }
    }
}
