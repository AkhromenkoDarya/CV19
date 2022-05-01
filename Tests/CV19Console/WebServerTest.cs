using CV19.Web;
using CV19.Web.Events;
using System;
using System.IO;
using System.Net;

namespace CV19Console
{
    internal static class WebServerTest
    {
        private const int DefaultPort = 8080;

        public static void Run()
        {
            var server = new WebServer(DefaultPort);
            server.RequestReceived += OnRequestReceived;

            server.Start();
            Console.WriteLine("Test Web Server is started!");

            Console.ReadLine();
        }

        private static void OnRequestReceived(object sender, RequestReceiverEventArgs e)
        {
            HttpListenerContext context = e.Context;
            Console.WriteLine("Connection {0}", context.Request.UserHostAddress);

            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.WriteLine("Hello from Test Web Server!");
        }
    }
}
