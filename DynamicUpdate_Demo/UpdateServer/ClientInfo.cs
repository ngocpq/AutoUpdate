using System;

namespace UpdateServer
{
    public class ClientInfo
    {
        public string UserId { get; set; }

        public string SessionId { get; set; }
        public DateTime LastActiveTime { get; set; }

        public string Version { get; set; }
    }
}