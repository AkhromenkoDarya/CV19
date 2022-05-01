using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CV19WPFTest.Behaviors
{
    public class DragInCanvas : Behavior<UIElement>
    {
        private Canvas _canvas;

        private Point _startPosition;

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (_canvas is null)
            //{
            //    _canvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;

            //    if (_canvas is null)
            //    {
            //        return;
            //    }
            //}

            //var canvas = _canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null;
            //          ==
            //var canvas = _canvas ?? (_canvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas);

            if ((_canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null)
            {
                return;
            }

            _startPosition = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            UIElement obj = AssociatedObject;
            Point currentPosition = e.GetPosition(_canvas);
            Vector delta = currentPosition - _startPosition;

            obj.SetValue(Canvas.LeftProperty, delta.X);
            obj.SetValue(Canvas.TopProperty, delta.Y);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }
    }
}
