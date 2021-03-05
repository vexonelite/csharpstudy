using System;
using System.Collections.Generic;   // need for IList<T>, Dictionary<K, V>
using System.Text.Json;
using ie.delegates.reactives;


namespace ie.serializations
{
    public class WeatherForecastWithPOCOs {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public int TemperatureCelsius { get; set; } = 0;
        public string Summary { get; set; } = "";
        public string SummaryField;
        public IList<DateTimeOffset> DatesAvailable { get; set; } = new List<DateTimeOffset>();
        public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; } = new Dictionary<string, HighLowTemps>();
        public string[] SummaryWords { get; set; }
    }

    public class HighLowTemps {
        public int High { get; set; } = 0;
        public int Low { get; set; } = 0;
    }

    public class WeatherForecast {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public int TemperatureCelsius { get; set; } = 0;
        public string Summary { get; set; } = "";

        public override string ToString() {
            return "WeatherForecast {Date: " + Date + ", TemperatureCelsius: " + TemperatureCelsius + ", Summary: " + Summary + "}";
        }
    }

    public class TestJsonSerialization1 : IRunnable {
        public void run() {
            // https://docs.microsoft.com/zh-tw/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-core-3-1
            // WeatherForecast weatherForecast = new WeatherForecast();
            // weatherForecast.Date = DateTimeOffset.Now;
            // weatherForecast.TemperatureCelsius = 25;
            // weatherForecast.Summary = "Hot";
            WeatherForecast weatherForecast = new WeatherForecast() {
                Date = DateTimeOffset.Now,
                TemperatureCelsius = 25,
                Summary = "Hot"
            };
            var options = new AppJsonSerializerOptionFactory().getOption1();            
            string jsonString = JsonSerializer.Serialize(weatherForecast, options);
            Console.WriteLine("Serialized - jsonString: {0}", jsonString);

            ///

            WeatherForecast weatherForecast2 = JsonSerializer.Deserialize<WeatherForecast>(jsonString);
            Console.WriteLine("deserialize： {0}", weatherForecast2);
        }
    }

    public class AppJsonSerializerOptionFactory {
        public JsonSerializerOptions getOption1() {
            return new JsonSerializerOptions { WriteIndented = true, };
        }
    }

    ///

    public class SsoFileBackup {
        public string id { get; set; } = "";
        public string comment { get; set; } = "";
        public string created_at { get; set; } = "";
        public string updated_at { get; set; } = "";
        public int version { get; set; } = 0;
       
        public override string ToString() {           
            return "SsoFileBackup { id: " + id + ", comment: " + comment + ", created_at: " + created_at + ", updated_at: " + updated_at + ", version: " + version + " }";
        }
    }


    public class SsoFileBackupList {
        public string status { get; set; } = "";
        public string message { get; set; } = "";
        public List<SsoFileBackupDongle> data { get; set; } = new List<SsoFileBackupDongle>();

        public override string ToString() {
            return "SsoFileBackupDongle { status: " + status + ", message: " + message + ", size of data: " + data.Count + " }";
        }
    }
    
    public class SsoFileBackupDongle {
        public int profile_id { get; set; } = Int32.MinValue;
        public string dongle_sn { get; set; } = "";
        public List<SsoFileBackup> backups { get; set; } = new List<SsoFileBackup>();

        public override string ToString() {
            return "SsoFileBackupDongle { ProfileId: " + profile_id + ", KessilId: " + dongle_sn + ", size of backups: " + backups.Count + " }";
        }
    }

    public class SsoLoginRequest {
        public string userToken { get; set; } = "";
        public string tokenSecret { get; set; } = "";
        public string userId { get; set; } = "";
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public string picture_url { get; set; } = "";

        public override string ToString() {
            return "SsoLoginRequest { userToken: " + userToken + ", tokenSecret: " + tokenSecret + ", userId: " + userId + ", name: " + name
                + ", email: " + email + ", picture_url: " + picture_url + " }";
        } 
    }

    public class SsoLoginResponse {
        public string status { get; set; } = "";
        public string message { get; set; } = "";
        public string auth_token { get; set; } = "";
        public bool is_new_user { get; set; } = false;
        public SsoLoginRequest payload { get; set; } = new SsoLoginRequest();

        public override string ToString() {
            return "LoginResponse { statusCode: " + status + ", message: " + message + ", accessToken: " + auth_token
                + ", isNewUser: " + is_new_user + ", payload: " + payload + " }";
        }
    }
}

