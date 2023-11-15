using Newtonsoft.Json;

namespace NJ.OpenAPIModel.JsonConverters
{
  public class PathsObjectJsonConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      if (value is null)
        return;
      if (value is not PathsObject pathsObject)
        throw new InvalidOperationException();
      serializer.Serialize(writer, pathsObject.PathItems, typeof(IDictionary<string, PathItemObject>));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (!CanConvert(objectType))
        throw new InvalidOperationException();
      var result = new PathsObject
      {
        PathItems = serializer.Deserialize<IReadOnlyDictionary<string, PathItemObject>>(reader)
      };
      return result;
    }

    public override bool CanConvert(Type objectType) => objectType.IsAssignableFrom(typeof(PathsObject));
  }
}
