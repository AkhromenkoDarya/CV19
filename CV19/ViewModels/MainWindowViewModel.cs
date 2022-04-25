using CV19.Infrastructure.Commands;
using CV19.Models.Deanery;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace CV19.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : ViewModel
    {
        public CountryStatisticsViewModel CountryStatistics { get; }

        #region StudentView

        private readonly CollectionViewSource _selectedGroupStudents = new CollectionViewSource();

        public ICollectionView StudentView => _selectedGroupStudents?.View;

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

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            // Возможно получить доступ к представлению из модели представления, но это не очень
            // хорошо с точки зрения архитектуры.
            // (RootObject as Window)?.Close();
            Application.Current.Shutdown();
        }

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

        #endregion

        public MainWindowViewModel(CountryStatisticsViewModel countryStatistics)
        {
            countryStatistics.MainWindowViewModel = this;
            CountryStatistics = countryStatistics;

            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted,
                CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new RelayCommand(OnChangeTabIndexCommandExecuted,
                CanChangeTabIndexCommandExecute);

            #endregion
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
