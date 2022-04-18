using CV19.Infrastructure.Commands;
using CV19.Models.Deanery;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Group> Groups { get; }

        #region GroupView

        public ICollectionView GroupView { get; }

        #endregion

        #region StudentView

        private readonly CollectionViewSource _selectedGroupStudents = new CollectionViewSource();

        public ICollectionView StudentView => _selectedGroupStudents?.View;

        #endregion

        public object[] CompositeCollection { get; }

        #region GroupFilterText: string - Текст фильтра групп студентов

        /// <summary>
        /// Текст фильтра групп студентов.
        /// </summary>
        private string _groupFilterText;

        /// <summary>
        /// Текст фильтра групп студентов.
        /// </summary>
        public string GroupFilterText
        {
            get => _groupFilterText;

            set
            {
                if (!Set(ref _groupFilterText, value))
                {
                    return;
                }

                GroupView.Refresh();
            }
        }

        #endregion

        #region StudentFilterText: string - Текст фильтра студентов

        /// <summary>
        /// Текст фильтра студентов.
        /// </summary>
        private string _studentFilterText;

        /// <summary>
        /// Текст фильтра студентов.
        /// </summary>
        public string StudentFilterText
        {
            get => _studentFilterText;

            set
            {
                if (!Set(ref _studentFilterText, value))
                {
                    return;
                }

                _selectedGroupStudents.View.Refresh();
            }
        }

        #endregion

        #region SelectedCompositeValue: object - Выбранный непонятный элемент

        /// <summary>
        /// Выбранный непонятный элемент.
        /// </summary>
        private object _selectedCompositeValue;

        /// <summary>
        /// Выбранный непонятный элемент.
        /// </summary>
        public object SelectedCompositeValue
        {
            get => _selectedCompositeValue;

            set => Set(ref _selectedCompositeValue, value);
        }

        #endregion

        #region SelectedGroup: Group - Выбранная группа

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        private Group _selectedGroup;

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;

            set
            {
                if (!Set(ref _selectedGroup, value))
                {
                    return;
                }

                _selectedGroupStudents.Source = value?.Students;
                _selectedGroupStudents.View.Refresh();
                OnPropertyChanged(nameof(StudentView));
            }
        }

        #endregion

        #region SelectedDirectory: DirectoryViewModel - Выбранная папка

        /// <summary>
        /// Выбранная папка.
        /// </summary>
        private DirectoryViewModel _selectedDirectory;

        /// <summary>
        /// Выбранная папка.
        /// </summary>
        public DirectoryViewModel SelectedDirectory
        {
            get => _selectedDirectory;

            set => Set(ref _selectedDirectory, value);
        }

        #endregion

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

        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 
            : 100000)
            .Select(i => new Student
            {
                Name = $"Name {i}",
                Surname = $"Surname {i}"
            });

        public DirectoryViewModel DiskRootDirectory => new DirectoryViewModel(@"C:\");

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p) => Application.Current.Shutdown();

        #endregion

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _selectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null)
            {
                return;
            }

            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #region AddGroupCommand

        public ICommand AddGroupCommand { get; }

        private bool CanAddGroupCommandExecute(object p) => true;

        private void OnAddGroupCommandExecuted(object p)
        {
            int groupMaxIndex = Groups.Count + 1;

            var newGroup = new Group
            {
                Name = $"Group {groupMaxIndex}",
                Students = new ObservableCollection<Student>()
            };

            Groups.Add(newGroup);
        }

        #endregion
                                                                                                   
        #region RemoveGroupCommand

        public ICommand RemoveGroupCommand { get; }

        private bool CanRemoveGroupCommandExecute(object p) => p is Group group
            && Groups.Contains(group);

        private void OnRemoveGroupCommandExecuted(object p)
        {
            if (!(p is Group group))
            {
                return;
            }

            int groupIndex = Groups.IndexOf(group);
            Groups.Remove(group);

            if (groupIndex < Groups.Count)
            {
                SelectedGroup = Groups[groupIndex];
            }
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, 
                CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new RelayCommand(OnChangeTabIndexCommandExecuted, 
                CanChangeTabIndexCommandExecute);
            AddGroupCommand = new RelayCommand(OnAddGroupCommandExecuted, 
                CanAddGroupCommandExecute);
            RemoveGroupCommand = new RelayCommand(OnRemoveGroupCommandExecuted,
                CanRemoveGroupCommandExecute);

            #endregion

            /*------------------------------- Plot ----------------------------------------------*/

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

            /*------------------------------- Students ------------------------------------------*/

            int studentIndex = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {studentIndex}",
                Surname = $"Surname {studentIndex}",
                Patronymic = $"Patronymic {studentIndex++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Group {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);
            SelectedGroup = Groups[0];

            var dataList = new List<object>();
            dataList.Add("Hello World!");
            dataList.Add(42);
            dataList.Add(Groups[1]);
            dataList.Add(Groups[1].Students[0]);

            CompositeCollection = dataList.ToArray();

            GroupView = CollectionViewSource.GetDefaultView(Groups);
            
            GroupView.Filter = GroupFilter;
            _selectedGroupStudents.Filter += SelectedGroupStudents_Filter;

            //_selectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", 
            //    ListSortDirection.Descending));
            //_selectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }

        private bool GroupFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_groupFilterText))
            {
                return true;
            }

            if (!(item is Group group))
            {
                return false;
            }

            if (group.Name is null)
            {
                return false;
            }

            return group.Name.Contains(_groupFilterText, StringComparison.OrdinalIgnoreCase);
        }

        private void SelectedGroupStudents_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_studentFilterText))
            {
                return;
            }

            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            if (student.Name is null || student.Surname is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(_studentFilterText, StringComparison.OrdinalIgnoreCase) 
                || student.Surname.Contains(_studentFilterText, StringComparison.OrdinalIgnoreCase)
                || student.Patronymic.Contains(_studentFilterText, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            e.Accepted = false;
        }
    }
}
