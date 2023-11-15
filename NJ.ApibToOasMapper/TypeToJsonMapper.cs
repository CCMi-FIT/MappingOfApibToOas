using Newtonsoft.Json.Linq;
using NJ.ApibToOasMapper.Model;

namespace NJ.ApibToOasMapper
{
  public static class TypeToJsonMapper
  {
    public static JToken MapTypeToJToken(ApiType apiType)
    {
      if (apiType is ApiPropertyType apiPropertyType)
      {
        var jObject = new JObject();
        foreach (var property in apiPropertyType.PropertiesWithParentProperties)
        {
          var value = SampleValueProvider.GetSampleValue(property);
          jObject.Add(property.PropertyName, JToken.FromObject(value));
        }
        return jObject;
      }
      else if (apiType is ApiArrayType apiArrayType)
      {
        var jArray = new JArray();
        var itemJToken = MapTypeToJToken(apiArrayType.ItemType);
        jArray.Add(itemJToken);
        return jArray;
      }

      throw new NotSupportedException();
    }
  }
}
