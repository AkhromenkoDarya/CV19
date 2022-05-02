using CV19.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CV19.Infrastructure.Behaviors
{
    public class MaximizeWindow : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(AssociatedObject.FindRoot(typeof(VisualTreeHelper)) is Window window))
            {
                return;
            }

            //switch (window.WindowState)
            //{
            //    case WindowState.Normal:
            //        window.WindowState = WindowState.Maximized;
            //        break;
            //    case WindowState.Maximized:
            //        window.WindowState = WindowState.Normal;
            //        break;
            //}

            window.WindowState = window.WindowState switch
            {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => window.WindowState
            };
        }
    }
}
