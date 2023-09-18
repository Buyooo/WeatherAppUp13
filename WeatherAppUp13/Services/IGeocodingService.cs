namespace WeatherAppUp13.Services
{
    public interface IGeocodingService
    {
        Task<string> GeocodeOneLineAddress(string address);
        Task<string> GeocodeAddress(string? street = null, string? city = null, string? state = null, string? zip = null);
    }
}
