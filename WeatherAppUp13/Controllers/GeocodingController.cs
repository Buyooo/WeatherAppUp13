using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAppUp13.Models;
using WeatherAppUp13.Services;

namespace WeatherAppUp13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocodingController : ControllerBase
    {
        private readonly IGeocodingService _geocodingService;
        private readonly IWeatherService _weatherService;
        private readonly IAddressCoordinatesParser _addressCoordinatesParser;
        private readonly ILogger<GeocodingController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingController"/> class.
        /// </summary>
        /// <param name="geocodingService">The geocoding service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="weatherService">The weather service.</param>
        /// <param name="addressCoordinatesParser">The address coordinates parser.</param>
        public GeocodingController(IGeocodingService geocodingService, ILogger<GeocodingController> logger, IWeatherService weatherService, IAddressCoordinatesParser addressCoordinatesParser)
        {
            _geocodingService = geocodingService ?? throw new ArgumentNullException(nameof(geocodingService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _addressCoordinatesParser = addressCoordinatesParser ?? throw new ArgumentException(nameof(addressCoordinatesParser));
            _logger = logger;            
        }

        /// <summary>
        /// Retrieves a 7-day weather forecast based on the provided address.
        /// </summary>
        /// <param name="address">The address to geocode and retrieve a forecast for.</param>
        /// <returns>A JSON response containing the 7-day weather forecast.</returns>
        [HttpGet("Get7DayForecast")]
        public async Task<IActionResult> Get7DayForecast(string address)
        {
            try
            {
                if (address == null || string.IsNullOrWhiteSpace(address))
                {
                    _logger.LogError("Address parameter cannot be null or empty.");
                    return BadRequest("Address parameter cannot be null or empty.");
                }

                // Geocode the address to get latitude and longitude
                var geocodeResponse = await _geocodingService.GeocodeOneLineAddress(address);

                // Parse the geocodeResponse to retrieve latitude and longitude
                (double Latitude, double Longitude) geocodeResult;

                try
                {
                    geocodeResult = _addressCoordinatesParser.Parse(geocodeResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error parsing geocode response.");
                    return StatusCode(500, "Internal Server Error: Error parsing geocode response.");
                }

                // Get the 7-day forecast URL using the latitude and longitude
                var jsonResponse = await _weatherService.GetWeatherPointGridDataAsync(geocodeResult.Latitude, geocodeResult.Longitude);

                // Deserialize the JSON response
                var forecastUrl = _addressCoordinatesParser.ParseUrlPropertyFromJson(jsonResponse);

                var forecastUrlInfo = _addressCoordinatesParser.ExtractForecastUrlInfo(forecastUrl);

                if (forecastUrl == null)
                {
                    _logger.LogError("Unable to parse forecast properties.");
                    return StatusCode(500, "Internal Server Error: Unable to parse forecast properties.");
                }

                var completeForecastData = await _weatherService.GetWeatherForecastGridAsync(forecastUrlInfo.Office!, forecastUrlInfo.GridX, forecastUrlInfo.GridY);

                var forecast = JsonConvert.DeserializeObject<WeatherForecast>(completeForecastData);

                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(forecast),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the 7-day forecast.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Geocodes a one-line address.
        /// </summary>
        /// <param name="address">The one-line address to geocode.</param>
        /// <returns>A JSON response containing geocoding information.</returns>
        [HttpGet("GeocodeOneLineAddress")]
        public async Task<IActionResult> GeocodeOneLineAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                _logger.LogError("Address parameter cannot be null or empty.");
                return BadRequest("Address parameter cannot be null or empty.");
            }

            try
            {
                var jsonResponse = await _geocodingService.GeocodeOneLineAddress(address);
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while geocoding one-line address.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Geocodes an address using individual address components.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The ZIP code.</param>
        /// <returns>A JSON response containing geocoding information.</returns>
        [HttpGet("GeocodeAddress")]
        public async Task<IActionResult> GeocodeAddress(string? street = null, string? city = null, string? state = null, string? zip = null)
        {
            if (string.IsNullOrEmpty(street) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(state) && string.IsNullOrEmpty(zip))
            {
                _logger.LogError("At least one parameter (street, city, state, or zip) must be provided.");
                return BadRequest("At least one parameter (street, city, state, or zip) must be provided.");
            }

            try
            {
                var jsonResponse = await _geocodingService.GeocodeAddress(street, city, state, zip);
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while geocoding address.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
