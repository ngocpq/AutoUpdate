using System.Collections.Generic;
using UpdateServer.Entities;

namespace UpdateServer.DataAccess.MockDB
{
    internal class ClientsDA : Interfaces.IClientsDA
    {
        public ClientInfo FindByUserId(string userId)
        {
            foreach (ClientInfo c in MockDatabase.Instance.Clients)
            {
                if (c.UserId == userId)
                {
                    return c;
                }
            }
            return null;
        }
        public ClientInfo FindBySessionId(string sessionId)
        {
            foreach (ClientInfo c in MockDatabase.Instance.Clients)
            {
                if (c.SessionId == sessionId)
                {
                    return c;
                }
            }
            return null;
        }

        public int AddClient(ClientInfo client)
        {
            MockDatabase.Instance.Clients.Add(client);
            return 1;
        }

        public List<ClientInfo> GetClients()
        {
            return MockDatabase.Instance.Clients;
        }

        public int UpdateClient(ClientInfo client)
        {
            ClientInfo oldInfo = FindByUserId(client.UserId);

            if (oldInfo != null)
            {
                lock (oldInfo)
                {
                    oldInfo.LastActiveTime = client.LastActiveTime;
                    oldInfo.Version = client.Version;
                    oldInfo.SessionId = client.SessionId;
                }
                return 1;
            }
            return 0;
        }
    }
}