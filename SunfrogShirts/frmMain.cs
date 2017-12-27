using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SunfrogShirts.Properties;

namespace SunfrogShirts
{
    public partial class frmMain : XtraForm
    {
        private ObjColor objColor = new ObjColor();
        private Category objCategory = new Category();
        private List<Category> lsCategory;
        private List<Category> lsCategoryProduct;
        private DataTable dtDataTemp = new DataTable();
        private DataTable dtDataTempColor = new DataTable();
        private CookieContainer cookieContainer;
        private string currentImageUploadOne = "";
        private string dirSaveStatusFile = Directory.GetCurrentDirectory() + "\\FileDataTool.xlsx";
        private string dirSaveImageSunUploaded = Directory.GetCurrentDirectory() + "\\SunfrogUploaded\\";
        private string dirSaveImageUploaded = Directory.GetCurrentDirectory() + "\\ImageUploaded\\";

        public frmMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Tạo folder save file sau khi upload thành công
            if (!Directory.Exists(dirSaveImageSunUploaded))
                Directory.CreateDirectory(dirSaveImageSunUploaded);
            if (!Directory.Exists(dirSaveImageUploaded))
                Directory.CreateDirectory(dirSaveImageUploaded);
            //clearContent();
            //Khởi tạo dtDataTempColor
            dtDataTempColor = new DataTable();
            dtDataTempColor.Columns.AddRange(new DataColumn[] {
                new DataColumn("Id"), new DataColumn("Name"), new DataColumn("Price")
                ,new DataColumn("Color1"), new DataColumn("Color2")
                ,new DataColumn("Color3"), new DataColumn("Color4")
                ,new DataColumn("Color5")
            });
            //Khởi tạo dtDataTemp
            dtDataTemp = new DataTable();
            dtDataTemp.Columns.AddRange(new DataColumn[] {
                new DataColumn("FrontBack"), new DataColumn("Image")
                , new DataColumn("Title"),new DataColumn("Category")
                , new DataColumn("Description"),new DataColumn("Keyword")
                , new DataColumn("Collection"),new DataColumn("Status")
            });

            lsCategory = objCategory.getListCategory();
            lsCategoryProduct = objCategory.getListCategoryProduct();

            info_cbbCategory.Properties.DataSource = lsCategoryProduct;
            info_cbbCategory.Properties.DisplayMember = "Name";
            info_cbbCategory.Properties.ValueMember = "Id";

            cbbCategory.Properties.DataSource = lsCategory;
            cbbCategory.Properties.DisplayMember = "Name";
            cbbCategory.Properties.ValueMember = "Id";

            cbbCategory.ItemIndex = 0;
            info_cbbCategory.ItemIndex = 0;

            cookieContainer = new CookieContainer();
            if (cookieContainer.Count == 0)
            {
                CoreLibary.writeLog(lsBoxLog, "Your session has expired and you are no longer logged in.", 1);
                groupControlInfo.Enabled = groupControlSelectTheme.Enabled = groupControlTheme.Enabled = panelControlAction.Enabled = false;
            }
        }
        /// <summary>
        /// Chọn màu cho themes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbCategory_EditValueChanged(object sender, EventArgs e)
        {
            var obj = cbbCategory.GetSelectedDataRow();
            switch (((SunfrogShirts.Category)obj).Id)
            {
                case 8:
                case 34:
                    List<ObjColor> lsColor = objColor.getObjColorGuysTeeAndLadiesTee();
                    addListColor(lsColor);
                    break;
                case 35:
                    List<ObjColor> lsColor1 = objColor.getObjColorYouthTee();
                    addListColor(lsColor1);
                    break;
                case 19:
                    List<ObjColor> lsColor2 = objColor.getObjColorHoodie();
                    addListColor(lsColor2);
                    break;
                case 27:
                    List<ObjColor> lsColor3 = objColor.getObjColorSweatSirt();
                    addListColor(lsColor3);
                    break;
                case 50:
                    List<ObjColor> lsColor4 = objColor.getObjColorGuysVNeck();
                    addListColor(lsColor4);
                    break;
                case 116:
                    List<ObjColor> lsColor5 = objColor.getObjColorLadiesVNeck();
                    addListColor(lsColor5);
                    break;
                case 118:
                case 119:
                    List<ObjColor> lsColor7 = objColor.getObjColorUnisex();
                    addListColor(lsColor7);
                    break;
                case 120:
                case 128:
                    List<ObjColor> lsColor8 = objColor.getObjColorBlack();
                    addListColor(lsColor8);
                    break;
                case 129:
                case 145:
                case 137:
                case 138:
                case 139:
                case 140:
                case 143:
                    List<ObjColor> lsColor9 = objColor.getObjColorWhite();
                    addListColor(lsColor9);
                    break;
                case 147:
                    List<ObjColor> lsColor10 = objColor.getObjColorHat();
                    addListColor(lsColor10);
                    break;
                case 148:
                    List<ObjColor> lsColor11 = objColor.getObjColorTruckerCap();
                    addListColor(lsColor11);
                    break;
            }
        }
        /// <summary>
        /// Add new themes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTheme_Click(object sender, EventArgs e)
        {
            var uChecked = 0;
            List<string> lsSelectColor = new List<string>();
            for (int i = 0; i < ckListBox.Items.Count; i++)
            {
                if (ckListBox.GetItemChecked(i))
                {
                    string str = ckListBox.GetItemText(ckListBox.Items[i]);
                    lsSelectColor.Add(str);
                    uChecked++;
                }
            }

            if (uChecked > 5)
            {
                XtraMessageBox.Show("Maximum is 5 colors", "Message");
                return;
            }
            if (uChecked == 0)
            {
                XtraMessageBox.Show("No color selected", "Message");
                return;
            }

            //Add theme here
            var obj = cbbCategory.GetSelectedDataRow();

            DataRow dr = dtDataTempColor.NewRow();
            dr[0] = ((SunfrogShirts.Category)obj).Id;
            dr[1] = ((SunfrogShirts.Category)obj).Name;
            dr[2] = ((SunfrogShirts.Category)obj).Price;
            int j = 3;
            foreach (string strColor in lsSelectColor)
            {
                dr[j] = strColor;
                j++;
            }
            int checkExists = 0;
            foreach (DataRow drFind in dtDataTempColor.Rows)
            {
                if (drFind["Id"].ToString().Equals(dr[0].ToString()))
                {
                    checkExists++;
                }
            }
            if (checkExists == 0)
                dtDataTempColor.Rows.Add(dr);
            dtDataTempColor.AcceptChanges();

            gridControl1.DataSource = dtDataTempColor;
        }
        /// <summary>
        /// Chọn file Excel chứa dữ liệu cần upload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFileExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel .xlsx|*.xlsx|Excel .xls|*.xls";
                if (DialogResult.OK == op.ShowDialog())
                {
                    txtPath.Text = op.FileName;
                    dtDataTemp = new DataTable();
                    dtDataTemp = CoreLibary.getDataExcelFromFileToDataTable(op.FileName);
                    CoreLibary.writeLog(lsBoxLog, "Success " + dtDataTemp.Rows.Count + " record(s) is opened", 1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Clear log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            lsBoxLog.Items.Clear();
            CoreLibary.writeLog(lsBoxLog, "CLEAR", 1);
        }
        /// <summary>
        /// Clear Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearColor_Click(object sender, EventArgs e)
        {
            dtDataTempColor.Rows.Clear();
            dtDataTempColor.AcceptChanges();
            gridControl1.Refresh();
        }
        /// <summary>
        /// Đăng nhập vào chương trình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmWait frm = new frmWait();
            frm.SetCaption("Connecting To Server");
            frm.SetDescription("Please wait...");
            Thread t = new Thread(new ThreadStart(() =>
            {
                HttpWebRequest wRequest1 = (HttpWebRequest)WebRequest.Create("https://manager.sunfrogshirts.com/");
                //wRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0";
                //wRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //wRequest.Referer = "https://manager.sunfrogshirts.com/";
                wRequest1.Method = "GET";
                //wRequest.KeepAlive = true;lo
                wRequest1.CookieContainer = cookieContainer;
                var cookieHeader1 = "";
                WebResponse resp1 = wRequest1.GetResponse();
                cookieHeader1 = resp1.Headers["Set-cookie"];

                foreach (Cookie cookie in ((HttpWebResponse)resp1).Cookies)
                {
                    cookieContainer.Add(cookie);
                }


                var userName = sys_txtAccount.Text;
                var password = sys_txtPassword.Text;
                //data
                //username=lchoang1995%40gmail.com&password=Omega%40111&login=Login
                ASCIIEncoding encoding = new ASCIIEncoding();
                var enUserName = HttpUtility.UrlEncode(userName);
                var enPassword = HttpUtility.UrlEncode(password);
                string data = "username=" + enUserName + "&password=" + enPassword + "&login=Login";
                byte[] postDataBytes = encoding.GetBytes(data);

                HttpWebRequest wRequest = (HttpWebRequest)WebRequest.Create("https://manager.sunfrogshirts.com/");
                //wRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0";
                //wRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //wRequest.Referer = "https://manager.sunfrogshirts.com/";
                wRequest.Method = "POST";
                wRequest.ContentType = "application/x-www-form-urlencoded";
                wRequest.ContentLength = postDataBytes.Length;
                //wRequest.KeepAlive = true;lo
                wRequest.CookieContainer = cookieContainer;

                using (Stream sr = wRequest.GetRequestStream())
                {
                    sr.Write(postDataBytes, 0, postDataBytes.Length);
                }
                var cookieHeader = "";
                WebResponse resp = wRequest.GetResponse();
                cookieHeader = resp.Headers["Set-cookie"];

                foreach (Cookie cookie in ((HttpWebResponse)resp).Cookies)
                {
                    cookieContainer.Add(cookie);
                }

                var pageSource = "";
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    pageSource = sr.ReadToEnd();
                }
                //<strong style="font-size:1.5em; line-height:15px; padding-bottom:0px;" class="clearfix">(.*?)</strong>
                var regex = "<strong style=\"font-size:1.5em; line-height:15px; padding-bottom:0px;\" class=\"clearfix\">(?<myId>.*?)</strong>";
                MatchCollection matchCollection = Regex.Matches(pageSource, regex);

                if (matchCollection.Count == 0)
                {
                    XtraMessageBox.Show("Login is disabled, and the password is incorrect!", "Message");
                    frm.Invoke((MethodInvoker)delegate { frm.Close(); });
                    imgLogo.Invoke((MethodInvoker)delegate { imgLogo.Visible = false; });
                    CoreLibary.writeLogThread(lsBoxLog, "User Login", 2);
                    return;
                }
                foreach (Match match in matchCollection)
                {
                    this.Invoke((MethodInvoker)delegate { this.Text += " - [Your Seller ID: " + match.Groups["myId"].Value + "]"; });
                }
                frm.Invoke((MethodInvoker)delegate { frm.Close(); });
                imgLogo.Invoke((MethodInvoker)delegate { imgLogo.Visible = true; });
                groupControlInfo.Invoke((MethodInvoker)delegate { groupControlInfo.Enabled = true; });
                groupControlSelectTheme.Invoke((MethodInvoker)delegate { groupControlSelectTheme.Enabled = true; });
                groupControlTheme.Invoke((MethodInvoker)delegate { groupControlTheme.Enabled = true; });
                panelControlAction.Invoke((MethodInvoker)delegate { panelControlAction.Enabled = true; });
                CoreLibary.writeLogThread(lsBoxLog, "User Login", 1);
            }));
            t.Start();
            frm.ShowDialog();
        }
        /// <summary>
        /// Upload 1 hình 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dtDataTempColor.Rows.Count == 0)
            {
                XtraMessageBox.Show("No themes selected!", "Message");
                return;
            }
            var title = info_txtTitle.Text.Trim();
            var obj = info_cbbCategory.GetSelectedDataRow();
            var category = ((SunfrogShirts.Category)obj).Name;
            var description = info_txtDescription.Text.Trim();
            var keyword = info_txtKeyWord.Text.Trim();
            var collection = info_txtCollection.Text.Trim();
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description)
                || string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(collection)
                || string.IsNullOrEmpty(currentImageUploadOne))
            {
                XtraMessageBox.Show("Title, Description, Keyword, Collection and Image is not empty.", "Message");
                return;
            }
            panelControlAction.Enabled = false;
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    dtDataTemp.Rows.Clear();
                    DataRow dr = dtDataTemp.NewRow();
                    //Load Data
                    if (info_ckFront.Checked)
                        dr["FrontBack"] = "F";
                    else
                        dr["FrontBack"] = "B";
                    dr["Image"] = currentImageUploadOne;
                    dr["Title"] = title;
                    dr["Category"] = category;
                    dr["Description"] = description;
                    dr["Keyword"] = keyword;
                    dr["Collection"] = collection;
                    dtDataTemp.Rows.Add(dr);
                    DataRow drItem = dtDataTemp.Rows[0];

                    CoreLibary.writeLogThread(lsBoxLog, "Uploading Item " + drItem[2].ToString(), 3);
                    UploadAndDownload(drItem);
                    CoreLibary.writeLogThread(lsBoxLog, "Uploaded Item " + drItem[2].ToString(), 1);

                }
                catch (Exception ex)
                {
                    CoreLibary.writeLogThread(lsBoxLog, "Uploaded image to Sunfrog", 2);
                    CoreLibary.writeLogThread(lsBoxLog, ex.Message, 2);
                    XtraMessageBox.Show("Upload failed.\n" + ex.Message, "Message");
                }
                panelControlAction.Invoke((MethodInvoker)delegate { panelControlAction.Enabled = true; });
            }));
            t.Start();
        }
        /// <summary>
        /// Upload hình lấy thông tin từ file Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            //Check data
            if (dtDataTemp.Rows.Count == 0)
            {
                XtraMessageBox.Show("Not found data!", "Message");
                return;
            }
            //Check data
            if (dtDataTempColor.Rows.Count == 0)
            {
                XtraMessageBox.Show("No themes selected!", "Message");
                return;
            }
            panelControlAction.Enabled = false;
            Thread t = new Thread(new ThreadStart(() =>
            {
                foreach (DataRow dr in dtDataTemp.Rows)
                {
                    try
                    {
                        CoreLibary.writeLogThread(lsBoxLog, "Uploading " + dr["Title"].ToString(), 3);
                        UploadAndDownload(dr);
                        CoreLibary.writeLogThread(lsBoxLog, "Uploaded " + dr["Title"].ToString(), 1);
                    }
                    catch (Exception ex)
                    {
                        CoreLibary.writeLogThread(lsBoxLog, "Upload Error: " + ex.Message, 2);
                    }
                }
                panelControlAction.Invoke((MethodInvoker)delegate { panelControlAction.Enabled = true; });
            }));
            t.Start();
        }
        /// <summary>
        /// Chọn image để hiển thị upload 1 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void info_picImageView_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "PNG .png|*.png|JPG .jpg|*.jpg";
                if (DialogResult.OK == op.ShowDialog())
                {
                    currentImageUploadOne = op.FileName;
                    info_picImageView.Image = Image.FromFile(currentImageUploadOne);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Xem Dữ liệu của file Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewData_Click(object sender, EventArgs e)
        {
            //UpdateExcel("", 3, 8, "Done");
        }
        /// <summary>
        /// Lưu theme lại thành file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTheme_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Mở các file chứa themes đã lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTheme_Click(object sender, EventArgs e)
        {

        }

        #region=========================Function=========================
        /// <summary>
        /// Khởi tạo các giá trị của control khi load form
        /// </summary>
        private void clearContent()
        {
            txtPath.Text = sys_txtPassword.Text = sys_txtAccount.Text
                = info_txtCollection.Text = info_txtDescription.Text
                = info_txtKeyWord.Text = info_txtTitle.Text = "";
        }
        /// <summary>
        /// Lấy chuỗi json màu sắc và danh mục
        /// </summary>
        /// <returns>Chuỗi json các màu sắc và category đã chọn</returns>
        private string getListTheme()
        {
            //  themes = "{\"id\":8,\"name\":\"Guys Tee\",\"price\":19,\"colors\":[\"Orange\",\"Yellow\"]}";
            string result = "[";
            foreach (DataRow drTheme in dtDataTempColor.Rows)
            {
                var color = "";
                result += "{\"id\": " + drTheme["Id"] + ", \"name\": \"" + drTheme["Name"] + "\", \"price\": " + drTheme["Price"] + ",";
                result += "\"colors\":[";
                if (!string.IsNullOrEmpty(drTheme["Color1"].ToString()))
                    color += drTheme["Color1"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color2"].ToString()))
                    color += drTheme["Color2"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color3"].ToString()))
                    color += drTheme["Color3"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color4"].ToString()))
                    color += drTheme["Color4"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color5"].ToString()))
                    color += drTheme["Color5"].ToString() + ",";
                result += CoreLibary.convertStringToJson(color);
                result += "]";
                result += "},";
            }
            result = result.TrimEnd(',');
            result += "]";
            return result;
        }
        /// <summary>
        /// Lấy chuỗi json màu sắc và danh mục
        /// </summary>
        /// <returns>Chuỗi json các màu sắc và category đã chọn</returns>
        private List<string> getListThemeObject()
        {
            List<string> data = new List<string>();
            //  themes = "{\"id\":8,\"name\":\"Guys Tee\",\"price\":19,\"colors\":[\"Orange\",\"Yellow\"]}";
            foreach (DataRow drTheme in dtDataTempColor.Rows)
            {
                var result = "";
                var color = "";
                result += "{\"id\": " + drTheme["Id"] + ", \"name\": \"" + drTheme["Name"] + "\", \"price\": " + drTheme["Price"] + ",";
                result += "\"colors\":[";
                if (!string.IsNullOrEmpty(drTheme["Color1"].ToString()))
                    color += drTheme["Color1"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color2"].ToString()))
                    color += drTheme["Color2"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color3"].ToString()))
                    color += drTheme["Color3"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color4"].ToString()))
                    color += drTheme["Color4"].ToString() + ",";
                if (!string.IsNullOrEmpty(drTheme["Color5"].ToString()))
                    color += drTheme["Color5"].ToString() + ",";
                result += CoreLibary.convertStringToJson(color);
                result += "]}";
                data.Add(result);
            }
            return data;
        }
        /// <summary>
        /// Lấy giá trị Id của category
        /// </summary>
        /// <param name="category">Tên category cần lấy ID</param>
        /// <returns>ID category</returns>
        private string getIDCategory(string category)
        {
            string id = "";
            foreach (Category cate in lsCategoryProduct)
            {
                if (cate.Name.Contains(category))
                {
                    id = cate.Id.ToString();
                    break;
                }
            }
            return id;
        }
        /// <summary>
        /// Update content row in file Excel
        /// Cập nhật trạng thái status của hình vừa upload: Done
        /// </summary>
        /// <param name="sheetName">Tên sheet. Chọn sheet mặc định mở nếu sheetName = ""</param>
        /// <param name="row">Dòng cần cập nhật</param>
        /// <param name="col">Cột cần cập nhật</param>
        /// <param name="data">Dữ liệu cần cập nhật</param>
        private void UpdateExcel(string sheetName, int row, int col, string data)
        {
            Microsoft.Office.Interop.Excel.Application oXL = null;
            Microsoft.Office.Interop.Excel._Workbook oWB = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;

            try
            {
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oWB = oXL.Workbooks.Open(dirSaveStatusFile);
                oSheet = String.IsNullOrEmpty(sheetName) ? (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet : (Microsoft.Office.Interop.Excel._Worksheet)oWB.Worksheets[sheetName];

                oSheet.Cells[row, col] = data;

                oWB.Save();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (oWB != null)
                    oWB.Close();
            }
        }
        /// <summary>
        /// Upload file to Sunfrog
        /// </summary>
        /// <param name="dr">Nội dụng dữ liệu cần upload</param>
        private void UploadAndDownload(DataRow dr)
        {
            //Config Data
            var frontbackImage = dr["FrontBack"].ToString().Trim();
            var pathImage = dr["Image"].ToString().Trim();
            var title = dr["Title"].ToString().Trim();
            var category = getIDCategory(dr["Category"].ToString().Trim());
            var description = dr["Description"].ToString().Trim();
            var keyword = CoreLibary.convertStringToJson(dr["Keyword"].ToString().Trim());
            var collection = dr["Collection"].ToString().Trim();
            var themes = getListTheme();
            var imgBase64 = CoreLibary.ConvertImageToBase64(pathImage);

            //themes = "{\"id\":8,\"name\":\"Guys Tee\",\"price\":19,\"colors\":[\"Orange\",\"Yellow\"]}";
            //themes += ",{\"id\":19,\"name\":\"Hoodie\",\"price\":34,\"colors\":[\"White\",\"Green\"]}";
            //2. Upload Image
            //var strFrontText = "<svg xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' id='SvgjsSvg1000' version='1.1' width='2400' height='3200' viewBox='311.00000000008 150 387.99999999984004 517.33333333312'><text id='SvgjsText1052' font-family='Source Sans Pro' fill='#808080' font-size='30' stroke-width='0' font-style='' font-weight='' text-decoration=' ' text-anchor='start' x='457.39119720458984' y='241.71535301208496'><tspan id='SvgjsTspan1056' dy='39' x='457.39119720458984'>adfasdf</tspan></text><defs id='SvgjsDefs1001'></defs></svg>";
            //Back:        <svg xmlns=\\\"http://www.w3.org/2000/svg\\\" xmlns:xlink=\\\"http://www.w3.org/1999/xlink\\\" id=\\\"SvgjsSvg1006\\\" version=\\\"1.1\\\" width=\\\"2400\\\" height=\\\"3200\\\" viewBox=\\\"311.00000000008 100 387.99999999984004 517.33333333312\\\"><g id=\\\"SvgjsG1052\\\" transform=\\\"scale(0.08399999999996445 0.08399999999996445) translate(3761.9047619073062 1569.8412698418072)\\\"><image id=\\\"SvgjsImage1053\\\" xlink:href=\\\"__dataURI:0__\\\" width=\\\"4500\\\" height=\\\"5400\\\"></image></g><defs id=\\\"SvgjsDefs1007\\\"></defs></svg>
            //Front:       "<svg xmlns=\\\"http://www.w3.org/2000/svg\\\" xmlns:xlink=\\\"http://www.w3.org/1999/xlink\\\" id=\\\"SvgjsSvg1000\\\" version=\\\"1.1\\\" width=\\\"2400\\\" height=\\\"3200\\\" viewBox=\\\"311.00000000008 150 387.99999999984004 517.33333333312\\\"><g id=\\\"SvgjsG1052\\\" transform=\\\"scale(0.08399999999996445 0.08399999999996445) translate(3761.9047619073062 2165.0793650801543)\\\"><image id=\\\"SvgjsImage1053\\\" xlink:href=\\\"__dataURI:0__\\\" width=\\\"4500\\\" height=\\\"5400\\\"></image></g><defs id=\\\"SvgjsDefs1001\\\"></defs></svg>"
            var strBack = "";
            var strFront = "";
            if (frontbackImage == "B")
                strBack = "<svg xmlns=\\\"http://www.w3.org/2000/svg\\\" xmlns:xlink=\\\"http://www.w3.org/1999/xlink\\\" id=\\\"SvgjsSvg1006\\\" version=\\\"1.1\\\" width=\\\"2400\\\" height=\\\"3200\\\" viewBox=\\\"311.00000000008 100 387.99999999984004 517.33333333312\\\"><g id=\\\"SvgjsG1052\\\" transform=\\\"scale(0.08399999999996445 0.08399999999996445) translate(3761.9047619073062 1569.8412698418072)\\\"><image id=\\\"SvgjsImage1053\\\" xlink:href=\\\"__dataURI:0__\\\" width=\\\"4500\\\" height=\\\"5400\\\"></image></g><defs id=\\\"SvgjsDefs1007\\\"></defs></svg>";
            else
                strFront = "<svg xmlns=\\\"http://www.w3.org/2000/svg\\\" xmlns:xlink=\\\"http://www.w3.org/1999/xlink\\\" id=\\\"SvgjsSvg1000\\\" version=\\\"1.1\\\" width=\\\"2400\\\" height=\\\"3200\\\" viewBox=\\\"311.00000000008 150 387.99999999984004 517.33333333312\\\"><g id=\\\"SvgjsG1052\\\" transform=\\\"scale(0.08399999999996445 0.08399999999996445) translate(3761.9047619073062 2165.0793650801543)\\\"><image id=\\\"SvgjsImage1053\\\" xlink:href=\\\"__dataURI:0__\\\" width=\\\"4500\\\" height=\\\"5400\\\"></image></g><defs id=\\\"SvgjsDefs1001\\\"></defs></svg>";

            var dataToSend = "";
            dataToSend = "{";
            dataToSend += "\"ArtOwnerID\":\"0\",\"IAgree\":\"true\"";
            dataToSend += ",\"Title\":\"" + title + "\"";
            dataToSend += ",\"Category\":\"" + category + "\"";
            dataToSend += ",\"Description\":\"" + description + "\"";
            dataToSend += ",\"Collections\":\"" + collection + "\"";
            dataToSend += ",\"Keywords\": [" + keyword + "]";
            dataToSend += ",\"imageFront\":\"" + strFront + "\"";
            dataToSend += ",\"imageBack\":\"" + strBack + "\"";
            dataToSend += ",\"types\":" + themes;
            dataToSend += ",\"images\":[{\"id\":\"__dataURI: 0__\",\"uri\":\"data:image/png;base64," + imgBase64 + "\"}]";
            dataToSend += "}";

            Dictionary<string, object> dataObj = new Dictionary<string, object>();
            dataObj.Add("Title", title);
            dataObj.Add("Category", category);
            dataObj.Add("Description", description);
            dataObj.Add("Collections", collection);
            dataObj.Add("Keywords", dr["Keyword"].ToString().Trim());
            dataObj.Add("imageFront", strFront);
            dataObj.Add("imageBack", strBack);
            dataObj.Add("images", imgBase64);

            
            dataToSend = refixData2Send(dataObj);
            byte[] postDataBytes2 = Encoding.ASCII.GetBytes(dataToSend);
            string getUrl = "https://manager.sunfrogshirts.com/Designer/php/upload-handler.cfm";

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
            //getRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:49.0) Gecko/20100101 Firefox/49.0";
            //getRequest.Accept = "*/*";
            getRequest.Method = "POST";
            //getRequest.AllowWriteStreamBuffering = true;
            //getRequest.AllowAutoRedirect = true;
            //getRequest.Host = "manager.sunfrogshirts.com";
            //getRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //getRequest.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
            //getRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            getRequest.ContentType = "application/json; charset=UTF-8";
            getRequest.Referer = "https://manager.sunfrogshirts.com/Designer/";
            getRequest.CookieContainer = cookieContainer;
            getRequest.ContentLength = postDataBytes2.Length;
            getRequest.Headers.Add("Origin", "https://sunfrogshirts.com");
            getRequest.KeepAlive = true;

            using (StreamWriter sr = new StreamWriter(getRequest.GetRequestStream()))
            {
                sr.Write(dataToSend);
                //sr.Write(postDataBytes2, 0, postDataBytes2.Length);
                sr.Flush();
                sr.Close();
            }
            HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
            string sourceCode = "";
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                sourceCode = sr.ReadToEnd();
            }

            //Dictionary<string, object> dataUpload = PostDataAPI(getRequest, dataToSend);
            //if (int.Parse(dataUpload["status"].ToString()) == -1)
            //{
            //    CoreLibary.writeLogThread(lsBoxLog, "Step 2: " + dataUpload["data"].ToString(), 1);
            //}
            //sourceCode = dataUpload["data"].ToString();
            var obj = JObject.Parse(sourceCode);
            if (int.Parse(obj["result"].ToString()) == 0)
                CoreLibary.writeLogThread(lsBoxLog, "Collection:" + obj["collectionName"].ToString() + " - " + obj["description"].ToString(), 2);
            else
                CoreLibary.writeLogThread(lsBoxLog, "Uploaded " + obj["description"].ToString(), 1);
            //Chuyển hình sang folder Uploaded
            //moveImageUploaded(pathImage);
        }

        private string refixData2Send(Dictionary<string, object> dataObj)
        {
            string data2SendFromFile = Resources.data2Send;
            JObject obj = JObject.Parse(data2SendFromFile);

            obj["Title"] = dataObj["Title"].ToString();
            obj["Category"] = dataObj["Category"].ToString();
            obj["Description"] = dataObj["Description"].ToString();
            obj["Collections"] = dataObj["Collections"].ToString();
            //set keyworks
            var keyworks = dataObj["Keywords"].ToString().Split(',');
            JArray itemTag = (JArray)obj["Keywords"];
            foreach (string str in keyworks)
            {
                if (!string.IsNullOrEmpty(str))
                    itemTag.Add(str);
            }
            obj["imageFront"] = dataObj["imageFront"].ToString();
            obj["imageBack"] = dataObj["imageBack"].ToString();
            //types
            JArray item = (JArray)obj["types"];
            foreach (string itemThemes in getListThemeObject())
            {
                var x = JObject.Parse(itemThemes);
                item.Add(x);
            }

            obj["images"][0]["uri"] = "data:image/png;base64," + dataObj["images"].ToString();

            var data2Send = obj.ToString(Newtonsoft.Json.Formatting.None);
            return data2Send;
        }
        /// <summary>
        /// Add màu tương ứng với themes đã chọn
        /// </summary>
        /// <param name="lsColor">List color tương ứng</param>
        private void addListColor(List<ObjColor> lsColor)
        {
            ckListBox.Items.Clear();
            foreach (ObjColor item in lsColor)
            {
                ckListBox.Items.Add(item.Name);
            }
        }
        /// <summary>
        /// Chuyển hình đã upload lên Sunfrog vào folder khác
        /// </summary>
        /// <param name="fileName">đường dẫn hình hiện tại</param>
        private void moveImageUploaded(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string newFileName = dirSaveImageUploaded + Path.GetFileName(fileName);
                    File.Move(fileName, newFileName);
                }
            }
            catch (Exception ex)
            {
                CoreLibary.writeLogThread(lsBoxLog, ex.Message, 2);
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
                wRequest.ContentLength = postDataBytes.Length;
                wRequest.Headers.Add("Origin", "https://sunfrogshirts.com");
                using (StreamWriter sr = new StreamWriter(wRequest.GetRequestStream()))
                {
                    sr.Write(data2Send);
                    //sr.Write(postDataBytes, 0, postDataBytes.Length);
                    sr.Flush();
                    sr.Close();
                }

                using (HttpWebResponse wResponse = (HttpWebResponse)wRequest.GetResponse())
                {
                    foreach (Cookie cookie in wResponse.Cookies)
                    {
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
        #endregion
    }
}
