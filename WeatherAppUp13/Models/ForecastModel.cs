using System;
using System.Collections.Generic;

namespace WeatherAppUp13.Models
{
    public class Forecast
    {
        public string? Updated { get; set; }
        public string? Units { get; set; }
        public string? ForecastGenerator { get; set; }
        public string? GeneratedAt { get; set; }
        public string? UpdateTime { get; set; }
        public string? ValidTimes { get; set; }
        public Elevation? Elevation { get; set; }
        public List<ForecastPeriod>? Periods { get; set; }
    }

    public class Elevation
    {
        public string? UnitCode { get; set; }
        public double? Value { get; set; }
    }

    public class ForecastPeriod
    {
        public int? Number { get; set; }
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsDaytime { get; set; }
        public int? Temperature { get; set; }
        public string? TemperatureUnit { get; set; }
        public string? TemperatureTrend { get; set; }
        public Precipitation? ProbabilityOfPrecipitation { get; set; }
        public Dewpoint? Dewpoint { get; set; }
        public RelativeHumidity? RelativeHumidity { get; set; }
        public string? WindSpeed { get; set; }
        public string? WindDirection { get; set; }
        public string? Icon { get; set; }
        public string? ShortForecast { get; set; }
        public string? DetailedForecast { get; set; }
    }

    public class Precipitation
    {
        public string? UnitCode { get; set; }
        public int? Value { get; set; }
    }

    public class Dewpoint
    {
        public string? UnitCode { get; set; }
        public double? Value { get; set; }
    }

    public class RelativeHumidity
    {
        public string? UnitCode { get; set; }
        public int? Value { get; set; }
    }
}
