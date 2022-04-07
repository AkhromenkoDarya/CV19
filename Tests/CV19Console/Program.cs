using System.Net.Http;

namespace CV19Console
{
    class Program
    {
        private const string _dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/"
            + @"COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/" 
            + @"time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            // var client = new WebClient();

            var client = new HttpClient();

            HttpResponseMessage responseMessage = client.GetAsync(_dataUrl).Result;
            string csvString = responseMessage.Content.ReadAsStringAsync().Result;

            // Console.WriteLine(csvString);
        }
    }
}
