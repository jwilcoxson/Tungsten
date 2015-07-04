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

        private s7Cpu MyCpu;

        public S7Backup()
        {
            InitializeComponent();
            MyCpu = new s7Cpu();

        }

        private void saveWldFile(s7Cpu cpu)
        {
            wldFile w = new wldFile(cpu);
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
                    fs.Write(w.data, 0, w.data.Length);
                    fs.Close();
                }
            }

        }

        private void connectCpu()
        {
            MyCpu.connect(txtIpAddress.Text);
            printCpuInfo(MyCpu);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectCpu();
        }

        private void printCpuInfo(s7Cpu cpu)
        {
            List<string> s = new List<string>();
            s.Add(cpu.orderCode);
            s.Add("CPU Serial Number:\t" + cpu.serialNumber);
            s.Add("OB Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.OB));
            s.Add("FC Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.FC));
            s.Add("FB Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.FB));
            s.Add("DB Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.DB));
            s.Add("SFC Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.SFC));
            s.Add("SFB Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.SFB));
            s.Add("SDB Count:\t" + MyCpu.blocks.Count(x => x.blockType == s7BlockType.SDB));
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

        private void saveVs7(s7Cpu cpu)
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
        
        private s7Cpu openVs7()
        {
            s7Cpu cpu;
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(s7Cpu));

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "VS7 files (*.VS7)|*.VS7|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.VS7";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(dialog.OpenFile());
                cpu = (s7Cpu)reader.Deserialize(file);
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
            MyCpu = new s7Cpu();
            txtCpuInfo.Text = "";
        }

    }
}
