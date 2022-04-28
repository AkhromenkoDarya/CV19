using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ParametricMultiplyValueConverter : Freezable, IValueConverter
    {
        private const double _defaultValue = 1d;

        #region Value : double - Прибавляемое значение

        /// <summary>
        /// Прибавляемое значение.
        /// </summary>
        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(ParametricMultiplyValueConverter),
                new PropertyMetadata(_defaultValue/*, (d, e) => {}*/));

        /// <summary>
        /// Прибавляемое значение.
        /// </summary>
        [Description("Прибавляемое значение")]
        // [Category("")]
        public double Value
        {
            get => (double) GetValue(ValueProperty);

            set => SetValue(ValueProperty, value);
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            var convertedValue = System.Convert.ToDouble(value, culture);
            return convertedValue * Value;
                                                                                                    
        }

        public object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            var convertedValue = System.Convert.ToDouble(value, culture);
            return convertedValue / Value;
        }

        protected override Freezable CreateInstanceCore() => new ParametricMultiplyValueConverter();
    }
}
