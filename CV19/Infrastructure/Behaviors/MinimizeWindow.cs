using CV19.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CV19.Infrastructure.Behaviors
{
    public class MinimizeWindow : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(AssociatedObject.FindRoot(typeof(VisualTreeHelper)) is Window window))
            {
                return;
            }

            window.WindowState = WindowState.Minimized;
        }
    }
}
