using Microsoft.AspNetCore.Mvc;
using WeatherAppUp13.Services;

namespace WeatherAppUp13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpPost("GridData")]
        public async Task<IActionResult> GetWeatherPointGridDataAsync(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
            {
                _logger.LogError("Invalid latitude or longitude values.");
                return BadRequest("Invalid latitude or longitude values.");
            }

            try
            {
                var jsonResponse = await _weatherService.GetWeatherPointGridDataAsync(latitude, longitude);
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting weather point grid data.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("GridForecast")]
        public async Task<IActionResult> GetWeatherForecastGridAsync(string office, double gridX, double gridY)
        {
            if (string.IsNullOrEmpty(office) || gridX < 0 || gridY < 0)
            {
                _logger.LogError("Invalid input parameters.");
                return BadRequest("Invalid input parameters.");
            }

            try
            {
                var jsonResponse = await _weatherService.GetWeatherForecastGridAsync(office, gridX, gridY);
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting weather forecast grid data.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
