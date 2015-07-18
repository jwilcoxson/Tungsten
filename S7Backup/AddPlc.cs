using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tungsten
{
    public partial class AddPlc : Form
    {
        public string ipAddress { get; private set; }
        public string bookmarkName { get; private set; }

        public AddPlc()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ipAddress = txtIpAddress.ToString();
            bookmarkName = txtBookmarkName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
