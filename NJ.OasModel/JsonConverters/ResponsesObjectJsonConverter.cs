using Newtonsoft.Json;

namespace NJ.OasModel.JsonConverters
{
  public class ResponsesObjectJsonConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      if (value is null)
        return;
      if (value is not ResponsesObject responsesObject)
        throw new InvalidOperationException();
      serializer.Serialize(writer, responsesObject.HttpStatusCodesWithResponses, typeof(IDictionary<string, IResponseOrReferenceObject>));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (!CanConvert(objectType))
        throw new InvalidOperationException();
      var result = new ResponsesObject
      {
        HttpStatusCodesWithResponses = serializer.Deserialize<IReadOnlyDictionary<string, IResponseOrReferenceObject>>(reader)
      };
      return result;
    }

    public override bool CanConvert(Type objectType) => objectType.IsAssignableFrom(typeof(ResponsesObject));
  }
}
