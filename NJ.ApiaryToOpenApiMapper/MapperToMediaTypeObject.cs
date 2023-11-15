using Newtonsoft.Json;
using NJ.ApiaryAPIModel;
using NJ.ApiaryToOpenApiMapper.Model;
using NJ.OpenAPIModel;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class MapperToMediaTypeObject
  {
    public static MediaTypeObject MapToMediaTypeObject(string mediaType, string content, SchemaSection schemaSection, AttributesSection attributesSection, IReadOnlyCollection<ApiType> apiaryNamedTypes, bool mapExampleInSchema)
    {

      SchemaObject schema;
      dynamic example;

      if (schemaSection is not null)
      {
        // TODO Is it correct to ignore Example? Or is this just some kind of apib2swagger specific stuff?
        // - Is this some kind of extension we could introduce?
        // TODO: Will this always be JSON?
        schema = MapperToSchemaObject.Map(schemaSection);
        if (content is not null)
          example = MapContentToExample(content, mediaType);
        else if (attributesSection is not null)
          example = MapAttributesToExample(attributesSection, mediaType, apiaryNamedTypes);
        else
          example = default;
      }
      else if (attributesSection is not null)
      {
        var apiaryType = ApiaryTypesProvider.GetApiaryType(attributesSection, apiaryNamedTypes);
        schema =
          MapperToSchemaObject.MapFromApiaryType(apiaryType, mediaType, mapExampleInSchema);
        example = MapAttributesToExample(attributesSection, mediaType, apiaryNamedTypes);
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

    private static dynamic MapAttributesToExample(AttributesSection attributes, string mediaType, IReadOnlyCollection<ApiType> apiaryNamedTypes)
    {
      dynamic result = mediaType switch
      {
        null => null,
        "application/json" => MapAttributesToJsonExample(attributes, apiaryNamedTypes),
        _ => throw new NotSupportedException()
      };
      return result;
    }

    private static dynamic MapAttributesToJsonExample(AttributesSection attributes, IReadOnlyCollection<ApiType> apiaryNamedTypes)
    {
      var apiaryType = ApiaryTypesProvider.GetApiaryType(attributes, apiaryNamedTypes);
      var result = ApiaryTypeToJsonMapper.MapApiaryTypeToJToken(apiaryType);
      return result;
    }
  }
}
