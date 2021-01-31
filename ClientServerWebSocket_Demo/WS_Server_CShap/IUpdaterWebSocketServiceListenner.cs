using WebSocketSharp;

namespace WS_Server_CShap
{
    public interface IUpdaterWebSocketServiceListenner
    {
        void OnMessage(UpdaterWebSocketService updaterWebSocketService, MessageEventArgs e);
        void OnOpen(UpdaterWebSocketService updaterWebSocketService);
        void OnClose(UpdaterWebSocketService updaterWebSocketService, CloseEventArgs e);
        void OnError(UpdaterWebSocketService updaterWebSocketService, ErrorEventArgs e);
    }
}