using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NJ.ApibToOasMapper.Tests.JsonHelpers
{
  public static class JsonHelper
  {
    public static JObject DeserializeWithLowerCasePropertyNames(string json)
    {
      using var textReader = new StringReader(json);
      using var jsonReader = new LowerCasePropertyNameJsonReader(textReader);

      var ser = new JsonSerializer
      {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore
      };
      ser.Converters.Add(new StringNewLineJsonConverter());
      var result = ser.Deserialize<JObject>(jsonReader);
      return result;
    }
  }
}
