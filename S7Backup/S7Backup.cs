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
        static S7Client MyClient;
        static s7Cpu MyCpu;
        bool connected = false;

        public S7Backup()
        {
            InitializeComponent();
            MyCpu = new s7Cpu();

        }

        private void connectCpu()
        {
            MyClient = new S7Client();
            MyClient.ConnectTo(txtIpAddress.Text, 0, 2);

            S7Client.S7OrderCode oc = new S7Client.S7OrderCode();
            S7Client.S7CpuInfo ci = new S7Client.S7CpuInfo();
            S7Client.S7BlocksList bl = new S7Client.S7BlocksList();
            ushort[] OBList = new ushort[0x1000];
            int itemsCount = OBList.Length;

            int result = MyClient.GetOrderCode(ref oc);
            MyClient.GetCpuInfo(ref ci);
            MyClient.ListBlocks(ref bl);
            MyClient.ListBlocksOfType(S7Client.Block_OB, OBList, ref itemsCount);




            // Successfully connected to CPU
            if (result == 0)
            {
                connected = true;
                btnDisconnect.Enabled = true;
                btnConnect.Enabled = false;
                MyCpu.orderCode = new s7OrderCode(oc);
                MyCpu.cpuInfo = new s7CpuInfo(ci);
                txtConsole.AppendText("CPU Order Code:\t" + MyCpu.orderCode.code);
                txtConsole.AppendText(System.Environment.NewLine);
                txtConsole.AppendText("CPU Serial Number:\t" + MyCpu.cpuInfo.serialNumber);
                txtConsole.AppendText(System.Environment.NewLine);
                txtConsole.AppendText("OB Count:\t" + itemsCount);
                txtConsole.AppendText(System.Environment.NewLine);

                for (int i = 0; i < itemsCount; i++)
                {

                    txtConsole.AppendText("OB" + OBList[i]);
                    txtConsole.AppendText(System.Environment.NewLine);
                }
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
