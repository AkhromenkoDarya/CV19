using System.Collections.Generic;
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

        #region SelectedStudentGroup : Group - Выбранная группа студентов

        /// <summary>
        /// Выбранная группа студентов.
        /// </summary>
        private Group _selectedStudentGroup;

        /// <summary>
        /// Выбранная группа студентов.
        /// </summary>
        public Group SelectedStudentGroup
        {
            get => _selectedStudentGroup;

            set => Set(ref _selectedStudentGroup, value);
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

        public StudentManagementViewModel(StudentManager studentManager) => 
            _studentManager = studentManager;
    }
}
