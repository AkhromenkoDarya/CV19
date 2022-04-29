using CV19.Web.Events;
using System;
using System.Net;

namespace CV19.Web
{
    public class WebServer
    {
        private event EventHandler<RequestReceiverEventArgs> RequestReceiver;

        //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));

        private HttpListener _listener;

        private readonly int _port;

        private bool _isEnabled;

        private readonly object _syncRoot = new object();

        public int Port => _port;

        public bool IsEnabled
        {
            get => _isEnabled;

            set
            {
                if (value)
                {
                    Start();
                    return;
                }
                
                Stop();
            }
        }

        public WebServer(int port) => _port = port;

        public void Start()
        {
            if (IsEnabled)
            {
                return;
            }

            //lock(this)
            lock (_syncRoot)
            {
                if (IsEnabled)
                {
                    return;
                }

                _listener = new HttpListener();
                _listener.Prefixes.Add($"http://*:{_port}");
                _listener.Prefixes.Add($"http://+:{_port}");
                _isEnabled = true;
            }

            ListenAsync();
        }

        public void Stop()
        {
            if (!IsEnabled)
            {
                return;
            }
            
            //lock(this)
            lock (_syncRoot)
            {
                if (!IsEnabled)
                {
                    return;
                }

                _listener = null;
                _isEnabled = false;
            }
        }

        private async void ListenAsync()
        {
            HttpListener listener = _listener;
            listener.Start();

            while (_isEnabled)
            {
                HttpListenerContext listenerContext = await listener.GetContextAsync()
                    .ConfigureAwait(false);
                ProcessRequest(listenerContext);
            }
            
            listener.Stop();
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            RequestReceiver?.Invoke(this, new RequestReceiverEventArgs(context));
        }
    }
}
