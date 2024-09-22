using Newtonsoft.Json;
using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.OasModel.JsonConverters
{
  public class ResponsesObjectJsonConverter : JsonConverter<ResponsesObject>
  {
    public override void WriteJson(JsonWriter writer, ResponsesObject value, JsonSerializer serializer)
    {
      if (value is null)
        throw new ArgumentNullException(nameof(value));

      writer.WriteStartObject();

      foreach (var kvp in value.HttpStatusCodesWithResponses)
      {
        writer.WritePropertyName(SerializeHttpStatusCodePattern(kvp.Key, serializer));
        serializer.Serialize(writer, kvp.Value);
      }

      writer.WriteEndObject();
    }

    public override ResponsesObject? ReadJson(JsonReader reader, Type objectType, ResponsesObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var result = new ResponsesObject
      {
        HttpStatusCodesWithResponses = serializer.Deserialize<IReadOnlyDictionary<HttpStatusCodePattern, IResponseOrReferenceObject>>(reader)
      };
      return result;
    }

    private string SerializeHttpStatusCodePattern(HttpStatusCodePattern httpStatusCodePattern, JsonSerializer serializer)
    {
      var stringWriter = new StringWriter();
      using (var jsonWriter = new JsonTextWriter(stringWriter))
        serializer.Serialize(jsonWriter, httpStatusCodePattern);
      var serializedKey = stringWriter.ToString();
      if (serializedKey.StartsWith("\"") && serializedKey.EndsWith("\""))
        serializedKey = serializedKey.Substring(1, serializedKey.Length - 2);

      return serializedKey;
    }
  }
}
