using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region SelectedPageIndex: int - Индекс выбранной вкладки

        /// <summary>
        /// Индекс выбранной вкладки.
        /// </summary>
        private int _selectedPageIndex;

        /// <summary>
        /// Индекс выбранной вкладки.
        /// </summary>
        public int SelectedPageIndex
        {
            get => _selectedPageIndex;

            set => Set(ref _selectedPageIndex, value);
        }

        #endregion

        #region TestDataPoints: IEnumerable<DataPoint> - Тестовый набор данных

        /// <summary>
        /// Тестовый набор данных для визуализации графиков.
        /// </summary>
        private PlotModel _testsDataPoints;

        /// <summary>
        /// Тестовый набор данных для визуализации графиков.
        /// </summary>
        public PlotModel TestDataPoints
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

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecute(object p) => Application.Current.Shutdown();

        #endregion

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _selectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecute(object p)
        {
            if (p is null)
            {
                return;
            }

            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecute, 
                CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new RelayCommand(OnChangeTabIndexCommandExecute, 
                CanChangeTabIndexCommandExecute);

            #endregion

            const double radians = Math.PI / 180;
            var dataPoints = new List<DataPoint>((int)(360 / 0.1));

            for (double x = 0d; x <= 360; x += 0.1)
            {
                double y = Math.Sin(x * radians);
                dataPoints.Add(new DataPoint(x, y));
            }

            TestDataPoints = new PlotModel();

            TestDataPoints.Axes.Add(new LinearAxis() 
            { 
                Title = "TestPlotXAxis", 
                Position = AxisPosition.Left 
            });

            TestDataPoints.Axes.Add(new LinearAxis() 
            { 
                Title = "TestPlotYAxis", 
                Position = AxisPosition.Bottom 
            });

            TestDataPoints.Series.Add(new LineSeries()
            {
                ItemsSource = dataPoints,
                Color = OxyColors.Red
            });
        }
    }
}
