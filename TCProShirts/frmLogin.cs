using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCProShirts
{
    public partial class frmLogin : XtraForm
    {
        private static CookieContainer cookieApplication = new CookieContainer();
        private string BROWSER_CHROME = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
        private string BROWSER_FIREFOX = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:49.0) Gecko/20100101 Firefox/49.0";
        private ApplicationUser User;
        private string currToken = "";

        public delegate void SendUser(ApplicationUser user);
        public SendUser senduser;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUserName.Text.Trim();
                var password = txtPassword.Text.Trim(); // ApplicationLibary.Base64Decode("");
                OpenFileDialog open = new OpenFileDialog();
                if (DialogResult.OK == open.ShowDialog())
                {
                    string dataFile = File.ReadAllText(open.FileName);
                    var encodeData = ApplicationLibary.Base64Decode(dataFile);
                    JObject jObj = JObject.Parse(encodeData);

                    var data = jObj["data"].ToString();
                    var offset = jObj["offset"].ToString();

                    var dataLogin = data.Remove(0, int.Parse(offset));
                    var afterDecode = ApplicationLibary.Base64Decode(dataLogin);

                    JObject jObjLogin = JObject.Parse(afterDecode);
                    username = jObjLogin["username"].ToString();
                    password = jObjLogin["password"].ToString();

                }
                if(username != "" && password != "")
                {
                    execLogin(username, password);
                }
            }catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message");
            }
        }
        
        private void execLogin(string username, string password)
        {
            frmWait frm = new frmWait();
            frm.SetCaption("Login");
            frm.SetDescription("Connecting...");
            var urlLogin = "https://pro.teechip.com/manager/auth/login";
            var data2Send = "{\"email\":\"" + username + "\",\"password\":\"" + password + "\"}";
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    var data1 = login(urlLogin, data2Send);
                    var rs = data1["data"].ToString();
                    if (int.Parse(data1["status"].ToString()) == -1)
                    {
                        var data2Send2 = "{\"email\":\"" + username + "\",\"password\":\"" + ApplicationLibary.Base64Decode(password) + "\"}";
                        var data2 = login(urlLogin, data2Send2);
                        if (int.Parse(data2["status"].ToString()) == -1)
                        {
                            frm.Invoke((MethodInvoker)delegate { frm.Close(); });
                            XtraMessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu\n" + data2["data"], "Thông báo");
                            return;
                        }
                        rs = data2["data"].ToString();
                    }
                    User = new ApplicationUser();
                    var obj = JObject.Parse(rs);
                    User.UserID = obj["_id"].ToString();
                    User.Email = obj["email"].ToString();
                    //User.Code = obj["referralCode"].ToString();
                    User.ApiKey = obj["apiKey"].ToString();
                    //User.ViewOnlyApiKey = obj["viewOnlyApiKey"].ToString();
                    User.GroupID = obj["groupId"].ToString();
                    User.EntityID = obj["entities"][0]["entityId"].ToString();
                    //User.PayableId = obj["payable"]["payableId"].ToString();
                    User.Authorization = "Basic " + ApplicationLibary.Base64Encode(":" + User.ApiKey);
                    User.UnAuthorization = "Basic " + ApplicationLibary.Base64Encode("undefined:" + User.ApiKey);
                    User.HasPassword = ApplicationLibary.Base64Encode(password);

                    frm.Invoke((MethodInvoker)delegate { frm.Close(); });
                    if (senduser != null)
                    {
                        senduser(User);
                        this.Invoke((MethodInvoker)delegate { this.Close(); });
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error");
                    frm.Invoke((MethodInvoker)delegate { frm.Close(); });
                }
            }));
            t.Start();
            frm.ShowDialog();
        }
        private Dictionary<string, object> login(string urlLogin, string data2Send)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //Step 1
            HttpWebRequest wRequest = (HttpWebRequest)WebRequest.Create(urlLogin);
            wRequest.Host = "pro.teechip.com";
            wRequest.CookieContainer = new CookieContainer();
            Dictionary<string, object> stepLogin = GetDataAPI(wRequest);
            cookieApplication = (CookieContainer)stepLogin["cookies"];
            //Step 2
            HttpWebRequest wRequestLogin = (HttpWebRequest)WebRequest.Create(urlLogin);
            wRequestLogin.Referer = "https://pro.teechip.com/manager/auth/login";
            wRequestLogin.ContentType = "application/json";
            wRequestLogin.Host = "pro.teechip.com";
            wRequestLogin.CookieContainer = cookieApplication;
            wRequestLogin.Headers.Add("x-xsrf-token", currToken);
            Dictionary<string, object> step2Login = PostDataAPI(wRequestLogin, data2Send);
            cookieApplication = (CookieContainer)step2Login["cookies"];
            var rs = step2Login["data"].ToString();
            var status = step2Login["status"].ToString();

            data.Add("data", rs);
            data.Add("status", status);

            return data;
        }

        private Dictionary<string, object> PostDataAPI(HttpWebRequest wRequest, string data2Send)
        {
            Dictionary<string, object> dataReturn = new Dictionary<string, object>();
            CookieContainer cookies = new CookieContainer();
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(data2Send);

                wRequest.Method = "POST";
                wRequest.UserAgent = BROWSER_FIREFOX;
                wRequest.ContentLength = postDataBytes.Length;

                using (Stream sr = wRequest.GetRequestStream())
                {
                    sr.Write(postDataBytes, 0, postDataBytes.Length);
                }

                HttpWebResponse wResponse = (HttpWebResponse)wRequest.GetResponse();
                foreach (Cookie cookie in wResponse.Cookies)
                {
                    if (cookie.Name.Contains("x-xsrf-token") || cookie.Name.Contains("XSRF-TOKEN"))
                        currToken = cookie.Value;
                    cookies.Add(cookie);
                }

                String htmlString;
                using (var reader = new StreamReader(wResponse.GetResponseStream()))
                {
                    htmlString = reader.ReadToEnd();
                }
                dataReturn.Add("cookies", cookies);
                dataReturn.Add("data", htmlString);
                dataReturn.Add("status", 1);
                return dataReturn;
            }
            catch (Exception ex)
            {
                dataReturn.Add("cookies", cookies);
                dataReturn.Add("data", ex.Message);
                dataReturn.Add("status", -1);
                return dataReturn;
            }

        }
        private Dictionary<string, object> GetDataAPI(HttpWebRequest wRequest, string data2Send = "")
        {
            wRequest.Method = "GET";
            wRequest.UserAgent = BROWSER_FIREFOX;
            if (data2Send != "")
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(data2Send);
                wRequest.ContentLength = postDataBytes.Length;

                using (Stream sr = wRequest.GetRequestStream())
                {
                    sr.Write(postDataBytes, 0, postDataBytes.Length);
                }
            }

            HttpWebResponse wResponse = (HttpWebResponse)wRequest.GetResponse();
            CookieContainer cookies = new CookieContainer();
            foreach (Cookie cookie in wResponse.Cookies)
            {
                if (cookie.Name.Contains("x-xsrf-token") || cookie.Name.Contains("XSRF-TOKEN"))
                    currToken = cookie.Value;
                cookies.Add(cookie);
            }

            String htmlString;
            using (var reader = new StreamReader(wResponse.GetResponseStream()))
            {
                htmlString = reader.ReadToEnd();
            }

            Dictionary<string, object> dataReturn = new Dictionary<string, object>();
            dataReturn.Add("cookies", cookies);
            dataReturn.Add("data", htmlString);

            return dataReturn;
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPassword.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin.PerformClick();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (User == null)
            {
                Application.ExitThread();
                Application.Exit();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Int64 currTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //Int64 dueTime = 1514188724000;
            Int64 dueTime = 1514446169000;
            currTime = currTime * 1000;
            if (currTime > dueTime)
            {
                btnLogin.Enabled = false;
                btnLogin.Visible = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                XtraMessageBox.Show("Thời gian dùng thử đã kết thúc", "Thông báo");
            }
            else
            {
                XtraMessageBox.Show("Thời gian dùng thử kết thúc Thursday, December 28, 2017 7:29:29 AM", "Thông báo");
            }

            //if (!ApplicationLibary.getActive())
            //{
            //    frmActive frm = new frmActive();
            //    frm.ShowDialog();
            //}else
            //{
            //}
        }
    }
}
