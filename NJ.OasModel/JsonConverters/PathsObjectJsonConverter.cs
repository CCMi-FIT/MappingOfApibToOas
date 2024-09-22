using Newtonsoft.Json;
using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel.JsonConverters
{
  public class PathsObjectJsonConverter : JsonConverter<PathsObject>
  {
    public override PathsObject? ReadJson(JsonReader reader, Type objectType, PathsObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var result = new PathsObject
      {
        PathItems = serializer.Deserialize<IReadOnlyDictionary<PathString, PathItemObject>>(reader)
      };
      return result;
    }

    public override void WriteJson(JsonWriter writer, PathsObject? value, JsonSerializer serializer)
    {
      if (value is null)
        throw new ArgumentNullException(nameof(value));

      writer.WriteStartObject();

      foreach (var kvp in value.PathItems)
      {
        writer.WritePropertyName(SerializePathString(kvp.Key, serializer));
        serializer.Serialize(writer, kvp.Value);
      }

      writer.WriteEndObject();
    }

    private string SerializePathString(PathString pathString, JsonSerializer serializer)
    {
      var stringWriter = new StringWriter();
      using (var jsonWriter = new JsonTextWriter(stringWriter))
        serializer.Serialize(jsonWriter, pathString);
      var serializedKey = stringWriter.ToString();
      if (serializedKey.StartsWith("\"") && serializedKey.EndsWith("\""))
        serializedKey = serializedKey.Substring(1, serializedKey.Length - 2);

      return serializedKey;
    }
  }
}
