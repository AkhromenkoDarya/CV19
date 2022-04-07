using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Title : string - Заголовок окна

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        private string _title = "CV-19 statistics analysis";

        /// <summary>
        /// Возвращает и задает заголовок окна.
        /// </summary>
        public string Title 
        {
            get => _title;

            //set
            //{
            //    //if (Equals(_title, value))
            //    //{
            //    //    return;
            //    //}

            //    //_title = value;
            //    //OnPropertyChanged();

            //    Set(ref _title, value);
            //}
            set => Set(ref _title, value);
        }

        #endregion

        #region Status : string - Статус программы

        /// <summary>
        /// Статус программы.
        /// </summary>
        private string _status = "Ready";

        /// <summary>
        /// Возвращает и задает статус программы.
        /// </summary>
        public string Status 
        { 
            get => _status; 
            
            set => Set(ref _status, value);
        }

        #endregion

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;

        private void OnCloseApplicationCommandExecute(object p) => Application.Current.Shutdown();

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecute, 
                CanCloseApplicationCommandExecuted);

            #endregion
        }
    }
}
