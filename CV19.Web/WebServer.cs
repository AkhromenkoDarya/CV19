using CV19.Web.Events;
using System;
using System.Net;

namespace CV19.Web
{
    public class WebServer
    {
        //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));

        public event EventHandler<RequestReceiverEventArgs> RequestReceived;

        private HttpListener _listener;

        private bool _isEnabled;

        private readonly object _syncRoot = new object();

        public int Port { get; }

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

        public WebServer(int port) => Port = port;

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

                // Выполнить в консоли команды с правами администратора:
                //netsh http add urlacl url=http://*:{Port}/ user=user_name
                //netsh http add urlacl url=http://+:{Port}/ user=user_name

                _listener.Prefixes.Add($"http://*:{Port}/");
                _listener.Prefixes.Add($"http://+:{Port}/");

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
            RequestReceived?.Invoke(this, new RequestReceiverEventArgs(context));
        }
    }
}
