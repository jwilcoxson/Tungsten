using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Tungsten
{
    public partial class BlockInfo : Form
    {
        public BlockInfo(wCpuBlock block)
        {
            InitializeComponent();

            txtBlock.Text = block.ToString();
            txtName.Text = block.name;
            txtAuthor.Text = block.author;
            txtFamily.Text = block.family;
            txtLanguage.Text = block.language.ToString();
            txtLocalData.Text = block.localData.ToString();
            txtLoadSize.Text = block.loadSize.ToString() + " bytes";
            txtMC7Size.Text = block.MC7Size.ToString() + " bytes";
            txtSBBLength.Text = block.SBBLength.ToString() + " bytes";
            txtCodeDate.Text = block.codeDate;
            txtInterfaceDate.Text = block.interfaceDate;
            //Format checksum as hex (ab cd)
            txtChecksum.Text = block.checksum.ToString("x4").Insert(2, " ");
            txtFlags.Text = block.blockFlags.ToString("x4").Insert(2, " ");
            txtVersion.Text = block.version.ToString("x4").Insert(2, " ");
            if (block.data != null)
            {
                ByteViewer bv = new ByteViewer();
                bv.SetBytes(block.data);
                bv.Location = new Point(12, 148);
                bv.Size = new Size(419, 222);
                Controls.Add(bv);
                byte[] data = new byte[block.loadSize - 10];
                Array.Copy(block.data, data, data.Length);
                txtTestChecksum.Text = Crc16.ComputeChecksum(data).ToString("x4").Insert(2, " ");
            }
            else
            {

            }
           
        }

    }
    public static class Crc16
    {
        const ushort polynomial = 0xA001;
        static readonly ushort[] table = new ushort[256];

        public static ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        static Crc16()
        {
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }
}
