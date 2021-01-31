using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UpdateServer.Entities;

namespace UpdateServer.BusinessLogic.Interfaces
{
    public delegate void ActiveClientUpdatedHandler(ClientInfo client, object source);

    public delegate HttpListenerResponse RequestHandler(HttpListenerRequest request);
    public interface IUpdaterWebServer
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
        void Shutdown();
        
        
        string[] Prefixes { get; }
        string RootDirPath { get; set; }

        void SetRoute(string relativePath, RequestHandler requestHandler);

        event ActiveClientUpdatedHandler ActiveClientUpdated;

        IList<ClientInfo> ActiveClients { get; }
    }
}
