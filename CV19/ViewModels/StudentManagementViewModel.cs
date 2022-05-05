using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class StudentManagementViewModel : ViewModel
    {
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
    }
}
