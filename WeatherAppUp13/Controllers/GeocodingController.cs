using Microsoft.AspNetCore.Mvc;

namespace WeatherAppUp13.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GeocodingController : ControllerBase
    {
        private const string GeocodingApiUrl = "https://geocoding.geo.census.gov/geocoder/locations/onelineaddress";

        private readonly IHttpClientFactory _httpClientFactory;

        public GeocodingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("GeocodeOneLineAddress")]
        public async Task<IActionResult> GeocodeOneLineAddress(string address)
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
                    // Read the JSON response
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a strongly-typed object if needed
                    // For now, let's return the JSON as-is
                    return Ok(jsonResponse);
                }
                else
                {
                    // Handle the error response if the request was not successful
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GeocodeAddress")]
        [HttpGet]
        public async Task<IActionResult> GeocodeAddress(string address)
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
                    // Read the JSON response
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a strongly-typed object if needed
                    // For now, let's return the JSON as-is
                    return Ok(jsonResponse);
                }
                else
                {
                    // Handle the error response if the request was not successful
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
