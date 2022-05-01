using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(Point), typeof(string))]
    [MarkupExtensionReturnType(typeof(LocationPointToString))]
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

            string[] components = str.Split(';');
            string latitudeString = components[0].Split(':')[1];
            string longitudeString = components[1].Split(':')[1];

            double latitude = double.Parse(latitudeString);
            double longitude = double.Parse(longitudeString);

            return new Point(latitude, longitude);
        }
    }
}
