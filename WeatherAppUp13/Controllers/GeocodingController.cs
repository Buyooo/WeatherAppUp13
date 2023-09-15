using Microsoft.AspNetCore.Mvc;
using WeatherAppUp13.Services;

namespace WeatherAppUp13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocodingController : ControllerBase
    {
        private readonly IGeocodingService _geocodingService;
        private readonly ILogger<GeocodingController> _logger;

        public GeocodingController(IGeocodingService geocodingService, ILogger<GeocodingController> logger)
        {
            _geocodingService = geocodingService ?? throw new ArgumentNullException(nameof(geocodingService));
            _logger = logger;
        }

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

        [HttpGet("GeocodeAddress")]
        [HttpGet]
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
