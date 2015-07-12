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

        public Tungsten()
        {
            InitializeComponent();
            MyCpu = new wCpu();
        }

        private void saveWldFile(wCpu cpu)
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

        private void button1_Click(object sender, EventArgs e)
        {
            MyCpu.upload();
            printCpuInfo(MyCpu);
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
                txtCpuInfo.AppendText(i + System.Environment.NewLine);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveWldFile(MyCpu);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveVs7(MyCpu);
        }

        private void S7Backup_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyCpu = openVs7();
            printCpuInfo(MyCpu);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyCpu = new wCpu();
            txtCpuInfo.Text = "";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            MyCpu.download(txtIpAddress.Text);
        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            MyCpu.erase();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            MyCpu.connect(txtIpAddress.Text);
        }

        private void aboutTungstenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void startCpu_Click(object sender, EventArgs e)
        {
            MyCpu.setCpuRunMode(wCpuRunMode.Run);
        }

        private void stopCpu_Click(object sender, EventArgs e)
        {
            MyCpu.setCpuRunMode(wCpuRunMode.Stop);
        }

        private void getRunMode_Click(object sender, EventArgs e)
        {
            MyCpu.getCpuRunMode();
        }

    }
}
