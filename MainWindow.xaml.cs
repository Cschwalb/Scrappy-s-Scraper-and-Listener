using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;

namespace WebScraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string sOfficial = "https://www.streetwearofficial.com/collections/fresh-arrivals/products/saucony-jazz-dst-rose-blue?variant=36704875380904";
        public MainWindow()
        {
            InitializeComponent();
        }

        public void getData(string sWebsiteChoice)
        {
            string sWebsite = sWebsiteChoice;
            CookieContainer cCookieContainer = new CookieContainer();
            HttpWebRequest hFirstRequest = hFirstRequest = (HttpWebRequest)WebRequest.Create(sWebsite);
            hFirstRequest.Method = "GET";
            hFirstRequest.CookieContainer = cCookieContainer;
            hFirstRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            hFirstRequest.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            HttpWebResponse hRes = (HttpWebResponse)hFirstRequest.GetResponse();
            Stream resourceStream = hRes.GetResponseStream();
            string tempString = null;
            int nCount = 0;
            do
            {
                byte[] bByteBuffer = new byte[2560000];
                nCount = resourceStream.Read(bByteBuffer, 0, bByteBuffer.Length);
                if (nCount == 0)
                {
                    continue;
                }
                else
                {
                    tempString = Encoding.ASCII.GetString(bByteBuffer, 0, nCount);
                    OutputBox.Text = tempString;
                }
            } while (nCount > 0); // read data until death do us part...
        }

        public string getDataString(string sWebsiteChoice)
        {
            string sWebsite = sWebsiteChoice;
            CookieContainer cCookieContainer = new CookieContainer();
            HttpWebRequest hFirstRequest = hFirstRequest = (HttpWebRequest)WebRequest.Create(sWebsite);
            hFirstRequest.Method = "GET";
            hFirstRequest.CookieContainer = cCookieContainer;
            hFirstRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            hFirstRequest.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            HttpWebResponse hRes = (HttpWebResponse)hFirstRequest.GetResponse();
            Stream resourceStream = hRes.GetResponseStream();
            string tempString = " ";
            int nCount = 0;
            do
            {
                byte[] bByteBuffer = new byte[2560000];
                nCount = resourceStream.Read(bByteBuffer, 0, bByteBuffer.Length);
                if (nCount == 0)
                {
                    continue;
                }
                else
                {
                    tempString += Encoding.ASCII.GetString(bByteBuffer, 0, nCount);
                }
            } while (nCount > 0); // read data until death do us part...
            return tempString;
        }
        private void EventTrigger_Click(object sender, RoutedEventArgs e)
        {
            this.OutputBox.Text = getDataString(sOfficial);
           
        }

        public int getDataCount(string sWebsiteChoice, int numberOfRuns, int nSeconds)
        {
            string sWebsite = sWebsiteChoice;
            CookieContainer cCookieContainer = new CookieContainer();
            HttpWebRequest hFirstRequest = hFirstRequest = (HttpWebRequest)WebRequest.Create(sWebsite);
            hFirstRequest.Method = "GET";
            hFirstRequest.CookieContainer = cCookieContainer;
            hFirstRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            hFirstRequest.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            HttpWebResponse hRes = (HttpWebResponse)hFirstRequest.GetResponse();
            Stream resourceStream = hRes.GetResponseStream();
            string tempString = null;
            int nCount = 0;
            int nCountOld = 0;
            int nTouched = 0;
            int nStart = 0;
            while (nStart < numberOfRuns)
            {
                Console.WriteLine("test");
                do
                {
                    byte[] bByteBuffer = new byte[2560000];
                    Console.WriteLine("Count original: {0} New Count {1}", nCountOld, nCount);
                    nCountOld = nCount;
                    nCount = resourceStream.Read(bByteBuffer, 0, bByteBuffer.Length);
                    if (nCount != nCountOld)
                    {
                        nTouched += 1;
                        Console.WriteLine("caleb found a file!");
                    }
                } while (nCount > 0); // read data until death do us part...
                System.Threading.Thread.Sleep(nSeconds * 1000);
                nStart++;
            }
            return nTouched -= 1;
        }

        private void Listen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("starting run!");
            int nEdited = getDataCount(sOfficial, 7, 8);
            MessageBox.Show(string.Format("edited this many times {0}", nEdited));
        }

        private void SearchRead_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.searchBox.Text);
            if(this.OutputBox.LineCount <= 0)
            {
                MessageBox.Show("cannot search string!  Populate Read first!");
            }
            else
            {
                if(this.OutputBox.Text.Contains(this.searchBox.Text))
                {
                    MessageBox.Show("Contains searched string!");
                    
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
        }

        private void parseThis_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = Utility.parseData(this.OutputBox.Text, this.searchBox.Text);
        }

        private void ReadHTML_Click(object sender, RoutedEventArgs e)
        {
            Utility.readDoc(getDataString(sOfficial));
        }
    }
}
