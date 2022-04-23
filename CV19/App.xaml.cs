﻿using CV19.Services;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CV19
{
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        public static string CurrentDiectory => IsDesignMode 
            ? Path.GetDirectoryName(GetSourceCodePath()) 
            : Environment.CurrentDirectory;

        private static IHost _host;

        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment
            .GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
            services.AddSingleton<CountryStatisticsViewModel>();
        }

        private static string GetSourceCodePath([CallerFilePath] string path = null) => path;
    }
}
