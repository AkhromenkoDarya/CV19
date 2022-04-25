using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CV19
{
    public static class Program
    {
        [STAThreadAttribute()]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(App.CurrentDiectory)
                .ConfigureAppConfiguration((host, configuration) => configuration
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
                .ConfigureServices(App.ConfigureServices);
    }
}
