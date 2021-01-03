using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateServer
{
    class UpdaterServerView
    {
        private List<ClientInfo> _ConnectedClients=new List<ClientInfo>();
        public List<ClientInfo> ActiveClients { get { return _ConnectedClients; }}

        public void updateActiveClients(ClientInfo client)
        {
            Boolean found = false;
            foreach (ClientInfo c in this.ActiveClients)
            {
                if (c.SessionId == client.SessionId)
                {
                    c.LastActiveTime = DateTime.Now;
                    found = true;
                    break;
                }
            }
            if (!found)
                ActiveClients.Add(client);
        }
    }
}
