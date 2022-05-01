using CV19.Services.Interfaces;
using System;
using System.Threading;

namespace CV19.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int SleepTime = 5000;

        public string GetResult(DateTime time)
        {
            Thread.Sleep(SleepTime);

            return $"Result value: {time}";
        }
    }
}
