using Newtonsoft.Json;
using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class MapperToMediaTypeObject
  {
    public static MediaTypeObject MapToMediaTypeObject(string mediaType, string content, SchemaSection schemaSection, AttributesSection attributesSection, IReadOnlyCollection<ApiType> apiNamedTypes, bool mapExampleInSchema)
    {

      SchemaObject schema;
      dynamic example;

      if (schemaSection is not null)
      {
        schema = MapperToSchemaObject.Map(schemaSection);
        if (content is not null)
          example = MapContentToExample(content, mediaType);
        else if (attributesSection is not null)
          example = MapAttributesToExample(attributesSection, schemaSection, mediaType, apiNamedTypes);
        else
          example = default;
      }
      else if (attributesSection is not null)
      {
        var apiType = ApiTypesProvider.GetApiType(attributesSection, apiNamedTypes);
        schema =
          MapperToSchemaObject.MapFromApiType(apiType, mediaType, mapExampleInSchema);
        example = MapAttributesToExample(attributesSection, mediaType, apiNamedTypes);
      }
      else if (content is not null)
      {
        schema = MapperToSchemaObject.Map(content, mediaType);
        example = MapContentToExample(content, mediaType);
      }
      else
      {
        schema = default;
        example = default;
      }

      var mediaTypeObject = new MediaTypeObject
      {
        Schema = schema,
        Example = example,
      };

      return mediaTypeObject;
    }

    private static dynamic MapContentToExample(string content, string mediaType)
    {
      dynamic result = mediaType switch
      {
        null => null,
        "text/plain" => content,
        "application/json" => content is not null ? JsonConvert.DeserializeObject<dynamic>(content) : null,
        _ => throw new NotSupportedException()
      };
      return result;
    }

    private static dynamic MapAttributesToExample(AttributesSection attributes, string mediaType, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      dynamic result = mediaType switch
      {
        null => null,
        "application/json" => MapAttributesToJsonExample(attributes, apiNamedTypes),
        _ => throw new NotSupportedException()
      };
      return result;
    }

    private static dynamic MapAttributesToExample(AttributesSection attributes, SchemaSection schemaSection, string mediaType, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      dynamic result = mediaType switch
      {
        null => null,
        "application/json" => MapAttributesToJsonExample(attributes, schemaSection, apiNamedTypes),
        _ => throw new NotSupportedException()
      };
      return result;
    }

    private static dynamic MapAttributesToJsonExample(AttributesSection attributes, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var apiType = ApiTypesProvider.GetApiType(attributes, apiNamedTypes);
      var result = TypeToJsonMapper.MapTypeToJToken(apiType);
      return result;
    }

    private static dynamic MapAttributesToJsonExample(AttributesSection attributes, SchemaSection schemaSection, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var apiType = ApiTypesProvider.GetApiType(attributes, schemaSection, apiNamedTypes);
      var result = TypeToJsonMapper.MapTypeToJToken(apiType);
      return result;
    }
  }
}
