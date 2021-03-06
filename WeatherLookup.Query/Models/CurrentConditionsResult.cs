﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherLookup.Query.Models
{
    public class CurrentConditionsResult
    {
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public double TempValue { get; set; }

        public string TempValueDisplay
        {
            get { return string.Format("{0}F", TempValue); }
        }
    }
}
