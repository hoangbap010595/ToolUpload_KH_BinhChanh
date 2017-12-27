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
    public partial class frmActive : XtraForm
    {
        public frmActive()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIsActive_Click(object sender, EventArgs e)
        {
            //a2V5IG1hIGhvYSBwaGFuIG1lbSB0ZWVjaGlwIHBybw==
            if (txtKey.Text.Trim() == "a2V5IG1hIGhvYSBwaGFuIG1lbSB0ZWVjaGlwIHBybw")
            {
                ApplicationLibary.setActive(true);
                Application.Restart();
            }else
            {
                XtraMessageBox.Show("Something's wrong. Please try again!");
            }
        }

        private void frmActive_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
