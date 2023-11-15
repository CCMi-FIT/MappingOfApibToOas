using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJ.ApibModel;
using NJ.ApibToOasMapper.Tests.JsonConverters;

namespace NJ.ApibToOasMapper.Tests
{
  public static class ApibToOasMapperTestRunner
  {
    public static void RunTest(Apib apib, string pathToExpectedOasJsonFile)
    {
      var jsonSettings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Converters = new List<JsonConverter> { new StringNewLineJsonConverter() }
      };

      var result = ApibToOasMapper.Map(apib);
      var resultJson = JsonConvert.SerializeObject(result, jsonSettings);
      var expectedJson = File.ReadAllText(pathToExpectedOasJsonFile);

      var resultJObject = JObject.Parse(resultJson);
      var expectedResultJObject = JObject.Parse(expectedJson);
      var equals = JObject.EqualityComparer.Equals(resultJObject, expectedResultJObject);

      Assert.True(equals);
    }
  }
}
