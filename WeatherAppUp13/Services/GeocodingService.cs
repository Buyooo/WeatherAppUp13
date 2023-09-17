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
                var queryString = $"locations/onelineaddress?address={Uri.EscapeDataString(address)}&benchmark=2020&format=json";

                var httpClient = _httpClientFactory.CreateClient("GeocodingClient");

                var response = await httpClient.GetAsync($"{queryString}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return jsonResponse;
                }
                else
                {
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

        public async Task<string> GeocodeAddress(string? street = null, string? city = null, string? state = null, string? zip = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(street) && (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(state)) && string.IsNullOrWhiteSpace(zip))
                {
                    throw new ArgumentException("Specify either a complete street address with city and state or a valid ZIP code.");
                }

                // Create a query string with the optional parameters
                var queryString = "locations/address?";

                if (!string.IsNullOrEmpty(street))
                {
                    queryString += $"street={Uri.EscapeDataString(street)}&";
                }

                if (!string.IsNullOrEmpty(city))
                {
                    queryString += $"city={Uri.EscapeDataString(city)}&";
                }

                if (!string.IsNullOrEmpty(state))
                {
                    queryString += $"state={Uri.EscapeDataString(state)}&";
                }

                if (!string.IsNullOrEmpty(zip))
                {
                    queryString += $"zip={Uri.EscapeDataString(zip)}&";
                }

                queryString += "benchmark=2020&format=json";

                var httpClient = _httpClientFactory.CreateClient("GeocodingClient");

                var response = await httpClient.GetAsync(queryString);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return jsonResponse;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        }

    }
}
