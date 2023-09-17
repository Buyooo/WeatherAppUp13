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
    }
}
