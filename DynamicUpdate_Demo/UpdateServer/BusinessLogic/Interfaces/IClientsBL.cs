using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateServer.Entities;

namespace UpdateServer.BusinessLogic
{
    interface IClientsBL
    {
        IList<ClientInfo> GetActiveClients();

        List<Command> GetPendingCommands(ClientInfo client);

        List<Command> PullPendingCommands(ClientInfo client);

        void PushPendingCommands(ClientInfo client, Command cmd);

        void AddOrUpdateClient(ClientInfo client);
    }
}
