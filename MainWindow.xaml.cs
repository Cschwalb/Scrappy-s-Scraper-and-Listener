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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EventTrigger_Click(object sender, RoutedEventArgs e)
        {
            string sWebsite = "https://google.com";
            HttpWebRequest hFirstRequest = (HttpWebRequest)WebRequest.Create(sWebsite);
            HttpWebResponse hRes = (HttpWebResponse)hFirstRequest.GetResponse();
            Stream resourceStream = hRes.GetResponseStream();
            string tempString = null;
            int nCount = 0;
            do
            {
                byte[] bByteBuffer = new byte[1024];
                nCount = resourceStream.Read(bByteBuffer, 0, bByteBuffer.Length);
                if(nCount == 0)
                {
                    MessageBox.Show("No count on buffer read!");
                }
                else
                {
                    tempString = Encoding.ASCII.GetString(bByteBuffer, 0, nCount);
                    MessageBox.Show(tempString, "It worked...");
                }
            } while (nCount > 0); // read data until death do us part...
        }
    }
}
