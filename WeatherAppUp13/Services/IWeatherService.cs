﻿namespace WeatherAppUp13.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeatherPointGridDataAsync(double latitude, double longitude);
        Task<string> GetWeatherForecastGridAsync(string office, double gridX, double gridY);
    }
}
