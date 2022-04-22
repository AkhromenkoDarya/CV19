using CV19.Infrastructure.Converters.Base;
using MapControl;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToMapLocation))]
    [ValueConversion(typeof(Point), typeof(Location))]
    internal class PointToMapLocation : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (!(value is Point point))
            {
                return null;
            }

            return new Location(point.X, point.Y);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (!(value is Location location))
            {
                return null;
            }

            return new Point(location.Latitude, location.Longitude);
        }
    }
}
