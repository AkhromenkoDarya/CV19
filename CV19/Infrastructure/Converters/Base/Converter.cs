using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters.Base
{
    internal abstract class Converter : MarkupExtension, IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            throw new NotSupportedException("Back conversion is not supported.");
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
