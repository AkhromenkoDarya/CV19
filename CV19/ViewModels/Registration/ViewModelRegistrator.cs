using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels.Registration
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<CountryStatisticsViewModel>();

            return services;
        }
    }
}
