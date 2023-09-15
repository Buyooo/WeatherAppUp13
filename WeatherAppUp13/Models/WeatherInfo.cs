using Newtonsoft.Json;

namespace WeatherAppUp13.Models
{
    public class WeatherInfo
    {
        [JsonProperty("properties")]
        public WeatherProperties Properties { get; set; }
    }

    public class WeatherProperties
    {
        [JsonProperty("forecast")]
        public string ForecastUrl { get; set; }
    }
}
