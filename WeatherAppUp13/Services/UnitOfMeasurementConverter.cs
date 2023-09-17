using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WeatherAppUp13.Models;

namespace WeatherAppUp13.Services
{
    public class UnitOfMeasurementConverter : JsonConverter<WeatherData>
    {
        public override WeatherData ReadJson(JsonReader reader, Type objectType, WeatherData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject obj = JObject.Load(reader);
                var uom = obj["uom"]?.Value<string>();
                if (!string.IsNullOrEmpty(uom))
                {
                    switch (uom)
                    {
                        case "wmoUnit:degC":
                            uom = "°C";
                            break;
                        case "wmoUnit:percent":
                            uom = "%";
                            break;
                        case "wmoUnit:degree_(angle)":
                            uom = "°";
                            break;
                        case "wmoUnit:km_h-1":
                            uom = "km/h";
                            break;
                    }
                    obj["uom"] = uom;
                }
                return obj.ToObject<WeatherData>(serializer);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, WeatherData value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}
