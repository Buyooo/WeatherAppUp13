using NUnit.Framework;
using WeatherAppUp13.Models;

namespace WeatherAppUp13.Services.Tests
{
    [TestFixture]
    public class AddressCoordinatesParserTests
    {
        [Test]
        public void Parse_ValidJsonResponse_ReturnsCoordinates()
        {
            // Arrange
            var parser = new AddressCoordinatesParser();
            var jsonResponse = "{\"result\": {\"addressMatches\": [{\"coordinates\": {\"x\": 123.45, \"y\": 67.89}}]}}";

            // Act
            var result = parser.Parse(jsonResponse);

            // Assert
            Assert.That(result.Longitude, Is.EqualTo(123.45));
            Assert.That(result.Latitude, Is.EqualTo(67.89));
        }

        [Test]
        public void Parse_InvalidJsonResponse_ThrowsException()
        {
            // Arrange
            var parser = new AddressCoordinatesParser();
            var jsonResponse = "{\"result\": {\"addressMatches\": []}}";

            // Act & Assert
            Assert.Throws<Exception>(() => parser.Parse(jsonResponse), "Coordinates not found in JSON response.");
        }

        [Test]
        public void ParseUrlPropertyFromJson_ValidJson_ReturnsUrl()
        {
            // Arrange
            var parser = new AddressCoordinatesParser();
            var json = "{\"properties\": {\"forecast\": \"https://api.weather.gov/gridpoints/ABR/164,70/forecast\"}}";

            // Act
            var result = parser.ParseUrlPropertyFromJson(json);

            // Assert
            Assert.That(result, Is.EqualTo("https://api.weather.gov/gridpoints/ABR/164,70/forecast"));
        }

        [Test]
        public void ParseUrlPropertyFromJson_InvalidJson_ReturnsNull()
        {
            // Arrange
            var parser = new AddressCoordinatesParser();
            var json = "{\"properties\": null}";

            // Act
            var result = parser.ParseUrlPropertyFromJson(json);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ExtractForecastUrlInfo_ValidUrl_ReturnsForecastUrlInfo()
        {
            // Arrange
            var parser = new AddressCoordinatesParser();
            var url = "https://api.weather.gov/gridpoints/ABR/164,70/forecast";

            // Act
            var result = parser.ExtractForecastUrlInfo(url);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Office, Is.EqualTo("ABR"));
            Assert.That(result.GridX, Is.EqualTo(164));
            Assert.That(result.GridY, Is.EqualTo(70));
        }

        [Test]
        public void ExtractForecastUrlInfo_InvalidUrl_ReturnsNull()
        {
            // Arrange
            var parser = new AddressCoordinatesParser();
            var url = "http://example.com/invalid/a/s/s/s";

            // Act
            var result = parser.ExtractForecastUrlInfo(url);

            // Assert
            Assert.IsNull(result);
        }
    }
}