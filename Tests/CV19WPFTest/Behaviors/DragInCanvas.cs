using System.ComponentModel;
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

        #region PositionX : double - Горизонтальное положение

        /// <summary>
        /// Горизонтальное положение.
        /// </summary>
        public static readonly DependencyProperty PositionXProperty
            = DependencyProperty.Register(
                nameof(PositionX),
                typeof(double),
                typeof(DragInCanvas),
                new PropertyMetadata(default(double)));

        /// <summary>
        /// Горизонтальное положение.
        /// </summary>
        [Description("Горизонтальное положение")]
        // [Category("")]
        public double PositionX
        {
            get => (double)GetValue(PositionXProperty);
            set => SetValue(PositionXProperty, value);
        }

        #endregion

        #region PositionY : double - Вертикальное положение

        /// <summary>
        /// Вертикальное положение.
        /// </summary>
        public static readonly DependencyProperty PositionYProperty
            = DependencyProperty.Register(
                nameof(PositionY),
                typeof(double),
                typeof(DragInCanvas),
                new PropertyMetadata(default(double)));

        /// <summary>
        /// Вертикальное положение.
        /// </summary>
        [Description("Вертикальное положение")]
        // [Category("")]
        public double PositionY
        {
            get => (double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }

        #endregion

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

            PositionX = delta.X;
            PositionY = delta.Y;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }
    }
}
