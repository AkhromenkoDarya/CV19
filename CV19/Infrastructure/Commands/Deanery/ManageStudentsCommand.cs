using CV19.Infrastructure.Commands.Base;
using CV19.Views.Windows.Deanery;
using System;
using System.Windows;

namespace CV19.Infrastructure.Commands.Deanery
{
    internal class ManageStudentsCommand : Command
    {
        private StudentManagementWindow _window;

        public override bool CanExecute(object parameter) => _window is null;

        public override void Execute(object parameter)
        {
            _window = new StudentManagementWindow
            {
                Owner = Application.Current.MainWindow
            };

            _window.Closed += OnWindowClosed;
            _window.ShowDialog();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= OnWindowClosed;
            _window = null;
        }
    }
}
