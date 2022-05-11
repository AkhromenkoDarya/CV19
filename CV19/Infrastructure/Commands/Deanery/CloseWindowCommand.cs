using CV19.Infrastructure.Commands.Base;
using System.Windows;

namespace CV19.Infrastructure.Commands.Deanery
{
    class CloseWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            ((Window)parameter).Close();
        }
    }
}
