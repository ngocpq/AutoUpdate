using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace UpdateServer
{
    public class UpdaterWebServer : WebServer
    {
        long _TimeOut = 30 * 1000; //30 seconds
        private List<ClientInfo> _ConnectedClients = new List<ClientInfo>();
        public List<ClientInfo> ActiveClients
        {
            get
            {
                //return _ConnectedClients.Where(x => x.LastActiveTime.AddSeconds(_TimeOut) < DateTime.Now);
                return _ConnectedClients;
            }
        }

        string UpdateInfoFilePath = "/VersionInfo".ToLower();
        
        public UpdaterWebServer(string rootDir, string prefixes) : this(rootDir,new string[] { prefixes })
        {
        }
        public UpdaterWebServer(string rootDir,string[] prefixes) : base(prefixes)
        {
            this.RootDir = rootDir;
        }

        public delegate void ActiveClientUpdatedHandler(ClientInfo client, object source);
        public event ActiveClientUpdatedHandler ActiveClientUpdated;

        public delegate void HttpRequestReceivedEventHandler(HttpListenerRequest request, object source);
        public event HttpRequestReceivedEventHandler HttpRequestListeners;

        public void updateActiveClients(ClientInfo client)
        {
            ClientInfo oldInfo = null;
            foreach (ClientInfo c in this._ConnectedClients)
            {
                if (c.UserId == client.UserId)
                {
                    oldInfo = c;
                    break;
                }
            }
            if (oldInfo == null)
                _ConnectedClients.Add(client);
            else
            {
                lock (oldInfo)
                {
                    oldInfo.LastActiveTime = DateTime.Now;
                    oldInfo.Version = client.Version;
                    oldInfo.SessionId = client.SessionId;
                }                
            }
            if (ActiveClientUpdated != null)
                ActiveClientUpdated(client, this);
        }

        ClientInfo getClientInfoFromRequest(HttpListenerRequest request)
        {
            if (request.UserAgent != "AutoUpdaterClientAgent")
                return null;

            ClientInfo client = new ClientInfo();
            client.UserId = request.Headers["Username"];
            if (client.UserId == null)
                client.UserId = "Unknown";

            client.SessionId = request.RequestTraceIdentifier.ToString();
            client.Version = request.Headers["AppVersion"];
            client.LastActiveTime = DateTime.Now;
            return client;
        }

        protected override HttpListenerResponse WriteResponse(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (this.HttpRequestListeners != null)
            {
                this.HttpRequestListeners(context.Request, context);
            }

            ClientInfo client = getClientInfoFromRequest(request);
            if (client != null)
            {
                updateActiveClients(client);
            }

            if (request.Url.AbsolutePath.ToLower().Equals(UpdateInfoFilePath))
            {                
                string updateInfoXml = this.GetVersionInfoForUser(client);
                byte[] page = Encoding.UTF8.GetBytes(updateInfoXml);

                response.ContentLength64 = page.Length;
                Stream output = response.OutputStream;
                try
                {
                    output.Write(page, 0, page.Length);
                }
                finally
                {
                    output.Close();
                }
            }
            else
            {
                string requestedFile = request.Url.AbsolutePath.Substring(1);                
                
                string filePath = Path.Combine(RootDir, requestedFile);
                byte[] page;
                if (File.Exists(filePath))
                    page = GetFile(filePath);
                else
                    page = Encoding.UTF8.GetBytes("File not found: "+filePath);

                response.ContentLength64 = page.Length;                
                Stream output = response.OutputStream;
                try
                {
                    output.Write(page, 0, page.Length);
                }
                finally
                {
                    output.Close();
                }
            }
            return response;
        }

        private string GetVersionInfoForUser(ClientInfo client)
        {
            //TODO: get by client
            
            return GetUpdateInfoFile();
        }

        private string GetUpdateInfoFile()
        {
            return "<item>< version > 2.0.0.0 </ version >< url >https://downloadURL/UpdatedFiles.zip</url></ item > ";
        }
    }
}