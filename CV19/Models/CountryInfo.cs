using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _location;

        private IEnumerable<ConfirmedCase> _confirmedCases;

        public override Point Location
        {
            get
            {
                if (_location != null)
                {
                    return (Point)_location;
                }

                if (Provinces is null)
                {
                    return default;
                }

                double averageCountX = Provinces.Average(p => p.Location.X);
                double averageCountY = Provinces.Average(p => p.Location.Y);

                return (Point)(_location = new Point(averageCountX, averageCountY));
            }

            set => _location = value;
        }

        public IEnumerable<PlaceInfo> Provinces { get; set; }

        public override IEnumerable<ConfirmedCase> ConfirmedCases
        {
            get
            {
                if (_confirmedCases != null)
                {
                    return _confirmedCases;
                }

                DateTime[] dates = Provinces
                    .Select(x => x.ConfirmedCases)
                    .First()
                    .Select(x => x.Date)
                    .ToArray();

                int dateCount = dates.Count();
                var sumByDate = new int[dates.Count()];
                PlaceInfo[] provinceArray = Provinces.ToArray();

                var resultArray = new ConfirmedCase[dateCount];

                for (var i = 0; i < dateCount; i++)
                {
                    for (var j = 0; j < provinceArray.Count(); j++)
                    {
                        sumByDate[i] += provinceArray[j].ConfirmedCases.ToArray()[i].Count;
                    }

                    resultArray[i] = new ConfirmedCase 
                    { 
                        Date = dates[i], 
                        Count = sumByDate[i] 
                    };
                }

                return _confirmedCases = resultArray;
            }

            set => _confirmedCases = value;
        }
    }
}
