using System.Collections.Generic;
using UpdateServer.Entities;

namespace UpdateServer.BusinessLogic.Implements
{
    public class ClientsBL : IClientsBL
    {
        
        DataAccess.Interfaces.IClientsDA clientsDA = DataAccess.DAFactory.Instance.GetClientsDA();

        public void AddOrUpdateClient(ClientInfo client)
        {
            ClientInfo oldInfo = clientsDA.FindBySessionId(client.SessionId);
            if (oldInfo == null)
                clientsDA.AddClient(client);
            else
            {                
                lock (oldInfo)
                {
                    oldInfo.UserId = client.UserId;
                    oldInfo.LastActiveTime = client.LastActiveTime;
                    oldInfo.Version = client.Version;                    
                }
            }
        }

        public IList<ClientInfo> GetActiveClients()
        {
            //TODO: filter by active time
            return clientsDA.GetClients();
        }

        public List<Command> GetPendingCommands(ClientInfo client)
        {
            ClientInfo info= clientsDA.FindBySessionId(client.SessionId);
            if (info ==null)
                return new List<Command>();
            return new List<Command>(info.PendingCommands);
        }

        public List<Command> PullPendingCommands(ClientInfo client)
        {            
            List<Command> result;
            ClientInfo info = clientsDA.FindBySessionId(client.SessionId);
            if (info == null)
                result = new List<Command>();
            else
            {
                lock (info.PendingCommands)
                {
                    result = new List<Command>(info.PendingCommands);
                    info.PendingCommands.Clear();
                }                
            }
            return result;
        }

        public void PushPendingCommands(ClientInfo client, Command cmd)
        {
            ClientInfo info = clientsDA.FindBySessionId(client.SessionId);
            if (info == null)
                return;
            
            lock (info.PendingCommands)
            {
                info.PendingCommands.Add(cmd);
            }
            
        }
    }

}