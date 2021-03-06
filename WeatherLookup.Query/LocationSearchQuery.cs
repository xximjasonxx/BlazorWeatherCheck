﻿using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherLookup.Query.Models;

namespace WeatherLookup.Query
{
    public class LocationSearchQuery
    {
        private readonly ConfigurationService configService;
        private readonly HttpClient httpClient;

        public LocationSearchQuery(ConfigurationService cService, HttpClient client)
        {
            this.configService = cService;
            this.httpClient = client;
        }

        public async Task<LocationResult> SearchLocations(string searchTerm)
        {
            string resultString = string.Empty;
            if (Regex.IsMatch(searchTerm, @"^\d\d\d\d\d$"))
            {
                resultString = await GetKeyForPostalCode(searchTerm);
            }
            else
            {
                resultString = await GetKeyForCity(searchTerm);
            }

            if (string.IsNullOrEmpty(resultString))
            {
                throw new Exception("No contents received");
            }

            var jsonResponse = JArray.Parse(resultString);
            return new LocationResult
            {
                LocationKey = jsonResponse[0]["Key"].Value<string>(),
                Name = jsonResponse[0]["EnglishName"].Value<string>(),
                RegionName = jsonResponse[0]["AdministrativeArea"]["EnglishName"].Value<string>(),
                PostalCode = jsonResponse[0]["PrimaryPostalCode"].Value<string>()
            };
        }

        async Task<string> GetKeyForPostalCode(string postalCode)
        {
            const string requestUrlBase = "http://dataservice.accuweather.com/locations/v1/postalcodes/search";
            string queryUrl = $"{requestUrlBase}?apikey={this.configService.ApiKey}&q={postalCode}";

            return await this.httpClient.GetStringAsync(queryUrl);
        }

        async Task<string> GetKeyForCity(string cityText)
        {
            const string requestUrlBase = "http://dataservice.accuweather.com/locations/v1/cities/search";
            string queryUrl = $"{requestUrlBase}?apikey={this.configService.ApiKey}&q={cityText}";

            return await this.httpClient.GetStringAsync(queryUrl);
        }
    }
}
