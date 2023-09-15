using Microsoft.AspNetCore.Mvc;
using WeatherAppUp13.Services;

namespace WeatherAppUp13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocodingController : ControllerBase
    {
        private readonly IGeocodingService _geocodingService;

        public GeocodingController(IGeocodingService geocodingService)
        {
            _geocodingService = geocodingService ?? throw new ArgumentNullException(nameof(geocodingService));
        }

        [HttpGet("GeocodeOneLineAddress")]
        public async Task<IActionResult> GeocodeOneLineAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
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
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GeocodeAddress")]
        [HttpGet]
        public async Task<IActionResult> GeocodeAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest("Address parameter cannot be null or empty.");
            }

            try
            {
                var jsonResponse = await _geocodingService.GeocodeAddress(address);
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
