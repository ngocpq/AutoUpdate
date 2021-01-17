using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateServer.Entities;

namespace UpdateServer.DataAccess.MockDB
{
    internal class MockDatabase
    {
        static private MockDatabase _Data;
        public static MockDatabase Instance
        {
            get
            {
                if (_Data == null)
                    _Data = new MockDatabase();
                return _Data;
            }
        }

        public MockDatabase()
        {
            Clients = new List<ClientInfo>();

            Commands = new List<Command>();
        }

        public List<ClientInfo> Clients { get; set; }
        public List<Command> Commands { get; set; }
    }
}
