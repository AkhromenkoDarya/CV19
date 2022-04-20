using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountryStatisticsViewModel : ViewModel
    {
        private MainWindowViewModel MainWindowViewModel { get; }

        #region Countries : IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>
        /// Статистика по странам.
        /// </summary>
        private IEnumerable<CountryInfo> _countries;

        /// <summary>
        /// Статистика по странам.
        /// </summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _countries;

            private set => Set(ref _countries, value);
        }

        #endregion

        #region Команды

        #region RefreshDataCommand

        public ICommand RefreshDataCommand { get; }

        //private bool CanRefreshDataCommandExecute(object p)
        //{
        //    return true;
        //}

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = DataService.GetData();
        }

        #endregion

        #endregion

        public CountryStatisticsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;

            #region Команды

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}
