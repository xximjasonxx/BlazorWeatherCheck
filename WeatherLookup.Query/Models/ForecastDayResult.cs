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

        public string DayHighTempDisplay
        {
            get { return string.Format("{0}F", DayHighTemp); }
        }

        public string DayLowTempDisplay
        {
            get { return string.Format("{0}F", DayLowTemp); }
        }
    }
}
