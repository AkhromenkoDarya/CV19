using System.Buffers;
using CV19.Infrastructure.Commands;
using CV19.Models.Deanery;
using CV19.Services.Deanery;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace CV19.ViewModels.Deanery
{
    internal class StudentManagementViewModel : ViewModel
    {
        private readonly StudentManager _studentManager;

        private readonly IUserDialogService _userDialog;

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
        private bool CanAddStudentCommandExecute(object p) => p is Group;

        /// <summary>
        /// Логика выполнения - Команда добавления нового студента.
        /// </summary>
        private void OnAddStudentCommandExecuted(object p)
        {
            var group = (Group)p;
            var student = new Student();

            if (!_userDialog.Edit(student) || _studentManager.Add(student, group.Name))
            {
                OnPropertyChanged(nameof(Students));
                return;
            }

            if (_userDialog.Confirm("Failed to add a new student. Do you want to try again?", 
                    "Student Manager"))
            {
                OnAddStudentCommandExecuted(p);
            }
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
        private bool CanEditStudentCommandExecute(object p) => p is Student;

        /// <summary>
        /// Логика выполнения - Команда редактирования студента.
        /// </summary>
        private void OnEditStudentCommandExecuted(object p)
        {
            if (_userDialog.Edit(p))
            {
                _studentManager.Update((Student) p);
                _userDialog.ShowInformation("Student is edited", "Student Manager");
                OnPropertyChanged(nameof(Students));
                return;
            }

            _userDialog.ShowWarning("Denial to edit", "Student Manager");
        }

        #endregion

        #region Command TestCommand - Тестовая команда

        /// <summary>
        /// Тестовая команда.
        /// </summary>
        private ICommand _testCommand;

        /// <summary>
        /// Тестовая команда.
        /// </summary>
        public ICommand TestCommand => _testCommand ??=
            new RelayCommand(OnTestCommandExecuted, CanTestCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Тестовая команда.
        /// </summary>
        private bool CanTestCommandExecute(object p) => true;

        /// <summary>
        /// Логика выполнения - Тестовая команда.
        /// </summary>
        private void OnTestCommandExecuted(object p)
        {
            string value = _userDialog.GetStringValue("Enter the string:", 
                "Window for entering a string value", "Default Value");
            _userDialog.ShowInformation($"You entered string \"{value}\"", "Information Window");
        }

        #endregion

        #endregion

        public StudentManagementViewModel(StudentManager studentManager, 
            IUserDialogService userDialog)
        {
            _studentManager = studentManager;
            _userDialog = userDialog;
        }
    }
}
