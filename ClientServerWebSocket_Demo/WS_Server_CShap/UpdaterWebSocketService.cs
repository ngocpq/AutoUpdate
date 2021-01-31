using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WS_Server_CShap
{
    public class UpdaterWebSocketService : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private string _prefix;

        public UpdaterWebSocketService()
          : this(null)
        {
        }

        public UpdaterWebSocketService(string prefix)
        {
            _prefix = !prefix.IsNullOrEmpty() ? prefix : "anonym#";
        }


        public IUpdaterWebSocketServiceListenner Listenner { get; set; }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            //Sessions.Broadcast(String.Format("{0} got logged off...", _name));
            if (this.Listenner != null)
            {
                this.Listenner.OnClose(this, e);
            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //Sessions.Broadcast(String.Format("{0}: {1}", _name, e.Data));
            if (this.Listenner != null)
            {
                this.Listenner.OnMessage(this, e);
            }
        }
        protected override void OnError(ErrorEventArgs e)
        {
            if (this.Listenner != null)
            {
                this.Listenner.OnError(this, e);
            }
        }
        protected override void OnOpen()
        {
            if (this.Listenner != null)
            {
                this.Listenner.OnOpen(this);
            }
        }

        public WebSocketSessionManager GetSessionManager()
        {
            return this.Sessions;
        }
    }
}