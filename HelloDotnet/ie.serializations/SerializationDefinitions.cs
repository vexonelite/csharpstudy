using System;
using System.Collections.Generic;   // need for IList<T>, Dictionary<K, V>
using System.Text.Json;
using ie.delegates.reactives;

namespace ie.serializations
{
    public class WeatherForecastWithPOCOs {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
        public string SummaryField;
        public IList<DateTimeOffset> DatesAvailable { get; set; }
        public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; }
        public string[] SummaryWords { get; set; }
    }

    public class HighLowTemps {
        public int High { get; set; }
        public int Low { get; set; }
    }

    public class WeatherForecast {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }

        public override string ToString() {
            return "WeatherForecast {Date: " + Date + ", TemperatureCelsius: " + TemperatureCelsius + ", Summary: " + Summary + "}";
        }
    }

    public class TestJsonSerialization : IRunnable {
        public void run() {
            // https://docs.microsoft.com/zh-tw/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-core-3-1
            WeatherForecast weatherForecast = new WeatherForecast();
            weatherForecast.Date = DateTimeOffset.Now;
            weatherForecast.TemperatureCelsius = 25;
            weatherForecast.Summary = "Hot";
            var options = new JsonSerializerOptions {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(weatherForecast, options);
            Console.WriteLine("Serialized - jsonString: {0}", jsonString);

            ///

            WeatherForecast weatherForecast2 = JsonSerializer.Deserialize<WeatherForecast>(jsonString);
            Console.WriteLine("deserialize： {0}", weatherForecast2);
        }
    }
}

