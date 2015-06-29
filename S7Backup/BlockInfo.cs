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

namespace S7Backup
{
    public partial class BlockInfo : Form
    {
        public BlockInfo(s7CpuBlock block)
        {
            InitializeComponent();

            txtBlock.Text = block.blockType.ToString() + block.blockNumber.ToString();
            txtName.Text = block.name;
            txtAuthor.Text = block.author;
            txtFamily.Text = block.family;
            txtLanguage.Text = block.language.ToString();
            txtLoadSize.Text = block.loadSize.ToString() + " bytes";
            txtMC7Size.Text = block.MC7Size.ToString() + " bytes";
            txtSBBLength.Text = block.SBBLength.ToString() + " bytes";
            txtCodeDate.Text = block.codeDate;
            txtInterfaceDate.Text = block.interfaceDate;
            //Format checksum as hex (ab cd)
            txtChecksum.Text = block.checksum.ToString("x4").Insert(2, " ");
            if (block.data != null)
            {
                ByteViewer bv = new ByteViewer();
                bv.SetBytes(block.data);
                bv.Location = new Point(12, 148);
                bv.Size = new Size(419, 222);
                Controls.Add(bv);
            }
            else
            {

            }
           
        }

    }
}
