using Microsoft.Extensions.DependencyInjection;

namespace CV19.Services.Interfaces.Registration
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Одиночная регистрация - объект создается один раз при его первом запросе.
            services.AddSingleton<IDataService, DataService>();

            // Временная регистрация - на каждый последующий запрос создается новый объект.
            //services.AddTransient<IDataService, DataService>();

            // Регистрация в режиме области видимости.
            //services.AddScoped<IDataService, DataService>();

            services.AddTransient<IAsyncDataService, AsyncDataService>();
            services.AddTransient<IWebServerService, HttpListenerWebServer>();

            return services;
        }
    }
}
