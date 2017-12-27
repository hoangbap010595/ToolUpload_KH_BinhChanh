using DevExpress.XtraEditors;
using ExcelDataReader;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadShirts
{
    public static class ApplicationLibary
    {
        //Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36
        public static string BROWSER_CHROME = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
       // public static string BROWSER_FIREFOX = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:49.0) Gecko/20100101 Firefox/49.0";
        public static string BROWSER_FIREFOX = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36";

        private static string ApplicationName = "SpreadShirt";
        private static string FolderUser = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + ApplicationName;

        public static string API_KEY = "1c711bf5-b82d-40de-bea6-435b5473cf9b";
        public static string SECRET = "fd9f23cc-2432-4a69-9dad-bbd57b7b9fdd";
        public static string Origin = "https://partner.spreadshirt.com";
        public static string ConvertImageToBase64(string path)
        {
            string base64String = string.Empty;
            if (!File.Exists(path))
                return base64String;
            // Convert Image to Base64
            using (Image img = Image.FromFile(path)) // Image Path from File Upload Controller
            {
                using (var memStream = new MemoryStream())
                {
                    img.Save(memStream, img.RawFormat);
                    byte[] imageBytes = memStream.ToArray();
                    // Convert byte[] to Base64 String
                    base64String = Convert.ToBase64String(imageBytes);
                }
                img.Dispose();
            }
            return base64String;
        }
        public static DataTable getDataExcelFromFileToDataTable(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    //Get all Table
                    DataTableCollection tables = result.Tables;
                    DataTable dt = tables[0];
                    return dt;
                }

            }
        }
        public static DataTable getDataExcelFromFileCSVToDataTable(string filePath)
        {
            return GetDataTabletFromCSVFile(filePath);
        }
        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)

        {

            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return csvData;

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                throw new Exception("Something's wrong. Please try again !");
            }
        }
        public static string readDataFromFile(string filePath)
        {
            var data = "";
            using(StreamReader reader = new StreamReader(filePath))
            {
                data = reader.ReadToEnd();
            }
            return data;
        }
        public static void writeLog(this ListBoxControl lsbox, string message, int status)
        {
            string space = "...................................." + (status == 1 ? "Done" : status == 2 ? "Faild" : "In Progress");
            string time = DateTime.Now.ToString("HH:mm:ss");
            lsbox.Items.Insert(0, "[" + time + "] " + message + space);
            lsbox.Refresh();
        }

        public static void writeLogThread(this ListBoxControl lsbox, string message, int status)
        {
            string space = "...................................." + (status == 1 ? "Done" : status == 2 ? "Faild" : "In Progress");
            lsbox.Invoke((MethodInvoker)delegate
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                lsbox.Items.Insert(0, "[" + time + "] " + message + space);
                lsbox.Refresh();
            });
        }
        public static void writeFileToResource(string templateName, string data)
        {
            var name = DateTime.Now.Ticks.ToString("x");
            string fileNameTheme = Path.Combine(FolderUser, "\\themesaved.txt");
            //if the file doesn't exist, create it
            if (!File.Exists(fileNameTheme))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileNameTheme));
                File.Create(fileNameTheme);
            }
            using (StreamWriter sw = File.AppendText(fileNameTheme))
            {
                sw.WriteLine(name + "|" + templateName);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            string fileName = Path.Combine(FolderUser, "\\" + name + ".txt");
            //if the file doesn't exist, create it
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine(data);
            }
        }
        public static string writeDataToFileText(string data)
        {
            var name = DateTime.Now.Ticks.ToString("x");
            string fileName = Path.Combine(FolderUser, "\\" + name + "_log.txt");
            //if the file doesn't exist, create it
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine(data);
            }
            return fileName;
        }
        public static void saveDataTableToFileCSV(string path, DataTable data)
        {
            try
            {
                string ex1 = Path.GetExtension(path);
                if (ex1 != ".csv")
                {
                    path = Path.GetDirectoryName(path) + "\\" + Path.GetFileName(path).Split('.')[0] + ".csv";
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                string text = "";
                foreach (DataColumn col in data.Columns)
                {
                    text += "," + col.ColumnName;
                }
                text += "\r\n";
                foreach (DataRow dr in data.Rows)
                {
                    text += "";
                    foreach (DataColumn col in data.Columns)
                    {
                        text += "," + dr[col].ToString().Replace("\n", "").Replace("\r", "").Trim();
                    }
                    text += "\r\n";
                }
                File.AppendAllText(path, text);
            }
            catch { }
        }
        public static string convertStringToJson(string text)
        {
            var result = "";
            var value = text.Split(',');
            foreach (string item in value)
            {
                if (item != "")
                    result += "\"" + item + "\",";
            }
            return result.TrimEnd(',');
        }

        /// <summary>
        /// take any string and encrypt it using SHA1 then
        /// return the encrypted data
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public static string GetSHA1HashData(string data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] keyByte = Encoding.UTF8.GetBytes(SECRET);
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
        public static string sha1Generate(string data, string secretAccessKey)
        {
            String rs = "";
            byte[] keyByte = Encoding.UTF8.GetBytes(secretAccessKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(data);
            using (var hmacsha256 = new HMACSHA1(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                for (int i = 0; i < hashmessage.Length; i++)
                {
                    byte bit = hashmessage[i];
                    rs += bit.ToString("x2");
                }
            }
            return rs;
        }
        public static string getTimeStamp()
        {
            int unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return (unixTimestamp * 1000).ToString();
        }
        public static string encodeURL(string url, string defaultParam = "", string method = "POST", string locale = "", string mediaType = "", string sessionId = "", string time = "")
        {

            string t_url = url.Split('?')[0];
            string t_time = time == "" ? getTimeStamp() : time;
            string t_data = method + " " + t_url + " " + t_time;
            //string t_sig = GetSHA1HashData(t_data);
            string t_sig = sha1Generate(t_data, SECRET);

            int index = t_url.IndexOf('?');
            var newUrl = "";// t_url;
            if (index == -1)
                newUrl += "?";
            else
                newUrl += "&";
            newUrl += defaultParam == "" ? "" : defaultParam + "&";
            newUrl += "apiKey=" + API_KEY;
            if (locale != "")
                newUrl += "&locale=" + locale;//us_US
            if (mediaType != "")
                newUrl += "&mediaType=" + mediaType;//json
            newUrl += "&sig=" + t_sig + "&time=" + t_time;
            if (sessionId != "")
                newUrl += "&sessionId=" + sessionId;
            return t_url + newUrl;
        }

    }
}
