using Microsoft.AspNetCore.Mvc;

namespace WeatherAppUp13.Services
{
    public interface IGeocodingService
    {
        Task<string> GeocodeOneLineAddress(string address);
        Task<string> GeocodeAddress(string address);
    }
}
