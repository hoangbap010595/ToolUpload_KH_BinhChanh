using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using DevExpress.XtraEditors;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;
using SpreadShirts.Properties;

namespace SpreadShirts
{
    public partial class frmMain : XtraForm
    {
        //https://www.spreadshirt.com/userarea/-C6840/User/ApiKeys/index
        //apiKey=1c711bf5-b82d-40de-bea6-435b5473cf9b
        //sig=da24c460f069ccc8a499fd96b395e8329cd9b670
        //time=1510655123467
        private static CookieContainer cookieApplication = new CookieContainer();
        private ApplicationUser User;
        private string currToken = "";
        private List<UCItemShopSpread> lsAllShopItem;
        private List<string> lsImageFileNames;
        private DataTable dtDataTemp;
        private List<Dictionary<string, object>> listDataUpload;
        private int currentIndexUpload = -1;

        private string dataCYOID = "{\"pointOfSale\":{\"id\":\"56963c0a59248d4dfb5c3852\",\"name\":\"CYO\",\"type\":\"CYO\",\"target\":{\"id\":\"93439\"}},\"id\":\"56963c0a59248d4dfb5c3852\"}";
        private string dataMarkID = @"{""id"":""55c864cc64c7436b464aeb7b"",""pointOfSale"":{""id"":""55c864cc64c7436b464aeb7b"",""type"":""MARKETPLACE"",""target"":{""id"":""93439""},""allowed"":true}}";
        private string dataShopID = @"{""id"":""@ShopID"",""pointOfSale"":{""id"":""@ShopID"",""name"":""@ShopName"",""type"":""SHOP"",""target"":{""id"":""@TargetID"",""currencyId"":""3""},""allowed"":true},""commission"":{""amount"":0,""currencyId"":""3""}}";
        public frmMain()
        {
            InitializeComponent();
        }
        private void getUser(ApplicationUser user, CookieContainer cookies)
        {
            if (user != null)
            {
                User = user;
                cookieApplication = cookies;
                ApplicationLibary.writeLogThread(lsBoxLog, "Login Successfully", 1);
                this.Invoke((MethodInvoker)delegate { this.Text += " - [" + User.USER_ID + "]"; });
                loadAllShop();
            }
            else
            {
                this.Close();
            }
        }
        private void executeLogin(string username, string password)
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

                ApplicationLibary.writeLog(lsBoxLog, "Login Successfully", 1);
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

                lsAllShopItem = new List<UCItemShopSpread>();
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
                loadAllShop();
                /*
                 *--hoangbap1595 
                 * 73338c5b-2cbe-4333-8164-b23e30a6e0da
                 * https://www.spreadshirt.com/api/v1/sessions/73338c5b-2cbe-4333-8164-b23e30a6e0da
                 * NA-2832173b-7196-4e59-bf8d-3740fdb3c71a
                 * https://www.spreadshirt.com/api/v1/identities/NA-2832173b-7196-4e59-bf8d-3740fdb3c71a
                 * 302721328
                 * https://www.spreadshirt.com/api/v1/users/302721328
                 * 
                 * --lcoang1995
                 * 4f6e5652-e7a7-47b5-bf0a-d96c8cbaf458
                 * https://www.spreadshirt.com/api/v1/sessions/4f6e5652-e7a7-47b5-bf0a-d96c8cbaf458
                 * NA-776b20df-bfb4-4d47-a225-9d1252f9fd4c
                 * https://www.spreadshirt.com/api/v1/identities/NA-776b20df-bfb4-4d47-a225-9d1252f9fd4c
                 * 302719724
                 * https://www.spreadshirt.com/api/v1/users/302719724
                 * 
                /*-------------------LOGIN FINISH-------------------*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //string username = "hoangbap1595@gmail.com";
            string username = "lchoang1995@gmail.com";
            string password = "Thienan@111";
            executeLogin(username, password);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            UploadProgress();
        }

        private void UploadFileProgress(List<Dictionary<string, object>> listDataUpload)
        {
            foreach (Dictionary<string, object> itemUpload in listDataUpload)
            {
                try
                {
                    currentIndexUpload++;
                    var t_image = itemUpload["Image"].ToString();
                    var t_price = itemUpload["Price"].ToString();

                    string image = t_image.Split('.').Length == 1 ? t_image + ".png" : t_image;
                    string title = itemUpload["Title"].ToString();
                    string description = itemUpload["Description"].ToString();
                    string tags = itemUpload["Tag"].ToString();
                    string shop = itemUpload["Shop"].ToString();
                    double amount = string.IsNullOrEmpty(t_price) ? double.Parse(double.Parse(txtPrice.Text).ToString("N2")) : double.Parse(double.Parse(t_price).ToString("N2"));
                    if (!File.Exists(image))
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "File not found: " + Path.GetFileName(image), 3);
                        continue;
                    }
                    #region -----------Step 1: Upload Image-----------
                    ApplicationLibary.writeLogThread(lsBoxLog, "Uploadding " + Path.GetFileName(image), 3);
                    string img_UrlUpload = User.USER_HREF + "/design-uploads";
                    var urlUploadImage = ApplicationLibary.encodeURL(url: img_UrlUpload, defaultParam: "createProductIdea=true", time: ApplicationLibary.getTimeStamp());
                    NameValueCollection nvc = new NameValueCollection();
                    var data = HttpUploadFile(urlUploadImage, image, "filedata", "image/png", nvc);
                    if (int.Parse(data["status"].ToString()) == -1)
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "Step 1: " + data["data"].ToString(), 3);
                        continue;
                    }
                    JObject jObj = JObject.Parse(data["data"].ToString());
                    var designId = jObj["designId"].ToString();
                    var ideaId = jObj["ideaId"].ToString();
                    var name = jObj["name"].ToString();
                    #endregion
                    #region -----------Step 2: Upload Data-----------
                    string u_method = "PUT";
                    //https://partner.spreadshirt.com/api/v1/users/302721328/ideas/5a33342faa0c6d3e511164f3?apiKey=1c711bf5-b82d-40de-bea6-435b5473cf9b&locale=us_US&mediaType=json&sig=5a88e6520a13a9aa1f7b39036a7c120cd445ccab&time=1513305661500
                    string u_urlUpload = User.USER_HREF + "/ideas/" + ideaId;
                    string u_time = ApplicationLibary.getTimeStamp();
                    string u_dataUrl = u_method + " " + u_urlUpload + " " + u_time + "";
                    string u_sig = ApplicationLibary.sha1Generate(u_dataUrl, ApplicationLibary.SECRET);
                    Dictionary<string, object> dataObj = new Dictionary<string, object>();
                    dataObj.Add("amount", amount);
                    dataObj.Add("shop", shop);
                    dataObj.Add("ideaID", ideaId);
                    dataObj.Add("designID", designId);
                    dataObj.Add("title", title);
                    dataObj.Add("description", description);
                    dataObj.Add("tags", tags);
                    dataObj.Add("sig", u_sig);

                    string rs_UrlUpload = ApplicationLibary.encodeURL(url: u_urlUpload, method: u_method, locale: "us_US", mediaType: "json", time: u_time);
                    string rs_Data2Send = refixData2Send(dataObj);

                    HttpWebRequest wRequestUpload = (HttpWebRequest)WebRequest.Create(rs_UrlUpload);
                    wRequestUpload.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                    wRequestUpload.Accept = "application/json, text/plain, */*";
                    wRequestUpload.Host = "partner.spreadshirt.com";
                    wRequestUpload.ContentType = "application/json;charset=utf-8";
                    wRequestUpload.Referer = "https://partner.spreadshirt.com/designs/" + ideaId;
                    wRequestUpload.CookieContainer = cookieApplication;
                    wRequestUpload.ServicePoint.Expect100Continue = false;
                    wRequestUpload.ProtocolVersion = HttpVersion.Version11;
                    wRequestUpload.Timeout = 90000;
                    wRequestUpload.ReadWriteTimeout = 90000;
                    wRequestUpload.KeepAlive = true;

                    Dictionary<string, object> step2Upload = PutDataAPI(wRequestUpload, rs_Data2Send);
                    if (int.Parse(step2Upload["status"].ToString()) == -1)
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "Step 2: " + step2Upload["data"].ToString(), 1);
                        continue;
                    }
                    var objUploadEnd = JObject.Parse(step2Upload["data"].ToString());
                    var linkIdea = objUploadEnd["href"].ToString();
                    #endregion
                    #region -----------Step 3: Publish-----------
                    //https://partner.spreadshirt.com/api/v1/users/302721328/ideas/5a339e2aaa0c6d3e511e3268/publishingDetails?apiKey=1c711bf5-b82d-40de-bea6-435b5473cf9b&locale=us_US&mediaType=json&sig=ff531cb9b45015934d699c796dc013033dcff8e8&time=1513333983186
                    var urlPublish = User.USER_HREF + "/ideas/" + ideaId + "/publishingDetails";
                    string p_urlPublish = ApplicationLibary.encodeURL(url: urlPublish, method: "PUT", locale: "us_US", mediaType: "json");
                    var p_data2SendPublish = @"{""list"": [" + dataCYOID;
                    foreach (string str in getPublishingDetailsPublish(shop))
                    {
                        var x = str.Replace(@"\", "");
                        p_data2SendPublish += "," + x;
                    }
                    p_data2SendPublish = p_data2SendPublish.TrimEnd(',') + "]}";

                    HttpWebRequest wRequestPublish = (HttpWebRequest)WebRequest.Create(p_urlPublish);
                    wRequestPublish.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                    wRequestPublish.Accept = "application/json, text/plain, */*";
                    wRequestPublish.Host = "partner.spreadshirt.com";
                    wRequestPublish.ContentType = "application/json;charset=utf-8";
                    wRequestPublish.Referer = "https://partner.spreadshirt.com/designs/" + ideaId;
                    wRequestPublish.CookieContainer = cookieApplication;

                    Dictionary<string, object> step3Publish = PutDataAPI(wRequestPublish, p_data2SendPublish);
                    if (int.Parse(step3Publish["status"].ToString()) == -1)
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "Step 3: " + step3Publish["data"].ToString(), 1);
                        continue;
                    }
                    ApplicationLibary.writeLogThread(lsBoxLog, "Upload & Publish finish: " + "https://partner.spreadshirt.com/designs/" + ideaId, 1);
                    #endregion
                    MoveFileUploaded(image);
                    dtDataTemp.Rows[currentIndexUpload]["Status"] = "Done";
                    ApplicationLibary.saveDataTableToFileCSV(txtPath.Text, dtDataTemp);
                }
                catch (Exception ex)
                {
                    ApplicationLibary.writeLogThread(lsBoxLog, ex.Message, 3);
                    continue;
                }
            }
        }
        private void UploadProgress()
        {
            foreach (var fileImage in lsImageFileNames)
            {
                try
                {
                    string image = fileImage;
                    string title = txtName.Text;
                    string description = memoDescription.Text;
                    string tags = memoTag.Text;
                    string shop = getSelectShop();
                    double amount = double.Parse(double.Parse(txtPrice.Text).ToString("N2"));
                    if (!File.Exists(image))
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "File not found: " + Path.GetFileName(image), 1);
                        continue;
                    }
                    #region -----------Step 1: Upload Image-----------
                    ApplicationLibary.writeLogThread(lsBoxLog, "Uploadding " + Path.GetFileName(image), 3);
                    string img_UrlUpload = User.USER_HREF + "/design-uploads";
                    var urlUploadImage = ApplicationLibary.encodeURL(url: img_UrlUpload, defaultParam: "createProductIdea=true", time: ApplicationLibary.getTimeStamp());
                    NameValueCollection nvc = new NameValueCollection();
                    var data = HttpUploadFile(urlUploadImage, image, "filedata", "image/png", nvc);
                    if (int.Parse(data["status"].ToString()) == -1)
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "Step 1: " + data["data"].ToString(), 3);
                        continue;
                    }
                    JObject jObj = JObject.Parse(data["data"].ToString());
                    var designId = jObj["designId"].ToString();
                    var ideaId = jObj["ideaId"].ToString();
                    var name = jObj["name"].ToString();

                    #endregion
                    #region -----------Step 2: Upload Data-----------
                    string u_method = "PUT";
                    //https://partner.spreadshirt.com/api/v1/users/302721328/ideas/5a33342faa0c6d3e511164f3?apiKey=1c711bf5-b82d-40de-bea6-435b5473cf9b&locale=us_US&mediaType=json&sig=5a88e6520a13a9aa1f7b39036a7c120cd445ccab&time=1513305661500
                    string u_urlUpload = User.USER_HREF + "/ideas/" + ideaId;
                    string u_time = ApplicationLibary.getTimeStamp();
                    string u_dataUrl = u_method + " " + u_urlUpload + " " + u_time + "";
                    string u_sig = ApplicationLibary.sha1Generate(u_dataUrl, ApplicationLibary.SECRET);
                    Dictionary<string, object> dataObj = new Dictionary<string, object>();
                    dataObj.Add("amount", amount);
                    dataObj.Add("shop", shop);
                    dataObj.Add("ideaID", ideaId);
                    dataObj.Add("designID", designId);
                    dataObj.Add("title", title);
                    dataObj.Add("description", description);
                    dataObj.Add("tags", tags);
                    dataObj.Add("sig", u_sig);

                    string rs_UrlUpload = ApplicationLibary.encodeURL(url: u_urlUpload, method: u_method, locale: "us_US", mediaType: "json", time: u_time);
                    string rs_Data2Send = refixData2Send(dataObj);

                    HttpWebRequest wRequestUpload = (HttpWebRequest)WebRequest.Create(rs_UrlUpload);
                    wRequestUpload.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                    wRequestUpload.Accept = "application/json, text/plain, */*";
                    wRequestUpload.Host = "partner.spreadshirt.com";
                    wRequestUpload.ContentType = "application/json;charset=utf-8";
                    wRequestUpload.Referer = "https://partner.spreadshirt.com/designs/" + ideaId;
                    wRequestUpload.CookieContainer = cookieApplication;

                    Dictionary<string, object> step2Upload = PutDataAPI(wRequestUpload, rs_Data2Send);
                    if (int.Parse(step2Upload["status"].ToString()) == -1)
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "Step 2: " + step2Upload["data"].ToString(), 1);
                        continue;
                    }
                    var objUploadEnd = JObject.Parse(step2Upload["data"].ToString());
                    var linkIdea = objUploadEnd["href"].ToString();
                    #endregion
                    #region -----------Step 3: Publish-----------
                    //https://partner.spreadshirt.com/api/v1/users/302721328/ideas/5a339e2aaa0c6d3e511e3268/publishingDetails?apiKey=1c711bf5-b82d-40de-bea6-435b5473cf9b&locale=us_US&mediaType=json&sig=ff531cb9b45015934d699c796dc013033dcff8e8&time=1513333983186
                    var urlPublish = User.USER_HREF + "/ideas/" + ideaId + "/publishingDetails";
                    string p_urlPublish = ApplicationLibary.encodeURL(url: urlPublish, method: "PUT", locale: "us_US", mediaType: "json");
                    var p_data2SendPublish = @"{""list"": [" + dataCYOID;
                    foreach (string str in getPublishingDetailsPublish(shop))
                    {
                        var x = str.Replace(@"\", "");
                        p_data2SendPublish += "," + x;
                    }
                    p_data2SendPublish = p_data2SendPublish.TrimEnd(',') + "]}";

                    HttpWebRequest wRequestPublish = (HttpWebRequest)WebRequest.Create(p_urlPublish);
                    wRequestPublish.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                    wRequestPublish.Accept = "application/json, text/plain, */*";
                    wRequestPublish.Host = "partner.spreadshirt.com";
                    wRequestPublish.ContentType = "application/json;charset=utf-8";
                    wRequestPublish.Referer = "https://partner.spreadshirt.com/designs/" + ideaId;
                    wRequestPublish.CookieContainer = cookieApplication;

                    Dictionary<string, object> step3Publish = PutDataAPI(wRequestPublish, p_data2SendPublish);
                    if (int.Parse(step3Publish["status"].ToString()) == -1)
                    {
                        ApplicationLibary.writeLogThread(lsBoxLog, "Step 3: " + step3Publish["data"].ToString(), 1);
                        continue;
                    }

                    ApplicationLibary.writeLogThread(lsBoxLog, "Upload & Publish finish: " + "https://partner.spreadshirt.com/designs/" + ideaId, 1);
                    #endregion
                    MoveFileUploaded(image);
                }
                catch (Exception ex)
                {
                    ApplicationLibary.writeLogThread(lsBoxLog, "Upload Error: " + ex.Message, 1);
                }
            }
        }
        private string getSelectShop()
        {
            string data = "";
            foreach (UCItemShopSpread item in lsAllShopItem)
            {
                if (item.Shop.isSelected)
                {
                    data += item.Shop.Name + ",";
                }
            }
            data = data.TrimEnd(',');
            return data;
        }
        private string createSprdAuthHeader(string method, string url, string time)
        {
            string data = method + " " + url + " " + time;
            string sig = ApplicationLibary.sha1Generate(data, ApplicationLibary.SECRET);
            string header = "Authorization: SprdAuth apiKey=\"$apiKey\", data=\"$data\", sig=\"$sig\"";
            header = header.Replace("$apiKey", ApplicationLibary.API_KEY);
            header = header.Replace("$data", data);
            header = header.Replace("$sig", sig);
            return header;
        }
        public static Dictionary<string, object> HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            HttpWebRequest wr = null;
            WebResponse wresp = null;
            try
            {
                Console.WriteLine(string.Format("Uploading {0} to {1}", file, url));
                string boundary = "-----------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                wr = (HttpWebRequest)WebRequest.Create(url);
                wr.Host = "partner.spreadshirt.com";
                wr.Accept = "application/json, text/plain, */*";
                wr.ContentType = "multipart/form-data; boundary=" + boundary;
                wr.Method = "POST";
                wr.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                wr.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                wr.Headers.Add("Origin", ApplicationLibary.Origin);
                wr.Referer = "https://partner.spreadshirt.com/designs";
                wr.UserAgent = ApplicationLibary.BROWSER_FIREFOX;
                wr.CookieContainer = cookieApplication;
                wr.KeepAlive = true;

                Stream rs = wr.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in nvc.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = string.Format(headerTemplate, paramName, Path.GetFileName(file), contentType);
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                rs.Write(headerbytes, 0, headerbytes.Length);

                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    rs.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();

                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                data.Add("location", wresp.Headers["Location"]);
                data.Add("data", reader2.ReadToEnd());
                data.Add("status", 1);
            }
            catch (Exception ex)
            {
                data.Add("data", ex.Message);
                data.Add("status", -1);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return data;
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
                wRequest.Headers.Add("Origin", ApplicationLibary.Origin);//chrome-extension://aejoelaoggembcahagimdiliamlcdmfm

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
        private Dictionary<string, object> PutDataAPI(HttpWebRequest wRequest, string data2Send)
        {
            Dictionary<string, object> dataReturn = new Dictionary<string, object>();
            CookieContainer cookies = new CookieContainer();
            String htmlString;
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(data2Send);
                wRequest.Method = "PUT";
                wRequest.UserAgent = ApplicationLibary.BROWSER_FIREFOX;
                wRequest.ContentLength = postDataBytes.Length;
                wRequest.Headers.Add("Origin", ApplicationLibary.Origin);//chrome-extension://aejoelaoggembcahagimdiliamlcdmfm

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
                    wResponse.Close();
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
        private string refixData2Send(Dictionary<string, object> dataObj)
        {
            string data2SendFromFile = Resources.data2send;
            JObject obj = JObject.Parse(data2SendFromFile);
            int time = int.Parse(ApplicationLibary.getTimeStamp());
            //set Amount
            obj["commission"]["amount"] = double.Parse(dataObj["amount"].ToString());

            //publishingDetails
            JArray item = (JArray)obj["publishingDetails"];
            foreach (Dictionary<string, object> itemShop in getPublishingDetails(dataObj["shop"].ToString()))
            {
                if (itemShop["type"].ToString().Equals("MARKETPLACE"))
                {
                    item.Add(JObject.Parse(dataMarkID));
                }
                else
                {
                    var xDa = dataShopID.Replace("@ShopID", itemShop["id"].ToString()).Replace("@ShopName", itemShop["name"].ToString()).Replace("@TargetID", itemShop["targetID"].ToString());
                    item.Add(JObject.Parse(xDa));

                }
            }
            //set Time configuration
            obj["properties"]["configuration"] = time;
            //set IdeaID
            obj["id"] = dataObj["ideaID"].ToString();
            //set userId
            obj["userId"] = User.USER_ID;
            //set mainDesignId
            obj["mainDesignId"] = dataObj["designID"].ToString();
            //set name
            obj["translations"][0]["name"] = dataObj["title"].ToString();
            //set description
            obj["translations"][0]["description"] = dataObj["description"].ToString();
            //set tags
            var tags = dataObj["tags"].ToString().Split(',');
            JArray itemTag = (JArray)obj["translations"][0]["tags"];
            foreach (string str in tags)
            {
                if (!string.IsNullOrEmpty(str))
                    itemTag.Add(str);
            }
            //set dateCreated
            obj["dateCreated"] = string.Format("{0:yyyy-MM-dd'T'hh:mm:ss.fff'Z'}", DateTime.UtcNow);
            //set Amount
            obj["dateModified"] = time;

            //set href
            obj["resources"][0]["href"] = "https://image.spreadshirtmedia.com/image-server/v1/products/" + "1522508808" + "/views/1";
            //set href
            obj["resources"][1]["href"] = "https://image.spreadshirtmedia.com/image-server/v1/designs/" + dataObj["designID"].ToString();

            //set apiKey
            obj["assortment"]["reqParams"]["apiKey"] = ApplicationLibary.API_KEY;
            //set Amount
            obj["assortment"]["reqParams"]["sig"] = dataObj["sig"].ToString();
            //set time
            obj["assortment"]["reqParams"]["time"] = time;
            //set id

            obj["assortment"]["parentResource"]["parentResource"]["id"] = User.USER_ID;
            //set href
            obj["assortment"]["parentResource"]["parentResource"]["href"] = User.USER_HREF;
            //set ideaID
            obj["assortment"]["parentResource"]["id"] = dataObj["ideaID"].ToString();
            //set time
            obj["assortment"]["parentResource"]["href"] = User.USER_HREF + "/ideas/" + dataObj["ideaID"].ToString();
            //set backgroundColor
            obj["backgroundColor"] = "#666666";

            var data2Send = obj.ToString(Newtonsoft.Json.Formatting.None);
            return data2Send;
        }
        private List<Dictionary<string, object>> getPublishingDetails(string shopName)
        {
            List<Dictionary<string, object>> lsData = new List<Dictionary<string, object>>();
            Dictionary<string, object> currDic = new Dictionary<string, object>();
            currDic.Add("id", "55c864cc64c7436b464aeb7b");
            currDic.Add("name", "");
            currDic.Add("targetID", "93439");
            currDic.Add("type", "MARKETPLACE");
            lsData.Add(currDic);
            var lsShop = shopName.Split(',');
            foreach (string strShop in lsShop)
            {
                if (!string.IsNullOrEmpty(strShop))
                {
                    currDic = new Dictionary<string, object>();
                    foreach (OShop shop in User.SHOPS)
                    {
                        if (shop.Name.Contains(strShop) || shop.TargetID.Contains(strShop))
                        {
                            currDic.Add("id", shop.Id);
                            currDic.Add("name", shop.Name);
                            currDic.Add("targetID", shop.TargetID);
                            currDic.Add("type", "SHOP");
                            lsData.Add(currDic);
                        }
                    }
                }
            }

            return lsData;
        }
        private List<string> getPublishingDetailsPublish(string shopName)
        {
            List<string> data = new List<string>();
            data.Add(dataMarkID);
            var lsShop = shopName.Split(',');
            foreach (string strShop in lsShop)
            {
                if (!string.IsNullOrEmpty(strShop))
                    foreach (OShop shop in User.SHOPS)
                    {
                        if (shop.Name.Contains(strShop) || shop.TargetID.Contains(strShop))
                        {
                            string x = dataShopID.Replace("@ShopID", shop.Id).Replace("@ShopName", shop.Name).Replace("@TargetID", shop.TargetID);
                            data.Add(x);
                        }
                    }
            }

            return data;
        }
        private void MoveFileUploaded(string fileName)
        {
            var path = Path.GetDirectoryName(fileName) + "\\Uploaded\\";
            var name = Path.GetFileName(fileName);
            var newDir = Path.Combine(path, name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            File.Move(fileName, newDir);
        }
        private void loadAllShop()
        {
            foreach (OShop item in User.SHOPS)
            {
                UCItemShopSpread frm = new UCItemShopSpread();
                frm.Shop = item;
                addShopToPanel(frm);
            }
        }
        private void addShopToPanel(UCItemShopSpread frm)
        {
            frm.Location = new Point(10, (lsAllShopItem.Count * frm.Height) + (lsAllShopItem.Count * 10) + 10);
            frm.Width = xtraScrollableShop.Width - 30;
            lsAllShopItem.Add(frm);

            xtraScrollableShop.Invoke((MethodInvoker)delegate { xtraScrollableShop.Controls.Clear(); });
            foreach (var item in lsAllShopItem)
            {
                xtraScrollableShop.Invoke((MethodInvoker)delegate { xtraScrollableShop.Controls.Add(item); });
            }
        }
        private void loadDataToTable(DataTable dt)
        {
            int index = 0;
            Dictionary<string, object> data;
            for (; index < dt.Rows.Count; index++)
            {
                data = new Dictionary<string, object>();
                for (int j = 0; j < dtDataTemp.Columns.Count; j++)
                {
                    var col = dtDataTemp.Columns[j].ColumnName;
                    var dr = dtDataTemp.Rows[index][col].ToString();
                    data.Add(col, dr);
                }
                listDataUpload.Add(data);
            }

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            User = new ApplicationUser();
            lsAllShopItem = new List<UCItemShopSpread>();
            frmLogin frm = new frmLogin();
            frm.senduser = new frmLogin.SendUser(getUser);
            frm.ShowDialog();
        }
        private void btnChooesImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = true;
                op.Filter = "Image .png|*.png";
                if (DialogResult.OK == op.ShowDialog())
                {
                    lsImageFileNames = op.FileNames.ToList();
                    foreach (var item in lsImageFileNames)
                    {
                        lsBoxImage.Items.Add(Path.GetFileName(item));
                    }
                    ApplicationLibary.writeLog(lsBoxLog, "Selected " + lsImageFileNames.Count + " file(s)", 1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnWriteLog_Click(object sender, EventArgs e)
        {
            try
            {
                string data = "========================LOG EVENT========================";
                foreach (var item in lsBoxLog.Items)
                {
                    data += "\r\n" + item.ToString();
                }
                if (data != "")
                {
                    string file = ApplicationLibary.writeDataToFileText(data);
                    Process.Start(file);
                }
            }
            catch
            {

            }
        }
        private void btnOpenFileExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel .csv|*.csv|Excel .xlsx|*.xlsx|Excel .xls|*.xls";
                if (DialogResult.OK == op.ShowDialog())
                {
                    txtPath.Text = op.FileName;
                    dtDataTemp = new DataTable();
                    listDataUpload = new List<Dictionary<string, object>>();
                    var x = Path.GetExtension(op.FileName);
                    if (x == ".csv")
                        dtDataTemp = ApplicationLibary.getDataExcelFromFileCSVToDataTable(op.FileName);
                    else
                        dtDataTemp = ApplicationLibary.getDataExcelFromFileToDataTable(op.FileName);
                    loadDataToTable(dtDataTemp);
                    ApplicationLibary.writeLog(lsBoxLog, "Success " + dtDataTemp.Rows.Count + " record(s) is opened", 1);
                    // testSaveFile();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (lsBoxLog.Items.Count > 0)
            {
                lsBoxLog.Items.Clear();
                ApplicationLibary.writeLog(lsBoxLog, "Cleared", 1);
            }
        }
        private void enableB(bool b)
        {
            btnStart.Enabled = b;
            btnClear.Enabled = b;
            btnOpenFileExcel.Enabled = b;
        }
        private void enableBThread(bool b)
        {
            btnStart.Invoke((MethodInvoker)delegate { btnStart.Enabled = b; });
            btnClear.Invoke((MethodInvoker)delegate { btnClear.Enabled = b; });
            btnOpenFileExcel.Invoke((MethodInvoker)delegate { btnOpenFileExcel.Enabled = b; });
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            currentIndexUpload = -1;
            enableB(false);

            if (ckUsingFileUpload.Checked)
            {
                if (dtDataTemp == null || dtDataTemp.Rows.Count == 0)
                {
                    XtraMessageBox.Show("File data is not found!", "Message");
                    enableB(true);
                    return;
                }
                if (double.Parse(txtPrice.Text) < 0 || double.Parse(txtPrice.Text) > 20)
                {
                    XtraMessageBox.Show("Choose a design price between $0.00 and $20.00", "Message");
                    enableB(true);
                    return;
                }
                Thread tStart1 = new Thread(new ThreadStart(() =>
                {
                    UploadFileProgress(listDataUpload);
                    enableBThread(true);
                }));
                tStart1.Start();
            }
            else
            {
                if (txtName.Text == "")
                {
                    XtraMessageBox.Show("Please enter field name!", "Message");
                    enableB(true);
                    return;
                }
                if (memoDescription.Text == "")
                {
                    XtraMessageBox.Show("Please enter field description!", "Message");
                    enableB(true);
                    return;
                }
                if (memoDescription.Text == "")
                {
                    XtraMessageBox.Show("Please enter field tag!", "Message");
                    enableB(true);
                    return;
                }
                if (double.Parse(txtPrice.Text) < 0 || double.Parse(txtPrice.Text) > 20)
                {
                    XtraMessageBox.Show("Choose a design price between $0.00 and $20.00", "Message");
                    enableB(true);
                    return;
                }
                else
                {
                    var x = memoTag.Text.Trim().Split(',');
                    if (x.Count() < 3)
                    {
                        XtraMessageBox.Show("Please enter more than 3 keywords!", "Message");
                        enableB(true);
                        return;
                    }
                }
                if(lsImageFileNames == null || lsImageFileNames.Count == 0)
                {
                    XtraMessageBox.Show("Please choose image desgin!", "Message");
                    enableB(true);
                    return;
                }
                Thread tStart = new Thread(new ThreadStart(() =>
                {
                    UploadProgress();
                    enableBThread(true);
                }));
                tStart.Start();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                var x = txtPrice.Text;
                if (x.IndexOf('.') > -1 && x != "")
                    e.Handled = true;
            }
            else if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
            if (txtPrice.Text == "")
            {
                txtPrice.Text = "00.00";
            }
        }
        private void ckUsingFileUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (ckUsingFileUpload.Checked)
            {
                btnOpenFileExcel.Enabled = true;
                ApplicationLibary.writeLog(lsBoxLog, "Enable upload styles using file", 1);
            }
            else
            {
                btnOpenFileExcel.Enabled = false;
                ApplicationLibary.writeLog(lsBoxLog, "Disable upload styles using file", 1);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }
    }
}
