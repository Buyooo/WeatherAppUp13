using Newtonsoft.Json;

namespace WeatherAppUp13.Models
{
    public class WeatherInfo
    {
        [JsonProperty("properties")]
        public WeatherGridProperties Properties { get; set; }
    }

    public class WeatherGridProperties
    {
        [JsonProperty("forecast")]
        public string ForecastUrl { get; set; }
    }
}
