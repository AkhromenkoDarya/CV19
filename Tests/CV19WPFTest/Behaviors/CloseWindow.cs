using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Media;

namespace CV19WPFTest.Behaviors
{
    public class CloseWindow : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var obj = (DependencyObject)AssociatedObject;

            while (true)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(obj);

                if (parent is null)
                {
                    break;
                }

                obj = parent;
            }

            var window = obj as Window;
            window?.Close();
        }
    }
}
