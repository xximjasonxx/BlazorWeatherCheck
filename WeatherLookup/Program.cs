using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using WeatherLookup.Query;

namespace WeatherLookup
{
    public class Program
    {
        static void Main(string[] args)
        {
            /* var builder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "settings.json"), optional: false);

            var configuration = builder.Build();
            var apiKey = configuration.GetSection("creds")["apikey"]; */

            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton(new ConfigurationService("KZMOg2RXf7bAN22rOrAotZ4wOqa7IA9W"));
                services.AddTransient<LocationSearchQuery>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
