using CV19.ViewModels.Deanery;
using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels.Registration
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<CountryStatisticsViewModel>();
            services.AddSingleton<WebServerViewModel>();

            services.AddTransient<StudentManagementViewModel>();

            return services;
        }
    }
}
