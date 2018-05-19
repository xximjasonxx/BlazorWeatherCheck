using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherLookup.Query.Models
{
    public class ForecastDayResult
    {
        public double DayHighTemp { get; set; }
        public double DayLowTemp { get; set; }
        public DateTime Date { get; set; }
        public int Icon { get; set; }
    }
}
