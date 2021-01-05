using AutoUpdaterTest;
using Update.Utils;
using Bingo.Update;
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
    public partial class FormAppClient : Form
    {
        public FormAppClient()
        {
            InitializeComponent();
        }

        private static Random random = new Random();

        static string CurrentVersion = AsmUtils.GetCurrentVersion().ToString();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static string userId = RandomString(10);

        static BasicAuthentication BasicAuthXML = new BasicAuthentication(userId, "pass");

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
                try
                {
                    webClient.Headers["AppVersion"] = CurrentVersion;
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
                }catch(Exception ex)
                {
                    richTextBox1.Text += "Cannot check for update. Error: " + ex.Message;
                }
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
            txtUpdateServerUrl.Text = AppSettings.UpdateUrl;
            this.lableTimeInterval.DataBindings.Add("Text", this.trackbar, "Value");            
        }

        private void trackbar_ValueChanged(object sender, EventArgs e)
        {
            timerCheckUpdate.Interval = trackbar.Value;
        }
        
    }
}
