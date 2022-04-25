using CV19.Models;
using System.Collections.Generic;

namespace CV19.Services.Interfaces
{
    internal interface IDataService
    {
        public IEnumerable<CountryInfo> GetData();
    }
}
