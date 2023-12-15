using Newtonsoft.Json;

namespace NJ.ApibToOasMapper.Tests.JsonHelpers
{
  public class StringNewLineJsonConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      if (value is string originalString)
      {
        var replaced = originalString.Replace("\r\n", "\n");
        writer.WriteValue(replaced);
      }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
      if (!CanConvert(objectType))
        throw new InvalidOperationException();
      var originalString = serializer.Deserialize<string>(reader);
      var result = originalString.Replace("\n", "\r\n");
      return result;
    }

    public override bool CanConvert(Type objectType) => objectType.IsAssignableTo(typeof(string));
  }
}
