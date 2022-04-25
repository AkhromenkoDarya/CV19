using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountryStatisticsViewModel : ViewModel
    {
        public MainWindowViewModel MainWindowViewModel { get; internal set; }

        private IDataService DataService { get; }

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

        #region SelectedCountry : CountryInfo - Выбранная страна

        /// <summary>
        /// Выбранная страна.
        /// </summary>
        private CountryInfo _selectedCountry;

        /// <summary>
        /// Выбранная страна.
        /// </summary>
        public CountryInfo SelectedCountry
        {
            get => _selectedCountry;

            set => Set(ref _selectedCountry, value);
        }

        #endregion

        #region Команды

        #region RefreshDataCommand

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p) => Countries = DataService.GetData();

        #endregion

        #endregion

        #region Конструкторы

        /// <summary>
        /// Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере.
        /// </summary>
        //public CountryStatisticsViewModel() : this(null)
        //{
        //    if (!App.IsDesignMode)
        //    {
        //        throw new InvalidOperationException("Calling a constructor not intended to work " 
        //            + "in a design mode.");
        //    }

        //    _countries = Enumerable.Range(1, 10)
        //        .Select(i => new CountryInfo
        //        {
        //            Name = $"Country {i}",
        //            Provinces = Enumerable.Range(1, 10).Select(j => new PlaceInfo
        //            {
        //                Name = $"Province {j}",
        //                Location = new Point(i, j),
        //                ConfirmedCases = Enumerable.Range(1, 10).Select(k => new ConfirmedCase
        //                {
        //                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
        //                    Count = k
        //                }).ToArray()
        //            }).ToArray()
        //        }).ToArray();
        //}

        public CountryStatisticsViewModel(IDataService dataService)
        {
            DataService = dataService;

            //var data = App.Host.Services.GetRequiredService<IDataService>();
            //var areReferencesEqual = ReferenceEquals(dataService, data);

            //using (var scope = App.Host.Services.CreateScope())
            //{
            //    var data2 = scope.ServiceProvider.GetRequiredService<IDataService>();
            //    var areReferencesEqual2 = ReferenceEquals(dataService, data2);
            //    var areReferencesEqual3 = ReferenceEquals(data, data2);
            //}

            #region Команды

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted);

            #endregion
        }

        #endregion
    }
}
