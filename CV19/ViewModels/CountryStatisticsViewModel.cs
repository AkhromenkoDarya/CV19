using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.ViewModels
{
    internal class CountryStatisticsViewModel : ViewModel
    {
        private MainWindowViewModel MainWindowViewModel { get; }

        public CountryStatisticsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
        }
    }
}
