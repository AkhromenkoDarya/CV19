using CV19.Services.Interfaces;
using CV19.Web;
using CV19.Web.Events;
using System.IO;

namespace CV19.Services
{
    internal class WebServerService : IWebServerService
    {
        private readonly WebServer _server = new WebServer(8080);

        public bool IsEnabled
        {
            get => _server.IsEnabled;

            set => _server.IsEnabled = !value;
        }

        public void Start() => _server.Start();

        public void Stop() => _server.Stop();

        public WebServerService() => _server.RequestReceived += OnRequestReceived;

        private static void OnRequestReceived(object sender, RequestReceiverEventArgs e)
        {
            using var writer = new StreamWriter(e.Context.Response.OutputStream);
            writer.WriteLine("CV19 Application");
        }
    }
}
