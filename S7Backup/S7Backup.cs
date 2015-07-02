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

namespace S7Backup
{
    public partial class S7Backup : Form
    {
        Dictionary<int, string> blockTypeNames = new Dictionary<int, string>() {
            {(int)s7BlockType.OB, "OB"},
            {(int)s7BlockType.FC, "FC"},
            {(int)s7BlockType.FB, "FB"},
            {(int)s7BlockType.DB, "DB"},
            {(int)s7BlockType.SFC, "SFC"},
            {(int)s7BlockType.SFB, "SFB"},
            {(int)s7BlockType.SDB, "SDB"}
        };

        static S7Client MyClient;
        static s7Cpu MyCpu;

        public S7Backup()
        {
            InitializeComponent();
            MyCpu = new s7Cpu();

        }

        private void saveWldFile(s7Cpu cpu)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "WLD files (*.WLD)|*.WLD|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.WLD";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fs;
                if ((fs = dialog.OpenFile()) != null)
                {
                    foreach (s7CpuBlock block in cpu.blocks)
                    {
                        if (block.data != null)
                            fs.Write(block.data, 0, block.data.Length);
                    }
                    fs.Close();
                }
            }

        }
        private void writeLog (string text)
        {
            txtConsole.AppendText(text);
            txtConsole.AppendText(System.Environment.NewLine);     
        }
        private void printCpuBlocks(s7Cpu cpu)
        {
            foreach(s7CpuBlock block in cpu.blocks)
            {
                printBlock(block);
            }
        }

        private void printBlock(s7CpuBlock block)
        {
            writeLog(block.blockType + block.blockNumber.ToString());
            writeLog("Name:\t" + block.name);
            writeLog("Family:\t" + block.family);
            writeLog("Author:\t" + block.author);
            writeLog("Load Size:\t" + block.loadSize);
        }

        private void connectCpu()
        {
            btnDisconnect.Enabled = true;
            btnConnect.Enabled = false;

            MyClient = new S7Client();
            
            int connectResult = MyClient.ConnectTo(txtIpAddress.Text, 0, 2);

            if (connectResult == 0)
            {
                writeLog("Connected to CPU at IP Address " + txtIpAddress.Text);
                S7Client.S7OrderCode oc = new S7Client.S7OrderCode();
                int orderCodeResult = MyClient.GetOrderCode(ref oc);

                if (orderCodeResult == 0)
                {
                    MyCpu.setOrderCode(oc);
                    writeLog("CPU Order Code:\t" + MyCpu.orderCode);


                    S7Client.S7CpuInfo ci = new S7Client.S7CpuInfo();
                    int cpuInfoResult = MyClient.GetCpuInfo(ref ci);

                    if (cpuInfoResult == 0)
                    {

                        MyCpu.setCpuInfo(ci);
                        writeLog("CPU Serial Number:\t" + MyCpu.serialNumber);

                        S7Client.S7BlocksList bl = new S7Client.S7BlocksList();
                        int listBlocksResult = MyClient.ListBlocks(ref bl);

                        if (listBlocksResult == 0)
                        {
                            writeLog("OB Count:\t" + bl.OBCount);
                            writeLog("FC Count:\t" + bl.FCCount);
                            writeLog("FB Count:\t" + bl.FBCount);
                            writeLog("DB Count:\t" + bl.DBCount);
                            writeLog("SFC Count:\t" + bl.SFCCount);
                            writeLog("SFB Count:\t" + bl.SFBCount);
                            writeLog("SDB Count:\t" + bl.SDBCount);

                        }
                        else //Failed to List Blocks
                        {
                            writeLog("Failed to list blocks. " + listBlocksResult.ToString("X4"));
                        }
                    }
                    else //Failed to get CPU Info
                    {
                        writeLog("Failed to get CPU info. " + cpuInfoResult.ToString("X4"));
                    }
                }
                else //Failed to get Order Code
                {
                    writeLog("Failed to get Order Code. " + orderCodeResult.ToString("X4"));
                }
            }
            else //Failed to connect to CPU
            {
                writeLog("Failed to connect to CPU. " + connectResult.ToString("X4"));
            }
        }

        private void disconnectCpu()
        {
            MyClient.Disconnect();
            MyClient = null;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void uploadCpu()
        {
            foreach (s7BlockType blockType in Enum.GetValues(typeof(s7BlockType)))
            {
                ushort[] blockList = new ushort[0x2000];
                int blockCount = blockList.Length;
                MyClient.ListBlocksOfType((int)blockType, blockList, ref blockCount);
                for (int i = 0; i < blockCount; i++)
                {
                    S7Client.S7BlockInfo blockInfo = new S7Client.S7BlockInfo();
                    MyClient.GetAgBlockInfo((int)blockType, blockList[i], ref blockInfo);

                    byte[] buffer = new byte[4096];
                    int bufferSize = buffer.Length;

                    if (blockType != s7BlockType.SFC && blockType != s7BlockType.SFB)
                        MyClient.FullUpload((int)blockType, blockList[i], buffer, ref bufferSize);
                    else
                        bufferSize = 0;

                    byte[] data = new byte[bufferSize];
                    Array.Copy(buffer, data, data.Length);

                    MyCpu.addCpuBlock(blockInfo, data);
                    TreeNode node = new TreeNode(blockType + blockInfo.BlkNumber.ToString());
                    TreeNode[] result = treeView1.Nodes[0].Nodes[1].Nodes.Find("nde" + blockType, false);
                    result[0].Nodes.Add(node);
                }
            }

            printCpuBlocks(MyCpu);
            treeView1.ExpandAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectCpu();
            uploadCpu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            disconnectCpu();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveWldFile(MyCpu);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyClient.Connected())
                disconnectCpu();
            Application.Exit();
        }

        private void block_Click(object sender, EventArgs e)
        {
            BlockInfo bi = new BlockInfo(MyCpu.blocks[0]);
            bi.Show();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (treeView1.SelectedNode.Parent.Parent.Name == "ndeBlocks")
                {
                    BlockInfo bi = new BlockInfo(MyCpu.blocks.Find(x => (x.blockType.ToString() + x.blockNumber.ToString()) ==
                        treeView1.SelectedNode.Text));
                    bi.Show();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(s7Cpu));
            
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


    }
}
