using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class CompositeConverter : Converter
    {
        public IValueConverter FirstConverter { get; set; }

        public IValueConverter SecondConverter { get; set; }

        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            var firstConverterResult = FirstConverter?.Convert(value, targetType, parameter, 
                culture) ?? value;
            var secondConverterResult = SecondConverter?.Convert(firstConverterResult, targetType, 
                parameter, culture) ?? firstConverterResult;

            return secondConverterResult;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            var secondConverterResult = SecondConverter?.ConvertBack(value, targetType, parameter, 
                culture) ?? value;
            var firstConverterResult = FirstConverter.ConvertBack(secondConverterResult, targetType,
                parameter, culture) ?? secondConverterResult;

            return firstConverterResult;
        }
    }
}
