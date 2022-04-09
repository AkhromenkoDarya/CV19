using System.Collections.Generic;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfoBase
    {
        public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }
}
