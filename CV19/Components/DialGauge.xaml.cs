using System.ComponentModel;
using System.Windows;

namespace CV19.Components
{
    public partial class DialGauge
    {
        #region Value : double - Значение стрелки индикатора

        /// <summary>
        /// Значение стрелки индикатора.
        /// </summary>
        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register(
                nameof(Value),
                typeof(double ),
                typeof(DialGauge),
                new PropertyMetadata(default(double )));

        //private static void OnPropertyChanged(DependencyObject d, 
        //    DependencyPropertyChangedEventArgs e)
        //{
        //    var dialGauge = (DialGauge)d;
        //    dialGauge.ArrowRotator.Angle = (double)e.NewValue;
        //}

        //private static object OnCoerceValue(DependencyObject d, object baseValue)
        //{
        //    var value = (double)baseValue;
        //    return Math.Max(0, Math.Min(100, value));
        //}

        //private static bool OnValidateValue(object value)
        //{
        //    return true;
        //}

        /// <summary>
        /// Значение стрелки индикатора.
        /// </summary>
        [Description("Значение стрелки индикатора")]
        [Category("Моя категория")]
        public double  Value
        {
            get => (double ) GetValue(ValueProperty);

            set => SetValue(ValueProperty, value);
        }

        #endregion


        public DialGauge() => InitializeComponent();
    }
}
