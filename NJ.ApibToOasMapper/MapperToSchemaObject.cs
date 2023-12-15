using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class MapperToSchemaObject
  {
    public static SchemaObject Map(SchemaSection schemaSection)
    {
      var schemaJObject = JObject.Parse(schemaSection.Schema);
      var result = new SchemaObject
      {
        Properties = schemaJObject["properties"]?.ToObject<dynamic>(),
        Type = schemaJObject["type"]?.ToString(),
        AdditionalProperties = schemaJObject["allowAdditionalProperties"]?.ToString() == "true",
        Description = schemaJObject["description"]?.ToString()
      };
      return result;
    }

    public static SchemaObject Map(string content, string mediaTypeString = null)
    {
      var result = mediaTypeString switch
      {
        null => new SchemaObject { Type = "string" },
        "application/json" => MapJsonContentToSchemaObject(content),
        "text/plain" => null,
        _ => throw new NotSupportedException()
      };
      return result;
    }

    public static SchemaObject MapFromApiType(ApiType apiType, string mediaTypeString, bool mapExample)
    {
      var result = mediaTypeString switch
      {
        null => new SchemaObject { Type = "string" },
        "application/json" => MapAttributesSectionToSchemaObject(apiType, mapExample),
        "text/plain" => null,
        _ => throw new NotSupportedException()
      };
      return result;
    }

    private static SchemaObject MapAttributesSectionToSchemaObject(ApiType apiType, bool mapExample)
    {
      string type;
      dynamic items = default;
      dynamic properties = default;
      ICollection<string> required = default;


      if (apiType is ApiArrayType)
      {
        type = "array";
        items = new object();
      }
      else if (apiType is ApiPropertyType apiPropertyType)
      {
        type = "object";
        var propertiesDictionary = new Dictionary<string, dynamic>();
        foreach (var property in apiPropertyType.PropertiesWithParentProperties)
        {
          var jObject = new JObject { { "type", property.TypeName } };
          if (property.Description is not null)
            jObject.Add("description", property.Description);
          if (mapExample && property.SampleValue is not null)
          {
            var jTokenExample = JToken.FromObject(property.SampleValue);
            jObject.Add("example", jTokenExample);
          }
          var jObjectString = jObject.ToString();
          propertiesDictionary[property.PropertyName] = JsonConvert.DeserializeObject<dynamic>(jObjectString);
        }
        properties = propertiesDictionary;
        var requiredProperties = apiPropertyType.Properties.Where(p => p.Required).ToList();
        if (requiredProperties.Count > 0)
          required = requiredProperties.Select(p => p.PropertyName).ToList();
      }
      else
        throw new NotSupportedException();

      var result = new SchemaObject
      {
        Type = type,
        Items = items,
        Properties = properties,
        Required = required
      };

      return result;
    }

    private static SchemaObject MapJsonContentToSchemaObject(string content)
    {
      var sampleJsonSchemaGenerator = new NJsonSchema.Generation.SampleJsonSchemaGenerator();
      var dollarSchema = sampleJsonSchemaGenerator.Generate(content).ToJson();
      var parsedDollarSchema = JObject.Parse(dollarSchema);
      parsedDollarSchema.Remove("$schema");
      ReplaceIntegerTypeWithNumber(parsedDollarSchema);

      dynamic properties;
      var propertiesString = parsedDollarSchema.GetValue("properties")?.ToString();
      if (propertiesString is not null)
        properties = JsonConvert.DeserializeObject<dynamic>(propertiesString);
      else
        properties = default;

      var result = new SchemaObject
      {
        Type = parsedDollarSchema.GetValue("type")?.ToString(),
        Properties = properties
      };
      return result;
    }

    private static readonly JValue IntegerJValue = new JValue("integer");

    private static void ReplaceIntegerTypeWithNumber(JObject jObject)
    {
      foreach (var kv in new List<KeyValuePair<string, JToken>>(jObject))
      {
        if (kv.Key == "type" && IntegerJValue.Equals(kv.Value))
        {
          jObject.Remove("type");
          jObject.Add("type", new JValue("number"));
        }
        else if (kv.Value is JObject jObjectValue)
          ReplaceIntegerTypeWithNumber(jObjectValue);
      }
    }
  }
}
