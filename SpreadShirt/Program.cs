using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace SpreadShirt
{
    class Program
    {
        public static String API_KEY = "...";
        public static String SECRET = "...";

        public static String UPLOAD_URL = "http://api.spreadshirt.net/api/v1/shops/100232707/designs";
        public static String UPLOAD_XML = "./src/main/resources/designupload/design.xml";
        public static String UPLOAD_IMAGE = "./src/main/resources/designupload/design.png";
        private static CookieContainer cookieContainer;

        static void Main(string[] args)
        {
            cookieContainer = new CookieContainer();


            string url = "https://www.spreadshirt.com/login";

            HttpWebRequest wRequest = (HttpWebRequest)WebRequest.Create(url);
            var cookieHeader = "";
            WebResponse resp = wRequest.GetResponse();
            cookieHeader = resp.Headers["Set-cookie"];

            string url2 = "https://www.spreadshirt.com/login";

            HttpWebRequest wRequest2 = (HttpWebRequest)WebRequest.Create(url2);
            wRequest.CookieContainer = cookieContainer;
            var cookieHeader2 = "";
            WebResponse resp2 = wRequest.GetResponse();
            cookieHeader = resp.Headers["Set-cookie"];
            var pageSource = "";
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                pageSource = sr.ReadToEnd();
            }


            string login = "https://www.spreadshirt.com/api/v1/sessions?mediaType=json";
            string data2Send = "{\"username\":\"lchoang1995@gmail.com\", \"password\":\"Thienan@111\", \"rememberMe\":false}";
            string rs2 = postData(login, data2Send);

            string url3 = " http://api.spreadshirt.net/api/v1/users/302721328/designs";
            string data = "";
            data += "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
            data += "<design xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns=\"http://api.spreadshirt.net\">";
            data += "<name> Photo Community - xy Design </name>";
            data += "<description> Photo Community - xy Design </description>";
            data += "</design>";

            string result = postXMLData(url3, data);
            Console.Write(result);
        }

        public static string postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        public static string postData(string destinationUrl, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            request.ContentType = "application/json;charset=utf-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }
    }
}
