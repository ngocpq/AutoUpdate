using System.Collections.Generic;
using UpdateServer.Entities;

namespace UpdateServer.DataAccess.Interfaces
{
    public interface IClientsDA
    {
        ClientInfo FindByUserId(string userId);
        ClientInfo FindBySessionId(string sessionId);
        List<ClientInfo> GetClients();
        int AddClient(ClientInfo client);
        int UpdateClient(ClientInfo client);
    }
}