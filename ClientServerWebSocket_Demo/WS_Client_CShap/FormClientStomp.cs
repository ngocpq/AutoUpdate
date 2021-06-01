using Newtonsoft.Json;
using StompHelper;
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

namespace WS_Client_CShap
{
    public partial class FormClientStomp : Form
    {
        public FormClientStomp()
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
            string newText = DateTime.Now.ToString("HH:mm:ss - ") + msg + Environment.NewLine + "=============" + Environment.NewLine + strOldText;
            SetControlPropertyThreadSafe(richTxtMsg, "Text", newText);
        }


        WebSocket wsClient = null;
        StompMessageSerializer serializer = new StompMessageSerializer();

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
            ws.OnOpen += Ws_OnOpen; ;
            ws.OnMessage += Ws_OnMessage;
            ws.OnError += Ws_OnError;
            ws.OnClose += Ws_OnClose;
            ws.Connect();
            return ws;
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
            WriteLog(String.Format("Ws_OnOpen: {0}", sender.ToString()));
            wsClient = sender as WebSocket;
            this.SendConnect(wsClient);
            this.DoSubscribe(wsClient);
        }

        void SendConnect(WebSocket ws)
        {
            var connect = new StompMessage("CONNECT");
            connect["accept-version"] = "1.2";
            connect["host"] = "";
            ws = ws as WebSocket;
            ws.Send(serializer.Serialize(connect));
        }
        private void DoSubscribe(WebSocket ws, string path, string id)
        {
            var sub = new StompMessage(StompFrame.SUBSCRIBE);            
            sub["id"] = id;
            sub["destination"] = path;
            ws.Send(serializer.Serialize(sub));
            string logMessage = "Subscribed to : " + path;
            WriteLog(logMessage);
        }

        private void DoSubscribe(WebSocket ws)
        {
            DoSubscribe(ws, "/sub/chat/room/80d9ffff-4349-4523-b9a4-d88d54d16c37-admin", "CShapAdmin");
            DoSubscribe(ws, "/sub/chat/room/80d9ffff-4349-4523-b9a4-d88d54d16c37", "CShapUser");            
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
                WriteLog("Connected to " + wsClient.Url.ToString());

            if (wsClient != null && wsClient.IsAlive)
                SetWSConnected(true);
        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            WriteLog(String.Format("Ws_OnClose: code: {0}, reason: {1}", e.Code, e.Reason));
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
            MessageBox.Show("Message from server: " + e.Data);
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

        void AddHeader(StompMessage message)
        {
            Dictionary<string, string> headerDict = new Dictionary<string, string>();

            string strHeader = txtHeader.Text.Trim();            
            foreach (string line in strHeader.Split(new string[] { "\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] arr = line.Split(new string[] { ":" }, 2 , StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length!= 2) {
                    throw new Exception("Header format error: "+line);
                }
                string key = arr[0].Trim();
                if (key == "")
                    throw new Exception("Header format error: " + line);
                string value = arr[1];                
                message[key] = value;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (wsClient == null || !wsClient.IsAlive)
            {
                MessageBox.Show("Please connect to server first!");
                return;
            }            
            var message = new StompMessage(StompFrame.MESSAGE, txtMsgData.Text);            
            message["destination"] = txtSendDest.Text;                        

            this.AddHeader(message);

            string data = serializer.Serialize(message);
            wsClient.Send(data);            
            WriteLog("Sent:" + Environment.NewLine + data);
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            if (wsClient == null || !wsClient.IsAlive)
            {
                MessageBox.Show("Please connect to server first!");
                return;
            }
            this.SendConnect(wsClient);            
            string subscribePath = txtSubscribeDest.Text;
            this.DoSubscribe(wsClient, subscribePath, "CShapClient");            
        }

        private void btnSendInfo_Click(object sender, EventArgs e)
        {
            if (wsClient == null || !wsClient.IsAlive)
            {
                MessageBox.Show("Please connect to server first!");
                return;
            }
            //setup subscribe info
            var content = new
            {
                type = "DEVICE",
                roomId = "80d9ffff-4349-4523-b9a4-d88d54d16c37-admin",
                message = new { admin_id = "admin2", user_id = "test5", version = "1.0.0.3" }
            };
            var message = new StompMessage(StompFrame.MESSAGE, JsonConvert.SerializeObject(content));            
            message["destination"] = "/pub/chat/message";
            string data = serializer.Serialize(message);
            wsClient.Send(data);
            WriteLog("Sent Info: /pub/chat/message" + Environment.NewLine + data);
        }
    }
}
