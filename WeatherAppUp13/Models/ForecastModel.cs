using Newtonsoft.Json;
using WeatherAppUp13.Services;

namespace WeatherAppUp13.Models
{
    public class WeatherForecast
    {
        public WeatherProperties Properties { get; set; }
    }

    public class WeatherProperties
    {
        public string UpdateTime { get; set; }
        public string ValidTimes { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData Temperature { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData Dewpoint { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData MaxTemperature { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData MinTemperature { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData RelativeHumidity { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData HeatIndex { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData WindChill { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData SkyCover { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData WindDirection { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData WindSpeed { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData WindGust { get; set; }

        [JsonConverter(typeof(UnitOfMeasurementConverter))]
        public WeatherData ProbabilityOfPrecipitation { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("uom")]
        public string UnitOfMeasurement { get; set; }
        public List<WeatherMeasurement> Values { get; set; }
    }

    public class WeatherMeasurement
    {
        [JsonProperty("ValidTime")]
        public string ValidTime { get; set; }

        [JsonProperty("Value")]
        public double? Value { get; set; }

        public double GetValueOrDefault(double defaultValue = 0.0)
        {
            return Value ?? defaultValue;
        }
    }
}