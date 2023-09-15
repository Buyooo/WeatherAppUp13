using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAppUp13.Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GeocodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<string> GeocodeOneLineAddress(string address)
        {
            try
            {
                // Create a query string with the required parameters
                var queryString = $"locations/onelineaddress?address={Uri.EscapeDataString(address)}&benchmark=2020&format=json";

                // Create an HttpClient instance using the HttpClientFactory
                var httpClient = _httpClientFactory.CreateClient("GeocodingClient");

                // Send the GET request to the Geocoding API
                var response = await httpClient.GetAsync($"{queryString}");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the JSON response as a string
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Return the JSON as a string
                    return jsonResponse;
                }
                else
                {
                    // Handle the error response if the request was not successful
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<string> GeocodeAddress(string address)
        {
            try
            {
                // Create a query string with the required parameters
                var queryString = $"locations/address?zip={Uri.EscapeDataString(address)}&benchmark=2020&format=json";

                // Create an HttpClient instance using the HttpClientFactory
                var httpClient = _httpClientFactory.CreateClient("GeocodingClient");

                // Send the GET request to the Geocoding API
                var response = await httpClient.GetAsync($"{queryString}");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the JSON response as a string
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Return the JSON as a string
                    return jsonResponse;
                }
                else
                {
                    // Handle the error response if the request was not successful
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
