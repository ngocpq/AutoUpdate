using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace WS_Server_CShap
{
    public partial class FormServerMonitor : Form, IUpdaterWebSocketServiceListenner
    {
        public FormServerMonitor()
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

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (wssv != null && wssv.IsListening)
            {
                MessageBox.Show("Server already started");
                return;
            }

            string url = txtWssAddress.Text;
            wssv = GetWebSocketServer(url);
            wssv.Start();
            if (wssv.IsListening)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format("Listening on port {0}, and providing WebSocket services:", wssv.Port));
                foreach (var path in wssv.WebSocketServices.Paths)
                    sb.Append(String.Format(Environment.NewLine + "- {0}", path));
                richTxtMsg.Text += sb.ToString();
            }
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
            this.SessionManager = ws.GetSessionManager();
        }    

        void WriteLog(string msg)
        {
            string strOldText = (string) GetControlPropertyThreadSafe(richTxtMsg, "Text");
            string newText = msg + Environment.NewLine + strOldText;
            SetControlPropertyThreadSafe(richTxtMsg, "Text", newText);
        }

        public void OnMessage(UpdaterWebSocketService updaterWebSocketService, MessageEventArgs e)
        {
            string message;
            if (e.IsPing)
            {
                message = "Ping from client '" + updaterWebSocketService.ID + "' ";
                WriteLog(message);
            }
            else
            {
                if (e.IsBinary)
                    message = System.Text.Encoding.Default.GetString(e.RawData);
                else
                    message = e.Data;
                MessageBox.Show("Message from client: " + updaterWebSocketService.ID + Environment.NewLine + message);
            }
        }

        public void OnOpen(UpdaterWebSocketService updaterWebSocketService)
        {
            WriteLog("Client: '" + updaterWebSocketService.ID+"' connected");
        }

        public void OnClose(UpdaterWebSocketService updaterWebSocketService, CloseEventArgs e)
        {
            WriteLog("Client: '" + updaterWebSocketService.ID + "' disconnected: code = '"+e.Code+"', reason: '"+e.Reason+"'");            
        }

        public void OnError(UpdaterWebSocketService updaterWebSocketService, ErrorEventArgs e)
        {
            WriteLog("Error on Client: '" + updaterWebSocketService.ID + "', Error Message: " + e.Message);
        }

        private void FormServerMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShutdownServer();
        }

        void ShutdownServer()
        {
            this.SessionManager = null;
            if (wssv != null)
                wssv.Stop(CloseStatusCode.Normal, "Server shutdown");
            wssv = null;
        }
    }
}
