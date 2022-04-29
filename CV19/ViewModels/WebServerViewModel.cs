using CV19.Infrastructure.Commands;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class WebServerViewModel : ViewModel
    {
        private readonly IWebServerService _serverService;

        #region Enabled : bool - Доступность веб-сервера

        /// <summary>
        /// Доступность веб-сервера.
        /// </summary>
        public bool Enabled
        {
            get => _serverService.Enabled;

            set
            {
                _serverService.Enabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команды

        #region Command StartWebServerCommand - Запуск веб-сервера

        /// <summary>
        /// Запуск веб-сервера.
        /// </summary>
        private ICommand _startWebServerCommand;

        /// <summary>
        /// Запуск веб-сервера.
        /// </summary>
        public ICommand StartWebServerCommand => _startWebServerCommand ??= new RelayCommand(
            OnStartWebServerCommandExecuted, CanStartWebServerCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Запуск веб-сервера.
        /// </summary>
        private bool CanStartWebServerCommandExecute(object p) => !Enabled;

        /// <summary>
        /// Логика выполнения - Запуск веб-сервера.
        /// </summary>
        private void OnStartWebServerCommandExecuted(object p)
        {
            _serverService.Start();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion

        #region Command StopWebServerCommand - Остановка веб-сервера

        /// <summary>
        /// Остановка веб-сервера.
        /// </summary>
        private ICommand _stopWebServerCommand;

        /// <summary>
        /// Остановка веб-сервера.
        /// </summary>
        public ICommand StopWebServerCommand => _stopWebServerCommand ??= new RelayCommand(
            OnStopWebServerCommandExecuted, CanStopWebServerCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Остановка веб-сервера.
        /// </summary>
        private bool CanStopWebServerCommandExecute(object p) => Enabled;

        /// <summary>
        /// Логика выполнения - Остановка веб-сервера.
        /// </summary>
        private void OnStopWebServerCommandExecuted(object p)
        {
            _serverService.Stop();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion

        #endregion

        public WebServerViewModel(IWebServerService serverService) => _serverService = serverService;
    }
}
