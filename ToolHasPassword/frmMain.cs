using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolHasPassword
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                Random rd = new Random();
                var number = "k" + DateTime.Now.Ticks.ToString("X") + rd.Next(1, 100).ToString("x");
                var email = txtEmail.Text.Trim();
                var pass = txtPassword.Text.Trim();
                var dataLogin = "{username: \"" + email + "\",password: \"" + pass + "\"}";
                var has_DataLogin = number + Base64Encode(dataLogin);


                var data = "{ data: \"" + has_DataLogin + "\", offset: " + number.Length + "}";

                var afterEncode = Base64Encode(data);
                var name = DateTime.Now.Ticks.ToString("x");
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = name;
                if (DialogResult.OK == save.ShowDialog())
                {
                    var path = save.FileName;
                    string a = writeDataToFileText(path, afterEncode);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string writeDataToFileText(string filePath, string data)
        {
            //if the file doesn't exist, create it
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine(data);
            }
            return filePath;
        }

        public static string GetSHA1HashData(string data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }

}
