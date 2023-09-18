using Newtonsoft.Json;
using System.Globalization;

namespace WeatherAppUp13.Services
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var dateStr = reader.Value.ToString();
                DateTime parsedDateTime;

                if (TryParseInitialDateTime(dateStr, out parsedDateTime))
                {
                    return parsedDateTime;
                }
            }

            throw new JsonSerializationException($"Unexpected token or invalid date format: {reader.TokenType}");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private bool TryParseInitialDateTime(string input, out DateTime result)
        {
            var parts = input.Split('/');
            if (parts.Length == 2)
            {
                var initialDateTimeStr = parts[0];
                if (DateTime.TryParseExact(initialDateTimeStr, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}
