using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace CV19.Infrastructure.Behaviors
{
    public class ShowWindowMenu : Behavior<FrameworkElement>
    {
        protected override void OnAttached() => AssociatedObject.MouseUp += OnMouseUp;

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            if (!(element.TemplatedParent is Window window))
            {
                return;
            }

            double halfWidthIcon = element.ActualWidth / 2;
            double halfHeightIcon = element.ActualHeight / 2;

            Point point = window.WindowState == WindowState.Maximized 
                ? new Point(halfWidthIcon, halfHeightIcon)
                : new Point(window.Left + halfWidthIcon, window.Top + halfHeightIcon);
            SystemCommands.ShowSystemMenu(window, point);
        }

        protected override void OnDetaching() => AssociatedObject.MouseUp -= OnMouseUp;
    }
}
