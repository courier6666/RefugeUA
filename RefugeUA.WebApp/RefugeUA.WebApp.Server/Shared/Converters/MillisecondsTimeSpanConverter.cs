using System.Text.Json;
using System.Text.Json.Serialization;

namespace RefugeUA.WebApp.Server.Shared.Converters
{
    public class MillisecondsTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var totalMilliseconds = reader.GetInt64();

            return TimeSpan.FromMilliseconds(totalMilliseconds);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            var totalMilliseconds = value.TotalMilliseconds;

            writer.WriteNumberValue(totalMilliseconds);
        }
    }
}
