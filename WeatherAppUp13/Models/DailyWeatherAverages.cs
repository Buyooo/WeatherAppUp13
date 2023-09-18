namespace WeatherAppUp13.Models
{
    public class DailyWeatherAverages
    {
        public DateTime Date { get; set; }
        public double TemperatureAverage { get; set; }
        public double DewpointAverage { get; set; }
        public double MaxTemperatureAverage { get; set; }
        public double MinTemperatureAverage { get; set; }
        public double RelativeHumidityAverage { get; set; }
        public double HeatIndexAverage { get; set; }
        public double WindChillAverage { get; set; }
        public double SkyCoverAverage { get; set; }
        public double WindDirectionAverage { get; set; }
        public double WindSpeedAverage { get; set; }
        public double WindGustAverage { get; set; }
        public double ProbabilityOfPrecipitationAverage { get; set; }
    }
}
