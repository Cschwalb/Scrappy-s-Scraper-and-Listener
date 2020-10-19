using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebScraper
{
    class Utility
    {
        public static string parseData(string sData, string sKey, string sType = "JSON")
        {
            Console.WriteLine(string.Format("Data  has this many chars to it: {0}",
                sData.Length));
            // sType is going to be jSon if it isn't signed.
            string sTotal = "";
            string[] sDataField = sData.Split(' ');
            Console.WriteLine(string.Format("Data field has this many aspects to it: {0}",
                sDataField.Length));

            foreach (string token in sDataField)
            {
                if (token.Contains(sKey))
                {
                    string sTemp = token + "\n";
                    sTotal += sTemp;
                    Console.WriteLine("[+]Found one in parsing....");
                }
            }
            return sTotal;
        }
        public static void ExecTouch(string sWebsite)
        {
            System.Net.WebClient webClient = new System.Net.WebClient();
            //webClient.Document.GetElementsByTagName("add");
            const string strUrl = "https://www.streetwearofficial.com/collections/fresh-arrivals/products/saucony-jazz-dst-rose-blue?variant=36704875380904";
            byte[] reqHTML;
            reqHTML = webClient.DownloadData(strUrl);
            UTF8Encoding objUTF8 = new UTF8Encoding();
            MessageBox.Show(objUTF8.GetString(reqHTML));
        }

        public static void readDoc(string sData)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlDocument loadedPage = new HtmlAgilityPack.HtmlDocument();
            loadedPage.Load(sData);
            // we need to edit this...
            HtmlAgilityPack.HtmlNode[] nodes = loadedPage.DocumentNode.SelectNodes("//a").ToArray();
            foreach(HtmlAgilityPack.HtmlNode item in nodes)
            {
                Console.WriteLine(item.InnerHtml);
            }
        }
    }
}
