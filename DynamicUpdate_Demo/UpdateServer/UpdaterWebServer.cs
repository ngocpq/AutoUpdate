using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UpdateServer.BusinessLogic;
using UpdateServer.DataAccess;
using UpdateServer.Entities;

namespace UpdateServer
{
    public class UpdaterWebServer : WebServer
    {
        IClientsBL clientsBL = BLFactory.Instance.GetClientsBL();

        long _TimeOut = 30 * 1000; //30 seconds
        //private List<ClientInfo> _ConnectedClients = new List<ClientInfo>();
        //public List<ClientInfo> ActiveClients
        //{
        //    get
        //    {
        //        //return _ConnectedClients.Where(x => x.LastActiveTime.AddSeconds(_TimeOut) < DateTime.Now);
        //        return _ConnectedClients;
        //    }
        //}

        string URL_RequestUpdateInfo = "/VersionInfo".ToLower();
        string URL_ServerCommands = "/Commands".ToLower();

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
            clientsBL.AddOrUpdateClient(client);

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
            client.Version = request.Headers["AppVersion"];
            client.LastActiveTime = DateTime.Now;

            if (request.Cookies["SESSION_ID"] != null)
                client.SessionId = request.Cookies["SESSION_ID"].Value;
            else if (request.QueryString["SESSION_ID"] != null)
                client.SessionId = request.QueryString["SESSION_ID"];
            else if (request.Headers["SESSION_ID"]!=null)
                client.SessionId = request.Headers["SESSION_ID"];
            else //newly session
            {
                //Generate new session id
                client.SessionId = GenerateNewSessionID();
            }
            //client.SessionId  = request.RequestTraceIdentifier.ToString();

            return client;
        }

        static long SessionCount = 0;
        private static string GenerateNewSessionID()
        {
            SessionCount++;
            return SessionCount.ToString();
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
                response.Headers["SESSION_ID"] = client.SessionId;
                response.SetCookie(new Cookie("SESSION_ID", client.SessionId));
            }            

            if (IsRequestServerCommands(request))
            {
                List<Command> commands = clientsBL.PullPendingCommands(client);
                string responseMessage = BuildServerCommandsRespondMessage(commands);

                byte[] page = Encoding.UTF8.GetBytes(responseMessage);

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
            else if (IsRequestUpdateInfo(request))
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
                    page = Encoding.UTF8.GetBytes("File not found: " + filePath);

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

        private static string BuildServerCommandsRespondMessage(List<Command> commands)
        {
            if (commands.Count == 0)
                return "<Commands/>";

            StringBuilder sb = new StringBuilder();
            sb.Append("<Commands>");
            foreach (Command cmd in commands)
            {
                sb.Append("\n\t<Command Name=\"" + cmd.Name + "\">");
                foreach (string paraName in cmd.Parameters.Keys)
                {
                    string value = cmd.Parameters[paraName];
                    sb.Append(String.Format("\n\t\t<Para Name=\"{0}\">{1}</Para>", paraName, value));
                }
                sb.Append("\n\t</Command>");
            }
            sb.Append("</Commands>");
            string responseMessage = sb.ToString();
            return responseMessage;
        }

        private bool IsRequestUpdateInfo(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.ToLower().Equals(URL_RequestUpdateInfo);
        }
        
        private bool IsRequestServerCommands(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.ToLower().Equals(URL_ServerCommands);
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