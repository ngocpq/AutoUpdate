using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WebSocketSharp;

namespace WS_Client_CShap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        void WriteLog(string msg)
        {
            string strOldText = (string)GetControlPropertyThreadSafe(richTxtMsg, "Text");
            string newText = DateTime.Now.ToString("HH:mm:ss - ") + msg + Environment.NewLine + "=======================" + Environment.NewLine + strOldText;
            SetControlPropertyThreadSafe(richTxtMsg, "Text", newText);
        }


        WebSocket wsClient = null;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        string userId = RandomString(10);
        string password = "password";

        WebSocket GetWebSocketClient(string url)
        {
            var ws = new WebSocket(url);
            ws.SetCredentials(userId, password, true);
            ws.OnMessage += Ws_OnMessage;            
            ws.OnError += Ws_OnError;
            ws.OnClose += Ws_OnClose;
            ws.Connect();
            return ws;
        }

        private void btnConnect_Click(object objSender, EventArgs eArgs)
        {
            if (wsClient != null && wsClient.IsAlive)
            {
                MessageBox.Show("Already connected to " + wsClient.Url.ToString());
                return;
            }
            string url = txtWssAddress.Text;
            wsClient = GetWebSocketClient(url);
            if (wsClient.IsAlive)
                WriteLog("Connected to "+ wsClient.Url.ToString());

            if (wsClient!=null && wsClient.IsAlive)
                SetWSConnected(true);
        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            WriteLog(String.Format("Ws_OnClose: code: {0}, reason: {1}", e.Code,e.Reason));
            wsClient = null;            
            SetWSConnected(false);
        }

        void SetWSConnected(bool connected)
        {
            //btnConnect.Enabled = !connected;
            SetControlPropertyThreadSafe(btnConnect, "Enabled", !connected);
            
            //btnDisconnect.Enabled = connected;
            SetControlPropertyThreadSafe(btnDisconnect, "Enabled", connected);
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            WriteLog("Ws_OnError: " + e.Message);
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            WriteLog("Server says: " + e.Data);            
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (wsClient != null && wsClient.IsAlive)
            {
                wsClient.Close(CloseStatusCode.Normal, "client closed");
                wsClient = null;

                SetWSConnected(false);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (wsClient == null || !wsClient.IsAlive)
            {
                MessageBox.Show("Please connect to server first!");
                return;
            }
            string data = txtMsgData.Text;
            wsClient.Send(data);
            WriteLog("Sent:" + Environment.NewLine + data);
        }
    }
}
