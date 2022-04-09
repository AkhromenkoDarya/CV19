using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region TestDataPoints: IEnumerable<DataPoint> - Тестовый набор данных

        /// <summary>
        /// Тестовый набор данных для визуализации графиков.
        /// </summary>
        private IEnumerable<DataPoint> _testsDataPoints;

        /// <summary>
        /// Тестовый набор данных для визуализации графиков.
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints 
        { 
            get => _testsDataPoints;

            set => Set(ref _testsDataPoints, value);
        }

        #endregion

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

            const double radians = Math.PI / 180;
            var dataPoints = new List<DataPoint>((int)(360 / 0.1));

            for (double x = 0d; x <= 360; x += 0.1)
            {
                double y = Math.Sin(x * radians);
                dataPoints.Add(new DataPoint { xValue = x, yValue = y });
            }

            TestDataPoints = dataPoints;
        }
    }
}
