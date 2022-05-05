using CV19.ViewModels.Deanery;
using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels.Locator
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindow => App.Host.Services
            .GetRequiredService<MainWindowViewModel>();

        public StudentManagementViewModel StudentManagement => App.Host.Services
            .GetRequiredService<StudentManagementViewModel>();
    }
}
