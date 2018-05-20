using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherLookup
{
    public static class ProcessController
    {
        public static string GetWeatherIconImageUrl(int iconNumber)
        {
            return string.Format("https://developer.accuweather.com/sites/default/files/{0}-s.png",
                iconNumber.ToString().PadLeft(2, '0'));
        }
    }
}
