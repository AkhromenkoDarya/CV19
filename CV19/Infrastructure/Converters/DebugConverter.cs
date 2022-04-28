using CV19.Infrastructure.Converters.Base;
using System;
using System.Diagnostics;
using System.Globalization;

namespace CV19.Infrastructure.Converters
{
    internal class DebugConverter : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}
