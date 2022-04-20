using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _location;

        public override Point Location 
        {
            get
            {
                if (_location != null)
                {
                    return (Point)_location;
                }

                if (ProvinceCount is null)
                {
                    return default;
                }

                double averageCountX = ProvinceCount.Average(p => p.Location.X);
                double averageCountY = ProvinceCount.Average(p => p.Location.Y);

                return (Point)(_location = new Point(averageCountX, averageCountY));
            }

            set => _location = value;
        }

        public IEnumerable<PlaceInfo> ProvinceCount { get; set; }
    }
}
