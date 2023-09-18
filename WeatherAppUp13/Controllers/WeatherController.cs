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

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="weatherService">The weather service.</param>
        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        /// <summary>
        /// Retrieves weather point grid data based on latitude and longitude coordinates.
        /// </summary>
        /// <param name="latitude">The latitude coordinate.</param>
        /// <param name="longitude">The longitude coordinate.</param>
        /// <returns>A JSON response containing weather point grid data.</returns>
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

        /// <summary>
        /// Retrieves weather forecast grid data based on office, gridX, and gridY coordinates.
        /// </summary>
        /// <param name="office">The office identifier.</param>
        /// <param name="gridX">The gridX coordinate.</param>
        /// <param name="gridY">The gridY coordinate.</param>
        /// <returns>A JSON response containing weather forecast grid data.</returns>
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
