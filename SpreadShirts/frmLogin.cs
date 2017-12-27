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

namespace SpreadShirts
{
    public partial class frmLogin : XtraForm
    {
        private static CookieContainer cookieApplication = new CookieContainer();
        private ApplicationUser User;
        private string currToken = "";

        public delegate void SendUser(ApplicationUser user, CookieContainer cookies);
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
                if (username != "" && password != "")
                {
                    executeLogin(username, password);
                }
                else
                {
                    XtraMessageBox.Show("Không tìm thấy dữ liệu!", "Message");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message");
            }
        }
        private void executeLogin(string username, string password)
        {
            frmWait frm = new frmWait();
            frm.SetCaption("Login");
            frm.SetDescription("Connecting...");
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    var urlLogin = ApplicationLibary.encodeURL("https://partner.spreadshirt.com/api/v1/sessions", "", "POST", "us_US", "json", "");
                    //string urlLogin = "https://www.spreadshirt.com/api/v1/sessions?mediaType=json";
                    string data2Send = "{\"rememberMe\":false,\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

                    HttpWebRequest wRequestLogin = (HttpWebRequest)WebRequest.Create(urlLogin);
                    wRequestLogin.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                    wRequestLogin.Accept = "application/json, text/plain, */*";
                    wRequestLogin.Host = "partner.spreadshirt.com";
                    wRequestLogin.ContentType = "application/json;charset=utf-8";
                    wRequestLogin.Referer = "https://partner.spreadshirt.com/login";
                    wRequestLogin.CookieContainer = new CookieContainer();

                    Dictionary<string, object> step2Login = PostDataAPI(wRequestLogin, data2Send);
                    cookieApplication = (CookieContainer)step2Login["cookies"];
                    var rs = step2Login["data"].ToString();
                    if (int.Parse(step2Login["status"].ToString()) == -1)
                    {
                        XtraMessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu\n" + rs, "Thông báo");
                        return;
                    }

                    var obj = JObject.Parse(rs);
                    User = new ApplicationUser();
                    User.SESSION_ID = obj["id"].ToString();
                    User.SESSION_HREF = obj["href"].ToString();
                    User.IDENTITY_ID = obj["identity"]["id"].ToString();
                    User.IDENTITY_HREF = obj["identity"]["href"].ToString();
                    User.USER_ID = obj["user"]["id"].ToString();
                    User.USER_HREF = obj["user"]["href"].ToString();

                    string urlShop = User.USER_HREF + "/pointsOfSale";
                    urlShop = ApplicationLibary.encodeURL(urlShop, "", "GET", "us_US", "json", "");

                    HttpWebRequest wRequestShopping = (HttpWebRequest)WebRequest.Create(urlShop);
                    wRequestShopping.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                    wRequestShopping.Accept = "application/json, text/plain, */*";
                    wRequestShopping.Host = "partner.spreadshirt.com";
                    wRequestShopping.ContentType = "application/json;charset=utf-8";
                    wRequestShopping.CookieContainer = cookieApplication;
                    Dictionary<string, object> dataShop = GetDataAPI(wRequestShopping);

                    JObject objShop = JObject.Parse(dataShop["data"].ToString());
                    var listShop = objShop["list"].ToString();
                    JArray arrShop = JArray.Parse(listShop);

                    User.SHOPS = new List<OShop>();
                    foreach (var item in arrShop)
                    {
                        if (!item["type"].ToString().Equals("MARKETPLACE") && !item["type"].ToString().Equals("CYO"))
                        {
                            OShop o = new OShop();
                            o.Id = item["id"].ToString();
                            o.Name = item["name"].ToString();
                            o.TargetID = item["target"]["id"].ToString();
                            o.Type = item["type"].ToString();
                            User.SHOPS.Add(o);
                        }
                    }
                    frm.Invoke((MethodInvoker)delegate { frm.Close(); });
                    if (senduser != null)
                    {
                        senduser(User, cookieApplication);
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
            User = new ApplicationUser();
            Int64 currTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int64 dueTime = 1514446169000;
            currTime = currTime * 1000;
            if (currTime > dueTime)
            {
                btnLogin.Enabled = false;
                btnLogin.Visible = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                XtraMessageBox.Show("Thời gian dùng thử đã kết thúc", "Thông báo");
            }else
            {
                XtraMessageBox.Show("Thời gian dùng thử kết thúc Thursday, December 28, 2017 7:29:29 AM", "Thông báo");
            }
        }
        private Dictionary<string, object> PostDataAPI(HttpWebRequest wRequest, string data2Send)
        {
            Dictionary<string, object> dataReturn = new Dictionary<string, object>();
            CookieContainer cookies = new CookieContainer();
            String htmlString;
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(data2Send);
                wRequest.Method = "POST";
                wRequest.UserAgent = ApplicationLibary.BROWSER_FIREFOX;
                wRequest.ContentLength = postDataBytes.Length;
                wRequest.Headers.Add("Origin", ApplicationLibary.Origin);

                using (Stream sr = wRequest.GetRequestStream())
                {
                    sr.Write(postDataBytes, 0, postDataBytes.Length);
                }

                using (HttpWebResponse wResponse = (HttpWebResponse)wRequest.GetResponse())
                {
                    foreach (Cookie cookie in wResponse.Cookies)
                    {
                        if (cookie.Name.Contains("sprd_auth_token"))
                            currToken = cookie.Value;
                        cookies.Add(cookie);
                    }
                    using (var reader = new StreamReader(wResponse.GetResponseStream()))
                    {
                        htmlString = reader.ReadToEnd();
                    }
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
            wRequest.UserAgent = ApplicationLibary.BROWSER_FIREFOX;
            wRequest.Headers.Add("Origin", ApplicationLibary.Origin);
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
                if (cookie.Name.Contains("sprd_auth_token"))
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
    }
}
