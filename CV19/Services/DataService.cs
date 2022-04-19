using CV19.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19.Services
{
    internal static class DataService
    {
        private const string _dataSourceAddress = @"https://raw.githubusercontent.com/"
            + @"CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/"
            + @"time_series_covid19_confirmed_global.csv";

        private static string[] _incorrectPlaceNames = { "Bonaire", "Helena", "Korea" };

        private const int _columnCountBeforeDates = 4;

        private const int _headerLineNumber = 1;

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_dataSourceAddress, 
                HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using Stream dataStream = GetDataStream().Result;
            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                yield return FixPlaceName(line);
            }
        }

        private static string FixPlaceName(string line)
        {
            foreach (string incorrectCountryName in _incorrectPlaceNames)
            {
                if (line.Contains(incorrectCountryName))
                {
                    return line.Replace($"{incorrectCountryName},", $"{incorrectCountryName} -");
                }
            }

            return line;
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(_columnCountBeforeDates)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string country, string province, (double latitdue, 
            double longitude) place, int[] infectedCount)> GetCountryData()
        {
            IEnumerable<string[]> lines = GetDataLines()
                .Skip(_headerLineNumber)
                .Select(line => line.Split(','));

            foreach (string[] row in lines)
            {
                string province = row[0].Trim();
                string country = row[1].Trim(' ', '\'', '"');
                double latitude = double.Parse(row[2]);
                double longitude = double.Parse(row[3]);
                int[] infectedCount = row
                    .Skip(_columnCountBeforeDates)
                    .Select(s => int.Parse(s))
                    .ToArray();

                yield return (country, province, (latitude, longitude), infectedCount);
            }
        }

        public static IEnumerable<CountryInfo> GetData()
        {
            DateTime[] dates = GetDates();

            var data = GetCountryData().GroupBy(x => x.country);

            foreach (var countryInfo in data)
            {
                var country = new CountryInfo
                {
                    Name = countryInfo.Key,
                    ProvinceCount = countryInfo.Select(x => new PlaceInfo
                    {
                        Name = x.province,
                        Location = new Point(x.place.latitdue, x.place.longitude),
                        InfectedCount = dates.Zip(x.infectedCount, (date, count) 
                            => new ConfirmedCount
                            {
                                Date = date,
                                Count = count
                            })
                    })
                };

                yield return country;
            }
        }
    }
}
