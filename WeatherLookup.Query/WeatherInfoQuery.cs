using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherLookup.Query.Models;

namespace WeatherLookup.Query
{
    public class WeatherInfoQuery
    {
        private readonly ConfigurationService configService;
        private readonly HttpClient httpClient;

        public WeatherInfoQuery(ConfigurationService config, HttpClient client)
        {
            this.configService = config;
            this.httpClient = client;
        }

        public async Task<CurrentConditionsResult> GetCurrentConditions(string locationKey)
        {
            string requestUrl = $"http://dataservice.accuweather.com/currentconditions/v1/{locationKey}?apikey={this.configService.ApiKey}";
            string contentResponse = await this.httpClient.GetStringAsync(requestUrl);
            JArray jsonArray = JArray.Parse(contentResponse);
            if (jsonArray.Count == 0)
            {
                throw new Exception("No Conditions returned - possibly a bad location");
            }

            JObject conditionsJson = jsonArray.Children<JObject>().First();

            return new CurrentConditionsResult()
            {
                WeatherIcon = conditionsJson["WeatherIcon"].Value<int>(),
                WeatherText = conditionsJson["WeatherText"].Value<string>(),
                TempValue = conditionsJson["Temperature"]["Imperial"]["Value"].Value<double>()
            };
        }

        public async Task<List<ForecastDayResult>> Get5DayForecast(string locationKey)
        {
            string requestUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{locationKey}?apikey={this.configService.ApiKey}";
            string contentResponse = await this.httpClient.GetStringAsync(requestUrl);
            JObject resultObject = JObject.Parse(contentResponse);
            JArray forecastArray = (JArray)resultObject["DailyForecasts"];

            return forecastArray.Children<JObject>().Select(x => BuildDayResult(x)).ToList();
        }

        ForecastDayResult BuildDayResult(JObject dayJson)
        {
            return new ForecastDayResult()
            {
                DayHighTemp = dayJson["Temperature"]["Maximum"]["Value"].Value<double>(),
                DayLowTemp = dayJson["Temperature"]["Minimum"]["Value"].Value<double>(),
                Date = dayJson["Date"].Value<DateTime>(),
                Icon = dayJson["Day"]["Icon"].Value<int>()
            };
        }
    }
}
