using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static BasicAuthentication BasicAuthXML = new BasicAuthentication("User", "pass");

        public static IWebProxy Proxy = null;
        private static string HttpUserAgent="AutoUpdaterClientAgent";
        private static NetworkCredential FtpCredentials = new NetworkCredential("user","pass");

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            string AppCastURL = txtUpdateServerUrl.Text;
            Uri BaseUri = new Uri(AppCastURL);

            checkUpdate(AppCastURL);
            
            MessageBox.Show("CheckUpdate finished");            
        }


        private void btnCheckUpdateAuto_Click(object sender, EventArgs e)
        {
            timerCheckUpdate.Enabled = !timerCheckUpdate.Enabled;
            btnCheckUpdateAuto.Text = timerCheckUpdate.Enabled ? "Stop Auto Check Update" : "Start auto check update";
        }

        void checkUpdate(string url)
        {            
            Uri BaseUri = new Uri(url);

            using (MyWebClient client = GetWebClient(BaseUri, BasicAuthXML))
            {
                string xml = client.DownloadString(BaseUri);

                richTextBox1.Text += xml;
                richTextBox1.Text += Environment.NewLine + "-------------------------" + Environment.NewLine;
                //if (ParseUpdateInfoEvent == null)
                //{
                //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(UpdateInfoEventArgs));
                //    XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xml)) { XmlResolver = null };
                //    args = (UpdateInfoEventArgs)xmlSerializer.Deserialize(xmlTextReader);
                //}
                //else
                //{
                //    ParseUpdateInfoEventArgs parseArgs = new ParseUpdateInfoEventArgs(xml);
                //    ParseUpdateInfoEvent(parseArgs);
                //    args = parseArgs.UpdateInfo;
                //}
            }
        }

        static MyWebClient webClient = null;
        internal static MyWebClient GetWebClient(Uri uri, IAuthentication basicAuthentication)
        {
            if (webClient == null)
                webClient = new MyWebClient
                {
                    CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                };

            if (Proxy != null)
            {
                webClient.Proxy = Proxy;
            }

            if (uri.Scheme.Equals(Uri.UriSchemeFtp))
            {
                webClient.Credentials = FtpCredentials;
            }
            else
            {
                basicAuthentication?.Apply(ref webClient);

                webClient.Headers[HttpRequestHeader.UserAgent] = HttpUserAgent;
            }

            return webClient;
        }

        private void timerCheckUpdate_Tick(object sender, EventArgs e)
        {
            checkUpdate(txtUpdateServerUrl.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.lableTimeInterval.DataBindings.Add("Text", this.trackbar, "Value");            
        }

        private void trackbar_ValueChanged(object sender, EventArgs e)
        {
            timerCheckUpdate.Interval = trackbar.Value;
        }

        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text.Trim().Length==0)
            {
                MessageBox.Show("Username is not entered!");
                return;
            }
            
            BasicAuthXML = new BasicAuthentication(txtUsername.Text, textBox2.Text);
            MessageBox.Show("Loggin successful");
        }
    }
}
