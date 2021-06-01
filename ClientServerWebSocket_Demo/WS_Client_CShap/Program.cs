using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS_Client_CShap
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new FormClientStomp()); // WsClient using Stomp, connect to ERPIA Websocket server 
            
            //Application.Run(new Form1()); // WsClient using WebSocket-Sharp, connect to our demo WsServer C# (run the project WS_Server_Shap).
        }
    }
}
