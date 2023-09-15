using System.Text.Json;

namespace WeatherAppUp13.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<string> GetWeatherPointGridDataAsync(double latitude, double longitude)
        {
            try
            {
                // Create an HttpClient instance using the factory
                var httpClient = _httpClientFactory.CreateClient("WeatherForecastClient");

                // Construct the full URL for the API endpoint
                string apiUrl = $"points/{latitude},{longitude}";

                // Send an HTTP GET request to the API endpoint
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Check if the response is successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                else
                {
                    // Handle the case where the request was not successful
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the request
                throw new Exception($"Error: {ex.Message}");
            }
        }        

        public async Task<string> GetWeatherForecastGridAsync(string office, double gridX, double gridY)
        {
            try
            {
                // Create an HttpClient instance using the factory
                var httpClient = _httpClientFactory.CreateClient("WeatherForecastClient");

                // Construct the full URL for the API endpoint
                string apiUrl = $"gridpoints/{office}/{gridX},{gridY}";

                // Send an HTTP GET request to the API endpoint
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Check if the response is successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                else
                {
                    // Handle the case where the request was not successful
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the request
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
