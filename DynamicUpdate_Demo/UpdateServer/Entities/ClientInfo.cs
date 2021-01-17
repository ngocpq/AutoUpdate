using System;
using System.Collections.Generic;

namespace UpdateServer.Entities
{
    public class ClientInfo
    {
        public string UserId { get; set; }

        public string SessionId { get; set; }
        public DateTime LastActiveTime { get; set; }

        public string Version { get; set; }

        public int PendingCommandsCount
        {
            get { return PendingCommands.Count; }
        }

        private List<Command> _PendingCommands;
        public List<Command> PendingCommands
        {
            get
            {
                if (_PendingCommands == null)
                    _PendingCommands = new List<Command>();
                return _PendingCommands;
            }
            protected set { _PendingCommands = value; }
        }

        public void AddCommand(Command cmd)
        {
            PendingCommands.Add(cmd);
        }
    }
}