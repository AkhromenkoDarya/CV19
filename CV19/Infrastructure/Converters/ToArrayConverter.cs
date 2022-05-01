using CV19.Infrastructure.Converters.Base;
using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ToArrayConverter : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, 
            CultureInfo culture)
        {
            var collection = new CompositeCollection();

            foreach (object value in values)
            {
                collection.Add(value);
            }

            return collection;
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            CultureInfo culture)
        {
            var enumerable = value as IEnumerable;

            return (object[])enumerable;
        }
    }
}
