using CV19.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CV19WPFTest.Behaviors
{
    public class CloseWindow : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object sender, RoutedEventArgs e) => (AssociatedObject
            .FindRoot(typeof(VisualTreeHelper)) as Window)?.Close();
    }
}
