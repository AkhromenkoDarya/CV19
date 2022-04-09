using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    class Program
    {
        private const string _dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/"
            + @"COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/" 
            + @"time_series_covid19_confirmed_global.csv";

        private const int _columnCountBeforeDates = 4;

        private const int _headerLineNumber = 1;

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_dataUrl, HttpCompletionOption.ResponseHeadersRead);
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

                yield return TransformCountryName(line, "Bonaire") 
                    ?? TransformCountryName(line, "Helena") 
                    ?? TransformCountryName(line, "Korea")
                    ?? line;
            }
        }

        private static string TransformCountryName(string line, string countryName)
        {
            return line.Contains(countryName) 
                ? line.Replace($"{countryName},", $"{countryName} -") 
                : null;
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(_columnCountBeforeDates)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string country, string province, int[] infectedCount)> GetData()
        {
            IEnumerable<string[]> lines = GetDataLines()
                .Skip(_headerLineNumber)
                .Select(line => line.Split(','));

            var province = string.Empty;
            var country = string.Empty;
            var infectedCount = new int[] { 0 };

            foreach (string[] row in lines)
            {
                province = row[0].Trim();
                country = row[1].Trim(' ', '\'', '"');
                infectedCount = row
                    .Skip(_columnCountBeforeDates)
                    .Select(s => int.Parse(s))
                    .ToArray();

                yield return (country, province, infectedCount);
            }
        }

        static void Main(string[] args)
        {
            //DateTime[] dates = GetDates();
            //Console.WriteLine(string.Join("\r\n", dates));

            var russiaData = GetData()
                .First(data => data.country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russiaData.infectedCount, (date, 
                count) => $"{date:dd:MM:yyyy} - {count}")));

            Console.ReadLine();
        }
    }
}
