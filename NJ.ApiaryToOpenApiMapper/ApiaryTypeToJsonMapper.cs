using Newtonsoft.Json.Linq;
using NJ.ApiaryToOpenApiMapper.Model;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class ApiaryTypeToJsonMapper
  {
    public static JToken MapApiaryTypeToJToken(ApiType apiaryType)
    {
      if (apiaryType is ApiPropertyType apiaryPropertyType)
      {
        var jObject = new JObject();
        foreach (var property in apiaryPropertyType.PropertiesWithParentProperties)
        {
          var value = SampleValueProvider.GetSampleValue(property);
          jObject.Add(property.PropertyName, JToken.FromObject(value));
        }
        return jObject;
      }
      else if (apiaryType is ApiArrayType apiaryArrayType)
      {
        var jArray = new JArray();
        var itemJToken = MapApiaryTypeToJToken(apiaryArrayType.ItemType);
        jArray.Add(itemJToken);
        return jArray;
      }

      throw new NotSupportedException();
    }
  }
}
