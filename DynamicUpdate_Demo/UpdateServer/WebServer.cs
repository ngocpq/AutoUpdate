using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;

namespace UpdateServer
{
    public abstract class WebServer
    {
        public string RootDir = @"C:\webserver\";

        private readonly HttpListener _listener;

        public bool isRunning { get { return _listener.IsListening; } }

        public WebServer(string prefixes):this(new string[] { prefixes }) { }

        public WebServer(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            _listener = new HttpListener();
            // URI prefixes are required, for example "http://localhost:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // A responder method is required
            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);            
        }

        protected abstract HttpListenerResponse WriteResponse(HttpListenerContext context);

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            if (!listener.IsListening)
                return;
            
            listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            HttpListenerContext context = listener.EndGetContext(result);
            
            this.WriteResponse(context);            
        }

        public string GetAbsolutePath(string virtualPath)
        {
            return Path.Combine(RootDir, virtualPath);
        }

        public static byte[] GetFile(string file)
        {
            if (!File.Exists(file)) return null;
            FileStream readIn = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024 * 1000];
            int nRead = readIn.Read(buffer, 0, 10240);
            int total = 0;
            while (nRead > 0)
            {
                total += nRead;
                nRead = readIn.Read(buffer, total, 10240);
            }
            readIn.Close();
            byte[] maxresponse_complete = new byte[total];
            System.Buffer.BlockCopy(buffer, 0, maxresponse_complete, 0, total);
            return maxresponse_complete;
        }

        public void start()
        {
            _listener.Start();
            _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
        }

        public void Stop()
        {
            _listener.Stop();
        }

        internal void shutdown()
        {
            _listener.Close();
        }

        public string[] Prefixes
        {
            get
            {
                string[] prefixes = new string[_listener.Prefixes.Count];
                int i = 0;
                foreach (string p in _listener.Prefixes)
                {
                    prefixes[i++] = p;
                }
                return prefixes;
            }
        }
    }
}
