using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherLookup.Query.Models
{
    public class LocationResult
    {
        public string LocationKey { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string PostalCode { get; set; }
    }
}
