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

        MyWebClient webClient = null;
        string SessionId = null;
        string URL_ServerCommands = "http://localhost:8080/commands";

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


        private void btnGetServerCommands_Click(object sender, EventArgs e)
        {
            timerCheckUpdate.Enabled = !timerCheckUpdate.Enabled;
            btnGetServerCommands.Text = timerCheckUpdate.Enabled ? "Stop Get Commands" : "Start Get Commands";
        }

        void checkUpdate(string url)
        {
            Uri BaseUri = new Uri(url);

            using (MyWebClient client = GetWebClient(BaseUri, BasicAuthXML))
            {
                try
                {                    
                    string xml = client.DownloadString(BaseUri);
                    richTextBox1.Text += xml;
                    richTextBox1.Text += Environment.NewLine + "-------------------------" + Environment.NewLine;
                    RichTextBoxRollToEnd();
                    if (client.ResponseHeaders["SESSION_ID"]!=null)
                        SessionId = client.ResponseHeaders["SESSION_ID"];                    
                }catch(Exception ex)
                {
                    richTextBox1.Text += "Cannot check for update. Error: " + ex.Message;
                }
            }
        }

        void RichTextBoxRollToEnd()
        {   // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();

        }
        void checkServerCommands()
        {
            string url = URL_ServerCommands;
            Uri BaseUri = new Uri(url);

            using (MyWebClient client = GetWebClient(BaseUri, BasicAuthXML))
            {
                try
                {
                    string xml = client.DownloadString(BaseUri);

                    richTextBox1.Text += xml;
                    richTextBox1.Text += Environment.NewLine + "-------------------------" + Environment.NewLine;
                    RichTextBoxRollToEnd();
                    if (client.ResponseHeaders["SESSION_ID"] != null)
                        SessionId = client.ResponseHeaders["SESSION_ID"];
                }
                catch (Exception ex)
                {
                    richTextBox1.Text += "Cannot check for update. Error: " + ex.Message;
                }
            }
        }

        internal MyWebClient GetWebClient(Uri uri, IAuthentication basicAuthentication)
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
            }

            if (SessionId != null)
                webClient.Headers["SESSION_ID"] = SessionId;
            webClient.Headers[HttpRequestHeader.UserAgent] = HttpUserAgent;
            webClient.Headers["AppVersion"] = CurrentVersion;
            return webClient;
        }

        private void timerCheckUpdate_Tick(object sender, EventArgs e)
        {
            checkServerCommands();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //txtUpdateServerUrl.Text = AppSettings.UpdateUrl;
            this.lableTimeInterval.DataBindings.Add("Text", this.trackbar, "Value");            
        }

        private void trackbar_ValueChanged(object sender, EventArgs e)
        {
            timerCheckUpdate.Interval = trackbar.Value;
        }
        
    }
}
