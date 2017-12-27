using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCProShirts
{
    public partial class frmDialog : XtraForm
    {
        public frmDialog(int flag = 0)
        {
            InitializeComponent();
            if (flag == 1)
                btnCancel.Visible = true;
            else
                btnCancel.Visible = false;
        }
        public delegate void SendText(string text);
        public SendText sendText;
        public frmDialog(string text)
        {
            InitializeComponent();
            txtText.Text = text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtText.Text.Trim()))
            {
                XtraMessageBox.Show("Text is empty..!", "Message");
                return;
            }
            if (sendText != null)
            {
                sendText(txtText.Text.Trim());
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
