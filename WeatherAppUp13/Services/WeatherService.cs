using WeatherAppUp13.Models;

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
                var httpClient = _httpClientFactory.CreateClient("WeatherForecastClient");

                string apiUrl = $"points/{latitude},{longitude}";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }        

        public async Task<string> GetWeatherForecastGridAsync(string office, double gridX, double gridY)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("WeatherForecastClient");

                string apiUrl = $"gridpoints/{office}/{gridX},{gridY}";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public IList<DailyWeatherAverages> CalculateUniqueDailyAverages(WeatherProperties weatherProperties)
        {
            var dailyAverages = new List<DailyWeatherAverages>();

            // Group temperature data by day
            var groupedData = weatherProperties.Temperature.Values
                .GroupBy(data => data.ValidTime.Date);

            foreach (var group in groupedData)
            {
                var dailyAverage = new DailyWeatherAverages
                {
                    Date = group.Key
                };

                if (group.Any())
                {
                    dailyAverage.TemperatureAverage = group.Average(data => data.GetValueOrDefault());
                }

                foreach (var property in typeof(WeatherProperties).GetProperties())
                {
                    if (property.Name != "Temperature" && property.PropertyType == typeof(WeatherData))
                    {
                        var propertyName = property.Name;
                        var weatherData = (WeatherData)property.GetValue(weatherProperties);

                        var propertyValues = weatherData.Values;

                        var propertyAverage = propertyValues
                            .Where(data => data.ValidTime.Date == group.Key)
                            .Select(data => data.GetValueOrDefault())
                            .DefaultIfEmpty() // Handle empty data
                            .Average();

                        typeof(DailyWeatherAverages).GetProperty($"{propertyName}Average")?.SetValue(dailyAverage, propertyAverage);
                    }
                }

                dailyAverages.Add(dailyAverage);
            }

            var uniqueDailyAverages = dailyAverages
                .GroupBy(average => average.Date.Date)
                .Select(group => group.First())
                .ToList();

            return uniqueDailyAverages;
        }
    }
}
