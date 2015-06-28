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

        private void writeWldFile(s7Cpu cpu)
        {
            using (System.IO.FileStream fs = System.IO.File.Create(@"C:\Users\Joe\My Documents\S7PROG.WLD"))
            {
                foreach (s7CpuOB ob in cpu.OB)
                {
                    foreach (byte b in ob.data)
                    {
                        fs.WriteByte(b);
                    }
                }
            }
        }

        private void printCpuBlocks(s7Cpu cpu)
        {
            Console.WriteLine("Print CPU Blocks");
            foreach(s7CpuOB ob in cpu.OB)
            {
                printBlock(ob);
            }
            foreach(s7CpuFC fc in cpu.FC)
            {
                printBlock(fc);
            }
            foreach (s7CpuFB fb in cpu.FB)
            {
                printBlock(fb);
            }
            foreach (s7CpuDB db in cpu.DB)
            {
                printBlock(db);
            }
            foreach (s7CpuSFC sfc in cpu.SFC)
            {
                printBlock(sfc);
            }
            foreach (s7CpuSFB sfb in cpu.SFB)
            {
                printBlock(sfb);
            }
            foreach (s7CpuSDB sdb in cpu.SDB)
            {
                printBlock(sdb);
            }
        }

        private void printBlock(s7CpuBlock block)
        {
            Console.WriteLine(block.blockType + block.blockNumber.ToString());
            Console.WriteLine("Name:\t" + block.name);
            Console.WriteLine("Family:\t" + block.family);
            Console.WriteLine("Author:\t" + block.author);
            Console.WriteLine("Load Size:\t" + block.loadSize);
        }

        private void connectCpu()
        {
            //TODO: Remove this
            int result = 0;

            MyClient = new S7Client();
            MyClient.ConnectTo(txtIpAddress.Text, 0, 2);

            S7Client.S7OrderCode oc = new S7Client.S7OrderCode();
            MyClient.GetOrderCode(ref oc);
            
            S7Client.S7CpuInfo ci = new S7Client.S7CpuInfo();
            MyClient.GetCpuInfo(ref ci);
            
            S7Client.S7BlocksList bl = new S7Client.S7BlocksList();
            MyClient.ListBlocks(ref bl);

            foreach (s7BlockType blockType in Enum.GetValues(typeof(s7BlockType)))
            {
                ushort[] blockList = new ushort[0x2000];
                int blockCount = blockList.Length;
                MyClient.ListBlocksOfType((int)blockType, blockList, ref blockCount);
                Console.WriteLine("Found " + blockCount + " blocks of type " + blockType); 
                for (int i = 0; i < blockCount; i++)
                {
                    S7Client.S7BlockInfo blockInfo = new S7Client.S7BlockInfo();
                    MyClient.GetAgBlockInfo((int)blockType, blockList[i], ref blockInfo);

                    byte[] buffer = new byte[4096];
                    int bufferSize = buffer.Length;

                    MyClient.FullUpload((int)blockType, blockList[i], buffer, ref bufferSize);

                    byte[] data = new byte[bufferSize];
                    Array.Copy(buffer, data, data.Length);
                  
                    MyCpu.addCpuBlock(blockInfo, data);
                }
            }

            // Successfully connected to CPU
            if (result == 0)
            {
                btnDisconnect.Enabled = true;
                btnConnect.Enabled = false;
                MyCpu.orderCode = new s7OrderCode(oc);
                MyCpu.cpuInfo = new s7CpuInfo(ci);
                txtConsole.AppendText("CPU Order Code:\t" + MyCpu.orderCode.code);
                txtConsole.AppendText(System.Environment.NewLine);
                txtConsole.AppendText("CPU Serial Number:\t" + MyCpu.cpuInfo.serialNumber);
                txtConsole.AppendText(System.Environment.NewLine);
                printCpuBlocks(MyCpu);
                writeWldFile(MyCpu);
            }

            //Error Connecting to CPU
            else
            {
                txtConsole.AppendText("Error Code:\t" + result.ToString());
                txtConsole.AppendText(System.Environment.NewLine);
            }
          
        }

        private void disconnectCpu()
        {
            MyClient.Disconnect();
            MyClient = null;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectCpu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            disconnectCpu();
        }

    }
}
