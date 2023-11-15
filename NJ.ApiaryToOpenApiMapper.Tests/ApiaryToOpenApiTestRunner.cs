using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJ.ApiaryAPIModel;
using NJ.ApiaryToOpenApiMapper.Tests.JsonConverters;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public static class ApiaryToOpenApiTestRunner
  {
    public static void RunTest(ApiaryApiBlueprint apiaryApiBlueprint, string pathToExpectedOpenApiJsonFile)
    {
      var jsonSettings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Converters = new List<JsonConverter> { new StringNewLineJsonConverter() }
      };

      var result = ApiaryApiBlueprintToOpenApiMapper.Map(apiaryApiBlueprint);
      var resultJson = JsonConvert.SerializeObject(result, jsonSettings);
      var expectedJson = File.ReadAllText(pathToExpectedOpenApiJsonFile);

      var resultJObject = JObject.Parse(resultJson);
      var expectedResultJObject = JObject.Parse(expectedJson);
      var equals = JObject.EqualityComparer.Equals(resultJObject, expectedResultJObject);

      Assert.True(equals);
    }
  }
}
