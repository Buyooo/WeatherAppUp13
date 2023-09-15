using Microsoft.AspNetCore.Mvc;
using WeatherAppUp13.Services;

namespace WeatherAppUp13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        [HttpPost("GridData")]
        public async Task<IActionResult> GetWeatherPointGridDataAsync(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
            {
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
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("GridForecast")]
        public async Task<IActionResult> GetWeatherForecastGridAsync(string office, double gridX, double gridY)
        {
            if (string.IsNullOrEmpty(office) || gridX < 0 || gridY < 0)
            {
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
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
