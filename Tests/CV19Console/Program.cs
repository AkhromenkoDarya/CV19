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

        private static readonly int _columnCountToSkip = 4;

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

                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(_columnCountToSkip)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        static void Main(string[] args)
        {
            // var client = new WebClient();

            // var client = new HttpClient();

            // HttpResponseMessage responseMessage = client.GetAsync(_dataUrl).Result;
            // string csvString = responseMessage.Content.ReadAsStringAsync().Result;

            // foreach (string dataLine in GetDataLines())
            // {
            //     Console.WriteLine(dataLine);
            // }

            DateTime[] dates = GetDates();
            Console.WriteLine(string.Join("\r\n", dates));
        }
    }
}
