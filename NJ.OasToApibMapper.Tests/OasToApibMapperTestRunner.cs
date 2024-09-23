using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJ.ApibModel;
using NJ.OasModel;
using NJ.OasToApibMapper.Tests.JsonHelpers;

namespace NJ.OasToApibMapper.Tests
{
  public static class OasToApibMapperTestRunner
  {
    public static void RunTest(Apib expectedApib, string pathToOasJsonFile)
    {
      throw new NotImplementedException();
      //var jsonSettings = new JsonSerializerSettings
      //{
      //  NullValueHandling = NullValueHandling.Ignore,
      //  DefaultValueHandling = DefaultValueHandling.Ignore,
      //  Converters = new List<JsonConverter> { new StringNewLineJsonConverter() }
      //};

      //var result = ApibToOasMapper.Map(expectedApib);
      //var resultJson = JsonConvert.SerializeObject(result, jsonSettings);
      //var expectedJson = File.ReadAllText(pathToOasJsonFile);

      //var resultJObject = JsonHelper.DeserializeWithLowerCase(resultJson);
      //var expectedResultJObject = JsonHelper.DeserializeWithLowerCase(expectedJson);
      //var equals = JToken.EqualityComparer.Equals(resultJObject, expectedResultJObject);

      //Assert.True(equals);
    }

    public static void RunTest(OpenApiObject oas, Apib expectedApib)
    {
      var result = OpenApiObjectMapper.Map(oas);
      result.Should().BeEquivalentTo(expectedApib);
    }
  }
}
