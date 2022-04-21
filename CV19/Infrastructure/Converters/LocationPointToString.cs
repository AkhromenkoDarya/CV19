using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows;

namespace CV19.Infrastructure.Converters
{
    internal class LocationPointToString : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (!(value is Point point))
            {
                return null;
            }

            return $"Lat: {point.X}; Lon: {point.Y}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (!(value is string str))
            {
                return null;
            }

            var components = str.Split(';');
            var latitudeString = components[0].Split(':')[1];
            var longitudeString = components[1].Split(':')[1];

            var latitude = double.Parse(latitudeString);
            var longitude = double.Parse(longitudeString);

            return new Point(latitude, longitude);
        }
    }
}
