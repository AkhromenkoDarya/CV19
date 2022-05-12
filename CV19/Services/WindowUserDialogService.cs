using CV19.Models.Deanery;
using CV19.Services.Interfaces;
using CV19.Views.Windows.Deanery;
using System;
using System.Windows;

namespace CV19.Services
{
    internal class WindowUserDialogService : IUserDialogService
    {
        public bool Edit(object item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            switch (item)
            {
                case Student student:
                    return EditStudent(student);
                default:
                    throw new NotSupportedException($"Editing an object of type " +
                                                    $"\"{item.GetType().Name}\" is not supported");
            }
        }

        public void ShowInformation(string information, string caption) 
            => MessageBox.Show(
                information, 
                caption, 
                MessageBoxButton.OK, 
                MessageBoxImage.Information);

        public void ShowWarning(string message, string caption) => 
            MessageBox.Show(
                message, 
                caption, 
                MessageBoxButton.OK, 
                MessageBoxImage.Warning);

        public void ShowError(string message, string caption) => 
            MessageBox.Show(
                message, 
                caption, 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);

        public bool Confirm(string message, string caption, bool exclamation = false) =>
            MessageBox.Show(
                message,
                caption,
                MessageBoxButton.YesNo,
                exclamation ? MessageBoxImage.Exclamation : MessageBoxImage.Question)
            == MessageBoxResult.Yes;

        private static bool EditStudent(Student student)
        {
            var dialog = new StudentEditingWindow
            {
                LastName = student.Surname,
                FirstName = student.Name,
                Patronymic = student.Patronymic,
                Rating = student.Rating,
                Birthday = student.Birthday
            };

            if (dialog.ShowDialog() != true)
            {
                return false;
            }

            student.Surname = dialog.LastName;
            student.Name = dialog.FirstName;
            student.Patronymic = dialog.Patronymic;
            student.Rating = dialog.Rating;
            student.Birthday = dialog.Birthday;

            return true;
        }
    }
}
