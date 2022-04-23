using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels.Locator
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowVM => App.Host.Services
            .GetRequiredService<MainWindowViewModel>();
    }
}
