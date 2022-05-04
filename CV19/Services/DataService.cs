using CV19.Models;
using CV19.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Services
{
    internal class DataService : IDataService
    {
        private const string DataSourceAddress = @"https://raw.githubusercontent.com/" +
            @"CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/" +
            @"time_series_covid19_confirmed_global.csv";

        private const int ColumnCountBeforeDates = 4;

        private const int HeaderLineNumber = 1;

        private const string RegexPattern = "\\\"(.*?)\\\"";

        private const NumberStyles Style = NumberStyles.AllowDecimalPoint;

        private static readonly IFormatProvider Formatter = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(DataSourceAddress, 
                HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using Stream dataStream = (SynchronizationContext.Current is null 
                ? GetDataStream() 
                : Task.Run(GetDataStream))
                .Result;

            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                string line = dataReader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (Regex.IsMatch(line, RegexPattern))
                {
                    line = Regex.Replace(line, RegexPattern, x => x.Value
                        .Replace(",", "(").Replace("( ", " (")
                        .Insert(x.Value.LastIndexOf("\"", StringComparison.Ordinal), ")"));
                }

                yield return line;
            }
        }

        public static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(ColumnCountBeforeDates)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        public static IEnumerable<(string country, string province, (double latitdue, 
            double longitude) location, int[] confirmedCases)> GetCountryData()
        {
            IEnumerable<string[]> lines = GetDataLines()
                .Skip(HeaderLineNumber)
                .Select(line => line.Split(','));

            foreach (string[] row in lines)
            {
                string province = row[0].Trim(' ', '"');
                string country = row[1].Trim(' ', '"');

                double.TryParse(row[2], Style, Formatter, out double latitude);
                double.TryParse(row[3], Style, Formatter, out double longitude);

                int[] confirmedCases = row
                    .Skip(ColumnCountBeforeDates)
                    .Select(s => int.TryParse(s, out int convertedToInt) ? convertedToInt : 0)
                    .ToArray();

                yield return (country, province, (latitude, longitude), confirmedCases);
            }
        }

        public IEnumerable<CountryInfo> GetData()
        {
            DateTime[] dates = GetDates();

            var data = GetCountryData().GroupBy(x => x.country);

            foreach (var countryInfo in data)
            {
                var country = new CountryInfo
                {
                    Name = countryInfo.Key,
                    Provinces = countryInfo.Select(x => new PlaceInfo
                    {
                        Name = x.province,
                        Location = new Point(x.location.latitdue, x.location.longitude),
                        ConfirmedCases = dates.Zip(x.confirmedCases, (date, count) => 
                            new ConfirmedCase
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
