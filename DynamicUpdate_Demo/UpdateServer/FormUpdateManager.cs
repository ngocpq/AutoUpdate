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

namespace UpdateServer
{
    public partial class FormUpdateManager : Form
    {
        //public List<ClientInfo> ActiveClients = new List<ClientInfo>();

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

        public static HttpListener listener;        

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (listener != null)
                return;
            string url = txtPrefix.Text;// "http://localhost:8000/test";
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            txtMsg.Text+=String.Format("Listening for connections on {0}\n", url);

            listener.BeginGetContext(new AsyncCallback(ListenerCallbackMethod), listener);
        }

        delegate void HttpRequestProcessor(HttpListenerContext context, object source);

        event HttpRequestProcessor HttpRequestReceivedEvent;

        void UpdateConnectedClient(HttpListenerContext context, object source)
        {
            HttpListenerRequest request = context.Request;            
            HttpListenerResponse response = context.Response;

            //request.RequestTraceIdentifier.ToString
        }

        ClientInfo getClientInfoFromRequest(HttpListenerRequest request)
        {
            ClientInfo client = new ClientInfo();
            client.SessionId = request.RequestTraceIdentifier.ToString();
            client.Version = request.QueryString["version"];
            client.LastActiveTime = DateTime.Now;
            return client;
        }

        //void updateActiveClients(ClientInfo client)
        //{
        //    Boolean found = false;
        //    foreach(ClientInfo c  in this.ActiveClients){
        //        if (client.SessionId == client.SessionId)
        //        {
        //            c.LastActiveTime = DateTime.Now;
        //            found = true;
        //            break;
        //        }                    
        //    }
        //    if (!found)
        //        ActiveClients.Add(client);
        //    //this.clientInfoBindingSource.DataSource = this.ActiveClients;
        //    //this.clientInfoBindingSource.ResetBindings(false);
        //    SetControlPropertyThreadSafe(dataGridView1, "DataSource", this.ActiveClients);
        //}

        //UpdaterServerView updaterServer = new UpdaterServerView();

        private void UpdateRequestedLog(HttpListenerRequest request, object source)
        {
            
            string strOldText = (string)GetControlPropertyThreadSafe(txtMsg, "Text");
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

            sb.Append(String.Format("\tClient Count: {0}", this.updaterWebServer.ActiveClients.Count));
            sb.Append(Environment.NewLine);


            SetControlPropertyThreadSafe(txtMsg, "Text", sb.ToString());
            

        }

        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";

        void ProcessRequest(HttpListenerContext context, object source)
        {
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            string strOldText = (string)GetControlPropertyThreadSafe(txtMsg, "Text");

            // Print out some info about the request
            StringBuilder sb = new StringBuilder(strOldText);
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("---- Request #: {0}---", ++requestCount));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("{0}", request.Url.ToString()));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\t{0}", request.HttpMethod));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\tUserHostName: {0}",request.UserHostName));
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("\tUserAgent: {0}", request.UserAgent));
            sb.Append(Environment.NewLine);

            if ((request.HttpMethod == "POST") && (request.Url.AbsolutePath == "/shutdown"))
            {
                sb.AppendFormat("Shutdown requested");
                sb.Append(Environment.NewLine);
            }
            SetControlPropertyThreadSafe(txtMsg, "Text", sb.ToString());

            // Make sure we don't increment the page views counter if `favicon.ico` is requested
            if (request.Url.AbsolutePath != "/favicon.ico")
                pageViews += 1;

            // Write the response info
            string disableSubmit = "";
            byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
            response.ContentType = "text/html";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = data.LongLength;

            System.IO.Stream output = response.OutputStream;
            output.Write(data, 0, data.Length);
            // You must close the output stream.
            output.Close();
        }

        public void ListenerCallbackMethod(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            if (listener == null)
                return;
            if (listener.IsListening)
                listener.BeginGetContext(new AsyncCallback(ListenerCallbackMethod), listener);
            if (this.HttpRequestReceivedEvent != null)
            {
                if (listener.IsListening)
                {
                    HttpListenerContext context = listener.EndGetContext(result);
                    HttpRequestReceivedEvent(context, listener);
                }
            }
        }


       public void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            if (listener == null)
                return;
            if (listener.IsListening)
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);

            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            // Print out some info about the request
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Request #: {0}\n", ++requestCount));

            sb.Append(String.Format("Request #: {0}\n", ++requestCount));
            sb.Append(String.Format("{0}\n", request.Url.ToString()));
            sb.Append(String.Format("{0}\n", request.HttpMethod));
            sb.Append(String.Format(request.UserHostName));
            sb.Append(String.Format(request.UserAgent));
            sb.Append("\n");

            if ((request.HttpMethod == "POST") && (request.Url.AbsolutePath == "/shutdown"))
            {
                sb.AppendFormat("Shutdown requested\n");                
            }

            SetControlPropertyThreadSafe(txtMsg, "Text", sb.ToString());

            // Make sure we don't increment the page views counter if `favicon.ico` is requested
            if (request.Url.AbsolutePath != "/favicon.ico")
                pageViews += 1;

            // Write the response info
            string disableSubmit = "";
            byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
            response.ContentType = "text/html";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = data.LongLength;

            System.IO.Stream output = response.OutputStream;
            output.Write(data, 0, data.Length);
            // You must close the output stream.
            output.Close();
        }

        UpdaterWebServer updaterWebServer = null;
        private void button3_Click(object sender, EventArgs e)
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
            txtMsg.Text+=Environment.NewLine+"Server is listenning to: "+ prefix;
        }


        public static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY>My web page.<br>{0}   <p><input type='submit' value='Отправить'></BODY></HTML>", DateTime.Now);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (updaterWebServer == null || !updaterWebServer.isRunning) { 
                MessageBox.Show("Server is not running");
                return;
            }

            updaterWebServer.Stop();
            MessageBox.Show("Server stopped");
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void btnShutdownServer_Click(object sender, EventArgs e)
        {
            if (listener == null)
            {
                MessageBox.Show("Server is null");
                return;
            }
            listener.Close();
            MessageBox.Show("Server shutdowned ");
        }

        private void FormUpdateManager_Load(object sender, EventArgs e)
        {
            txtBaseDir.Text = AppDomain.CurrentDomain.BaseDirectory;

            //this.HttpRequestReceivedEvent += ProcessRequest;
            //this.updaterServer.updateActiveClients(new ClientInfo { SessionId = "client1", LastActiveTime = DateTime.Now, Version="Version 0" });
            //this.updaterWebServerBindingSource.DataSource = this.updaterServer;

            //this.button3_Click(null, null);

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
            txtMsg.Text += Environment.NewLine + "Server is listenning to: " + prefix;

            this.updaterWebServerBindingSource.DataSource = this.updaterWebServer;

        }

        private void Ws_ActiveClientUpdated(ClientInfo client, object source)
        {
            //this.timer1.Enabled = true;
            this.timer1.Start();
        }

        private void btnUpdateBinding_Click(object sender, EventArgs e)
        {
            this.updaterWebServerBindingSource.ResetBindings(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.updaterWebServerBindingSource.ResetBindings(true);            
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
    }
}
