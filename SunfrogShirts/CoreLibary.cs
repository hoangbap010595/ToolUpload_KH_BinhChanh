using DevExpress.XtraEditors;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SunfrogShirts
{
    public static class CoreLibary
    {
        //DataJSon
        //{
        //  "ArtOwnerID":0
        //  ,"IAgree":true
        //  ,"Title":"Good for boy"
        //  ,"Category":"76"
        //  ,"Description":"THis for..."
        //  ,"Collections":""
        //  ,"Keywords":["boy","man"]
        //  ,"imageFront":"<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" id=\"SvgjsSvg1000\" version=\"1.1\" width=\"2400\" height=\"3200\" viewBox=\"311.00000000008 150 387.99999999984004 517.33333333312\"><g id=\"SvgjsG1052\" transform=\"scale(0.08399999999996445 0.08399999999996445) translate(3761.9047619073062 2165.0793650801543)\"><image id=\"SvgjsImage1053\" xlink:href=\"__dataURI:0__\" width=\"4500\" height=\"5400\"></image></g><defs id=\"SvgjsDefs1001\"></defs></svg>"
        //  ,"imageBack":"<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" id=\"SvgjsSvg1006\" version=\"1.1\" width=\"2400\" height=\"3200\" viewBox=\"311.00000000008 100 387.99999999984004 517.33333333312\"><g id=\"SvgjsG1052\" transform=\"scale(0.08399999999996445 0.08399999999996445) translate(3761.9047619073062 1569.8412698418072)\"><image id=\"SvgjsImage1053\" xlink:href=\"__dataURI:0__\" width=\"4500\" height=\"5400\"></image></g><defs id=\"SvgjsDefs1007\"></defs></svg>"
        //  ,"types":[
        //      {"id":8,"name":"Guys Tee","price":19,"colors":["Orange","Yellow"]}
        //      ,{"id":19,"name":"Hoodie","price":34,"colors":["White","Green"]}
        //  ]
        //  ,"images":[{"id":"__dataURI:0__","uri":"data:image/png;base64,"}]}

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
    }
}
