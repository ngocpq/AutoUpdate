using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace WS_Server_CShap
{
    public partial class FormUpdateManager : Form, IUpdaterWebSocketServiceListenner
    {
        public FormUpdateManager()
        {
            InitializeComponent();
        }

        WebSocketSessionManager SessionManager;

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        private delegate object GetControlPropertyThreadSafeDelegate(Control control, string propertyName);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                object[] para = new object[] { control, propertyName, propertyValue };
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), para);
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
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

        void SetServerStarted(bool connected)
        {
            //btnConnect.Enabled = !connected;
            SetControlPropertyThreadSafe(btnStartServer, "Enabled", !connected);

            //btnDisconnect.Enabled = connected;
            SetControlPropertyThreadSafe(btnStopServer, "Enabled", connected);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {            
            if (wssv != null && wssv.IsListening)
            {
                MessageBox.Show("Server already started");
                return;
            }
            if (txtServerAddress.Text.Trim().Length == 0)
            {
                MessageBox.Show("WebSocket address is empty");
                return;
            }

            string url = txtServerAddress.Text;
            wssv = GetWebSocketServer(url);
            wssv.Start();
            if (wssv.IsListening)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format("Listening on port {0}, and providing WebSocket services:", wssv.Port));
                foreach (var path in wssv.WebSocketServices.Paths)
                    sb.Append(String.Format(Environment.NewLine + "- {0}", path));
                WriteLog(sb.ToString());
            }
            SetServerStarted(true);
        }


        WebSocketServer wssv;

        WebSocketServer GetWebSocketServer(string url)
        {
            if (wssv != null && wssv.IsListening)
                return wssv;

            // Create a new instance of the WebSocketServer class.
            wssv = new WebSocketServer(url);
#if DEBUG
            // To change the logging level.
            wssv.Log.Level = LogLevel.Trace;
#endif
            wssv.WaitTime = TimeSpan.FromSeconds(10); // the waiting time for the response to the WebSocket Ping or Close.
            wssv.KeepClean = true; //remove the inactive sessions periodically.            

            // To provide the HTTP Authentication (Basic/Digest).            
            wssv.AuthenticationSchemes = AuthenticationSchemes.Basic;
            wssv.Realm = "WebSocket Realm";
            wssv.UserCredentialsFinder = id => {
                //TODO:
                return new NetworkCredential(id.Name, "password");
            };

            // Add the WebSocket services.
            wssv.AddWebSocketService<UpdaterWebSocketService>("/services", InitializeUpdaterWebSocketService);            

            return wssv;
        }

        private void InitializeUpdaterWebSocketService(UpdaterWebSocketService ws)
        {
            ws.Listenner = this;
            ws.EmitOnPing = true;
        }

        void WriteLog(string msg)
        {
            string strOldText = (string)GetControlPropertyThreadSafe(richTxtMsg, "Text");
            string newText = DateTime.Now.ToString("HH:mm:ss - ") + msg + Environment.NewLine +"======================="+Environment.NewLine+ strOldText;
            SetControlPropertyThreadSafe(richTxtMsg, "Text", newText);
        }

        public void OnMessage(UpdaterWebSocketService updaterWebSocketService, MessageEventArgs e)
        {            
            string message  = "Message from: " + updaterWebSocketService.ID + Environment.NewLine;
            if (e.IsPing)
            {
                message += "Ping from client '" + updaterWebSocketService.ID + "'";
                WriteLog(message);
            }
            else
            {
                if (e.IsBinary)
                    message += System.Text.Encoding.Default.GetString(e.RawData);
                else
                    message += e.Data;
                WriteLog(message);
            }
        }

        void UpdateMonitorGUI(WebSocketSessionManager sessionManager)
        {
            IEnumerable<IWebSocketSession> datasource;
            if (sessionManager != null)
            {
                datasource = sessionManager.Sessions;
            }
            else
                datasource = new List<IWebSocketSession>();
            SetControlPropertyThreadSafe(this.dataGridView1, "DataSource", datasource);
        }

        //void UpdateMonitorGUI(WebSocketSessionManager sessionManager)
        //{
        //    IEnumerable<string> datasource;
        //    if (sessionManager != null)
        //    {
        //        datasource = sessionManager.ActiveIDs;
        //    }
        //    else
        //        datasource = new List<string>();
        //    SetControlPropertyThreadSafe(this.dataGridView1, "DataSource", datasource);
        //}

        public void OnOpen(UpdaterWebSocketService ws)
        {
            WriteLog("Client: '" + ws.ID + "' connected");            
            if (this.SessionManager ==null)
                this.SessionManager = ws.GetSessionManager();
            UpdateMonitorGUI(this.SessionManager);
        }

        public void OnClose(UpdaterWebSocketService ws, CloseEventArgs e)
        {
            WriteLog("Client: '" + ws.ID + "' disconnected: code = '" + e.Code + "', reason: '" + e.Reason + "'");
            if (this.SessionManager == null)
                this.SessionManager = ws.GetSessionManager();
            UpdateMonitorGUI(this.SessionManager);
        }

        public void OnError(UpdaterWebSocketService ws, ErrorEventArgs e)
        {
            WriteLog("Error on Client: '" + ws.ID + "', Error Message: " + e.Message);
        }

        private void FormServerMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShutdownServer();
        }

        void ShutdownServer()
        {
            if (wssv != null)
                wssv.Stop(CloseStatusCode.Normal, "Server shutdown");
            wssv = null;

            this.SessionManager = null;
            UpdateMonitorGUI(null);
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            if (wssv != null)
                wssv.Stop(CloseStatusCode.Normal, "Server shutdown");
            this.SessionManager = null;
            UpdateMonitorGUI(null);
            SetServerStarted(false);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtSelectedClientsCount.Text = dataGridView1.SelectedRows.Count.ToString();
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {            
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("No client selected");
                return;
            }
            string command = txtCommand.Text.Trim();
            if (command.Length == 0)
            {
                MessageBox.Show("Please input command message");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Send to {0} clients: {1}", dataGridView1.SelectedRows.Count, Environment.NewLine));
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string sessionId;
                IWebSocketSession ws = row.DataBoundItem as IWebSocketSession;
                if (ws != null)
                    sessionId = ws.ID;
                else
                    sessionId = (string) row.DataBoundItem;
                sb.Append(String.Format("\t{0}) {1}{2}", ++count, sessionId, Environment.NewLine));
                this.SessionManager.SendTo(command, sessionId);                
            }
            sb.Append(String.Format("Command: {1}-----{1}{0}{1}-----", command, Environment.NewLine));
        }
    }
}
