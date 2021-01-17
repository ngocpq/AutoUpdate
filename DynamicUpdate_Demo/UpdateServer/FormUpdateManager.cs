using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateServer.Entities;

namespace UpdateServer
{
    public partial class FormUpdateManager : Form
    {
        BusinessLogic.IClientsBL clientsBL = BusinessLogic.BLFactory.Instance.GetClientsBL();

        public FormUpdateManager()
        {
            InitializeComponent();
        }        

        private delegate void SetControlPropertyThreadSafeDelegate(Control control,string propertyName,object propertyValue);

        private delegate object GetControlPropertyThreadSafeDelegate(Control control, string propertyName);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                object[] para = new object[] { control, propertyName, propertyValue };
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe),para);
            }
            else
            {
                control.GetType().InvokeMember(propertyName,BindingFlags.SetProperty,null,control,new object[] { propertyValue });
            }
        }

        private delegate void InvokeControlMethodThreadSafeDelegate(Control control, string methodName, object[] parameter);

        public static void InvokeControlMethodThreadSafe(Control control, string methodName, object[] parameters)
        {
            if (control.InvokeRequired)
            {
                object[] para = new object[] { control, methodName, parameters };
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), para);
            }
            else
            {
                control.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, control, parameters);
            }
        }

        public static object GetControlPropertyThreadSafe(Control control, string propertyName)
        {
            if (control.InvokeRequired)
            {
                object[] para = new object[] { control, propertyName };
                return control.Invoke(new GetControlPropertyThreadSafeDelegate(GetControlPropertyThreadSafe), para);
            }
            else
            {
                return control.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, control, new object[] { });
            }
        }
       
        private void UpdateRequestedLog(HttpListenerRequest request, object source)
        {
            
            string strOldText = (string)GetControlPropertyThreadSafe(richTxtMsg, "Text");
            // Print out some info about the request
            StringBuilder sb = new StringBuilder(strOldText);
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("---- New request #: {0}, {1}---", ++requestCount, request.Url.ToString()));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\tMethod: {0}, UserHostName: {1}, UserAgent: {2}", request.HttpMethod, request.UserHostName,request.UserAgent));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\tPath: {0}", request.Url.AbsolutePath));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\tIsAuthenticated: {0}, IsSSL: {1}", request.IsAuthenticated,request.IsSecureConnection));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\tRequestTraceIdentifier: {0}", request.RequestTraceIdentifier));
            sb.Append(Environment.NewLine);            
            sb.Append(Environment.NewLine);

            SetControlPropertyThreadSafe(richTxtMsg, "Text", sb.ToString());

        }
        void RichTextBoxRollToEnd()
        {   // set the current caret position to the end
            richTxtMsg.SelectionStart = richTxtMsg.Text.Length;
            // scroll it automatically
            richTxtMsg.ScrollToCaret();

        }


        UpdaterWebServer updaterWebServer = null;
        private int requestCount;

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            String prefix = txtPrefix.Text;// "http://localhost:8080/";
            if (updaterWebServer == null)
            {
                String rootDir = txtBaseDir.Text;
                updaterWebServer = new UpdaterWebServer(rootDir,prefix);
            }
            if (!updaterWebServer.isRunning)
            {
                updaterWebServer.start();
                //ws.RequestHandler += this.ProcessRequest;
                updaterWebServer.HttpRequestListeners += this.UpdateRequestedLog;

            }
            richTxtMsg.Text+=Environment.NewLine+"Server is listenning to: "+ prefix;
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            if (updaterWebServer == null || !updaterWebServer.isRunning) { 
                MessageBox.Show("Server is not running");
                return;
            }

            updaterWebServer.Stop();
            MessageBox.Show("Server stopped");
        }

        private void btnShutDownServer_Click(object sender, EventArgs e)
        {
            if (updaterWebServer == null)
            {
                MessageBox.Show("Server is null");
                return;
            }

            updaterWebServer.shutdown();
            updaterWebServer = null;
            MessageBox.Show("Server shutdowned");
        }

        private void FormUpdateManager_Load(object sender, EventArgs e)
        {
            txtBaseDir.Text = AppDomain.CurrentDomain.BaseDirectory;
            String prefix = txtPrefix.Text;// "http://localhost:8080/";
            if (updaterWebServer == null)
            {
                String rootDir = txtBaseDir.Text;
                updaterWebServer = new UpdaterWebServer(rootDir,prefix);
            }
            if (!updaterWebServer.isRunning)
            {
                updaterWebServer.start();
                updaterWebServer.HttpRequestListeners += this.UpdateRequestedLog;
                updaterWebServer.ActiveClientUpdated += Ws_ActiveClientUpdated;
            }
            richTxtMsg.Text += Environment.NewLine + "Server is listenning to: " + prefix;
            
        }

        private void Ws_ActiveClientUpdated(ClientInfo client, object source)
        {
            //this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //RichTextBoxRollToEnd();
            this.clientInfoBindingSource.DataSource = clientsBL.GetActiveClients();
            this.clientInfoBindingSource.ResetBindings(true);
        }

        private void btnBaseDirBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBaseDir.Text = dlg.SelectedPath;
                updaterWebServer.RootDir = txtBaseDir.Text;
            }
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            if (txtCmdName.Text == null)
            {
                MessageBox.Show("Enter command name first!");
                return;
            }
            
            if (clientInfoBindingSource.Current == null)
                return;
            ClientInfo client = (ClientInfo)clientInfoBindingSource.Current;
            Command cmd = new Command(txtCmdName.Text);
            if (txtParamName.Text.Trim().Length > 0)
                cmd.SetParameter(txtParamName.Text, txtParamValue.Text);

            clientsBL.PushPendingCommands(client, cmd);
            MessageBox.Show("Command added");
        }
    }
}
