using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherAppUp13.Models;

namespace WeatherAppUp13.Services
{
    public class AddressCoordinatesParser : IAddressCoordinatesParser
    {
        public (double Latitude, double Longitude) Parse(string jsonResponse)
        {
            try
            {
                var jObject = JObject.Parse(jsonResponse);

                var addressMatches = jObject["result"]["addressMatches"];

                if (addressMatches != null && addressMatches.HasValues)
                {
                    var firstMatch = addressMatches.First;

                    var coordinates = firstMatch["coordinates"];

                    if (coordinates != null)
                    {
                        var latitude = coordinates["y"].Value<double>();
                        var longitude = coordinates["x"].Value<double>();

                        return (latitude, longitude);
                    }
                }

                throw new Exception("Coordinates not found in JSON response.");
            }
            catch (Exception ex)
            {
                // Handle parsing errors or missing data
                Console.WriteLine($"Error parsing coordinates: {ex.Message}");
                throw;
            }
        }

        public string ParseUrlPropertyFromJson(string json)
        {
            try
            {
                // Deserialize the JSON into the WeatherInfo object
                var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(json);

                // Access the forecast URL from the parsed object
                string forecastUrl = weatherInfo?.Properties?.ForecastUrl;

                return forecastUrl;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during parsing
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return null;
            }
        }

        public ForecastUrlInfo ExtractForecastUrlInfo(string url)
        {
            // Split the URL by '/' to get individual parts
            string[] urlParts = url.Split('/');

            if (urlParts.Length >= 4)
            {
                // The office is the third part of the URL
                string office = urlParts[4];

                // The gridX and gridY are the fourth part of the URL, split by ','
                string[] gridParts = urlParts[5].Split(',');

                if (gridParts.Length == 2 && int.TryParse(gridParts[0], out int gridX) && int.TryParse(gridParts[1], out int gridY))
                {
                    // Create and return the ForecastUrlInfo object
                    return new ForecastUrlInfo
                    {
                        Office = office,
                        GridX = gridX,
                        GridY = gridY
                    };
                }
            }

            // Return null if parsing fails
            return null;
        }
    }
}
