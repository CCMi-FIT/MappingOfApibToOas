using Newtonsoft.Json;
using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel.JsonConverters
{
  public class PathStringJsonConverter : JsonConverter<PathString>
  {
    public override PathString ReadJson(JsonReader reader, Type objectType, PathString existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var @string = serializer.Deserialize<string>(reader)!;
      var result = new PathString(@string);
      return result;
    }

    public override void WriteJson(JsonWriter writer, PathString value, JsonSerializer serializer)
    {
      serializer.Serialize(writer, value.String, typeof(string));
    }
  }
}
