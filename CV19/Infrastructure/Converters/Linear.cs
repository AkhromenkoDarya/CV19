using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    /// <summary>
    /// Реализация линейного преобразования f(x) = k * x + b.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    [MarkupExtensionReturnType(typeof(Linear))]
    internal class Linear : Converter
    {
        [ConstructorArgument(nameof(K))]
        public double K { get; set; } = 1;

        [ConstructorArgument(nameof(B))]
        public double B { get; set; } = 0;

        public Linear()
        {
        }

        public Linear(double k) => K = k;

        public Linear(double k, double b) : this(k) => B = b;

        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            var x = System.Convert.ToDouble(value, culture);
            
            return K * x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            var y = System.Convert.ToDouble(value, culture);

            return (y - B) / K;
        }
    }
}
