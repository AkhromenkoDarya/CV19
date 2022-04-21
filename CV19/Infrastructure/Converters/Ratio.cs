using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;

namespace CV19.Infrastructure.Converters
{
    internal class Ratio : Converter
    {
        public double Coefficient { get; set; } = 1;

        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            var x = System.Convert.ToDouble(value, culture);

            return x * Coefficient;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }

            var x = System.Convert.ToDouble(value, culture);

            return x / Coefficient;
        }
    }
}
