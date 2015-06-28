using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            txtLoadSize.Text = block.loadSize.ToString() + " bytes";
            txtMC7Size.Text = block.MC7Size.ToString() + " bytes";
            txtChecksum.Text = block.checksum.ToString("x4").Insert(2, " ");
        }
    }
}
