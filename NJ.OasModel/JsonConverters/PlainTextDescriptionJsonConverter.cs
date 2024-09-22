using Newtonsoft.Json;
using NJ.SharedModel;

namespace NJ.OasModel.JsonConverters
{
  public class PlainTextDescriptionJsonConverter : JsonConverter<Description>
  {
    public override Description? ReadJson(JsonReader reader, Type objectType, Description? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var text = serializer.Deserialize<string>(reader)!;
      var result = new PlainTextDescription(text);
      return result;
    }

    public override void WriteJson(JsonWriter writer, Description? value, JsonSerializer serializer)
    {
      serializer.Serialize(writer, value!.Text, typeof(string));
    }
  }
}
