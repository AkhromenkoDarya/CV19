using System.Collections.Generic;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models.Deanery;
using CV19.Services.Deanery;
using CV19.ViewModels.Base;

namespace CV19.ViewModels.Deanery
{
    internal class StudentManagementViewModel : ViewModel
    {
        private readonly StudentManager _studentManager;

        public IEnumerable<Group> Groups => _studentManager.Groups;

        public IEnumerable<Student> Students => _studentManager.Students;

        #region Title : string - Заголовок окна

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        private string _title = "Student Management";

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public string Title
        {
            get => _title;

            set => Set(ref _title, value);
        }

        #endregion

        #region SelectedGroup : Group - Выбранная группа студентов

        /// <summary>
        /// Выбранная группа студентов.
        /// </summary>
        private Group _selectedGroup;

        /// <summary>
        /// Выбранная группа студентов.
        /// </summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;

            set => Set(ref _selectedGroup, value);
        }

        #endregion

        #region SelectedStudent : Student - Выбранный студент

        /// <summary>
        /// Выбранный студент.
        /// </summary>
        private Student _selectedStudent;

        

        /// <summary>
        /// Выбранный студент.
        /// </summary>
        public Student SelectedStudent
        {
            get => _selectedStudent;

            set => Set(ref _selectedStudent, value);
        }

        #endregion

        #region Команды

        #region Command AddStudentCommand - Команда добавления нового студента.

        /// <summary>
        /// Команда добавления нового студента.
        /// </summary>
        private ICommand _addStudentCommand;

        /// <summary>
        /// Команда добавления нового студента.
        /// </summary>
        public ICommand AddStudentCommand => _addStudentCommand ??=
            new RelayCommand(OnAddStudentCommandExecuted, CanAddStudentCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда добавления нового студента.
        /// </summary>
        private bool CanAddStudentCommandExecute(object p) => SelectedGroup != null;

        /// <summary>
        /// Логика выполнения - Команда добавления нового студента.
        /// </summary>
        private void OnAddStudentCommandExecuted(object p)
        {
            var student = (Student)p;
        }

        #endregion

        #region Command EditStudentCommand - Команда редактирования студента

        /// <summary>
        /// Команда редактирования студента.
        /// </summary>
        private ICommand _editStudentCommand;

        /// <summary>
        /// Команда редактирования студента.
        /// </summary>
        public ICommand EditStudentCommand => _editStudentCommand ??=
            new RelayCommand(OnEditStudentCommandExecuted, CanEditStudentCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда редактирования студента.
        /// </summary>
        private bool CanEditStudentCommandExecute(object p) => SelectedStudent != null;

        /// <summary>
        /// Логика выполнения - Команда редактирования студента.
        /// </summary>
        private void OnEditStudentCommandExecuted(object p)
        {
            var student = (Student)p;

        }

        #endregion

        #endregion

        public StudentManagementViewModel(StudentManager studentManager) => 
            _studentManager = studentManager;
    }
}
