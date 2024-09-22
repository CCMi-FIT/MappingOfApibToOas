using Newtonsoft.Json;
using NJ.SharedModel;

namespace NJ.OasModel.JsonConverters
{
  public class MediaTypeJsonConverter : JsonConverter<MediaType>
  {
    public override MediaType ReadJson(JsonReader reader, Type objectType, MediaType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var pattern = serializer.Deserialize<string>(reader)!;
      var result = new MediaType(pattern);
      return result;
    }

    public override void WriteJson(JsonWriter writer, MediaType value, JsonSerializer serializer)
    {
      serializer.Serialize(writer, value.Pattern, typeof(string));
    }
  }
}
