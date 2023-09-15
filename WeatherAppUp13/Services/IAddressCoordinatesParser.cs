using WeatherAppUp13.Models;

namespace WeatherAppUp13.Services
{
    public interface IAddressCoordinatesParser
    {
        (double Latitude, double Longitude) Parse(string jsonResponse);
        string ParseUrlPropertyFromJson(string json);
        ForecastUrlInfo ExtractForecastUrlInfo(string url);
    }
}
