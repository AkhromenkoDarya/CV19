using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(CompositeConverter))]
    internal class CompositeConverter : Converter
    {
        [ConstructorArgument(nameof(FirstConverter))]
        public IValueConverter FirstConverter { get; set; }

        [ConstructorArgument(nameof(SecondConverter))]
        public IValueConverter SecondConverter { get; set; }

        public CompositeConverter()
        {
        }

        public CompositeConverter(IValueConverter firstConverter) 
            => FirstConverter = firstConverter;

        public CompositeConverter(IValueConverter firstConverter, IValueConverter secondConverter)
            : this(firstConverter)
            => SecondConverter = secondConverter;

        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            object firstConverterResult = FirstConverter?.Convert(value, targetType, parameter, 
                culture) ?? value;
            object secondConverterResult = SecondConverter?.Convert(firstConverterResult, targetType, 
                parameter, culture) ?? firstConverterResult;

            return secondConverterResult;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            object secondConverterResult = SecondConverter?.ConvertBack(value, targetType, parameter, 
                culture) ?? value;
            object firstConverterResult = FirstConverter.ConvertBack(secondConverterResult, targetType,
                parameter, culture) ?? secondConverterResult;

            return firstConverterResult;
        }
    }
}
