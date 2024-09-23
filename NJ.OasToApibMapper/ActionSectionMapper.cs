using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class ActionSectionMapper
  {
    public static OperationObject Map(ActionSection actionSection, ResourceSection resourceSection, ResourceGroupSection resourceGroupSection, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var (requestBodyObject, requestBodyParameters) = MapRequestSections(actionSection, apiNamedTypes);
      var responseSectionsGroupedByStatusCode = actionSection.ResponseSections.GroupBy(s => s.HttpStatusCode);
      var statusCodesWithResponseObjects = responseSectionsGroupedByStatusCode.ToDictionary(g => g.Key.ToString(), g => (IResponseOrReferenceObject)MapToResponseObject(g.Key, g, apiNamedTypes));
      var responses = new ResponsesObject { HttpStatusCodesWithResponses = statusCodesWithResponseObjects };
      var parameterObjectsFromUriTemplate = MapUriTemplate(actionSection, resourceSection);

      var result = new OperationObject
      {
        Summary = actionSection.Identifier ?? "",
        Description = actionSection.Description ?? "",
        OperationId = actionSection.Identifier ?? "",
        RequestBody = requestBodyObject,
        Tags = ResourceGroupSectionToTagsMapper.MapToStrings(new[] { resourceGroupSection }),
        Responses = responses,
        Parameters = requestBodyParameters.Concat(parameterObjectsFromUriTemplate).ToList()
      };
      return result;
    }

    private static ResponseObject MapToResponseObject(int statusCode, IEnumerable<ResponseSection> responseSections, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      IReadOnlyCollection<ResponseSection> responseSectionCollection = responseSections.ToList();
      var result = new ResponseObject
      {
        Description = Constants.StatusCodesWithDescriptions[statusCode],
        Headers = responseSectionCollection.SelectMany(r => MapHeaderSectionToNamesWithHeaderObjects(r.HeadersSection)).DistinctBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value),
        Content = responseSectionCollection.Where(r => r.MediaType is not null).ToDictionary(r => r.MediaType, r => MapperToMediaTypeObject.MapToMediaTypeObject(r.MediaType, r.BodySection?.Content, r.SchemaSection, r.AttributesSection, apiNamedTypes, false))
      };

      return result;
    }

    private static IReadOnlyDictionary<string, IHeaderOrReferenceObject> MapHeaderSectionToNamesWithHeaderObjects(HeadersSection headersSection)
    {
      if (headersSection is null)
        return new Dictionary<string, IHeaderOrReferenceObject>();
      var result = headersSection.ToDictionary(h => h.Key, h => MapHeaderToHeaderObject(h.Value));
      return result;
    }

    private static IHeaderOrReferenceObject MapHeaderToHeaderObject(object headerValue)
    {
      var typeString = headerValue switch
      {
        int => "string",
        string => "string",
        _ => "object"
      };
      var headerObject = new HeaderObject
      {
        Schema = new SchemaObject { Type = typeString }
      };
      return headerObject;
    }

    private static IEnumerable<ParameterObject> MapUriTemplate(ActionSection actionSection, ResourceSection resourceSection)
    {
      List<UriParameter> pathParameters;
      if (actionSection.UriParametersSection?.Parameters is not null)
        pathParameters = actionSection.UriParametersSection.Parameters.ToList();
      else if (resourceSection.ParametersSection?.Parameters is not null)
        pathParameters = resourceSection.ParametersSection.Parameters.ToList();
      else
        pathParameters = new List<UriParameter>();

      ICollection<string> parsedPathParameters;
      if (actionSection.UriTemplate?.Path is not null)
        parsedPathParameters = UriTemplateParametersParser.ParsePathParameters(actionSection.UriTemplate.Path).ToList();
      else
        parsedPathParameters = UriTemplateParametersParser.ParsePathParameters(resourceSection.UriTemplate.Path).ToList();

      var result = pathParameters.Select(p => MapParameterToParameterObject(p, parsedPathParameters));
      return result;
    }

    private static ParameterObject MapParameterToParameterObject(UriParameter parameter, ICollection<string> parsedPathParameters)
    {
      string @in;
      if (parsedPathParameters.Contains(parameter.Name))
        @in = "path";
      else
        @in = "query";

      var result = new ParameterObject
      {
        Name = parameter.Name,
        In = @in,
        Description = parameter.Description,
        Required = parameter.Required,
        Example = parameter.ExampleValue?.ToString(),
        Schema = new SchemaObject { Type = parameter.Type, Default = parameter.DefaultValue },
      };
      return result;
    }

    public static (RequestBodyObject RequestBodyObject, IList<IParameterOrReferenceObject> ParameterObjects) MapRequestSections(ActionSection actionSection, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var requestSections = actionSection.RequestSections;
      if (requestSections is null)
        return (null, new List<IParameterOrReferenceObject>());
      var parameters = requestSections.Where(r => r.HeadersSection is not null).SelectMany(r => r.HeadersSection.Select(kv => MapHeaderSectionKeyValueToParameter(kv.Key, kv.Value))).ToList();
      var mediaTypesWithMediaTypeObjects = requestSections.Where(r => r.MediaType is not null).ToDictionary(r => r.MediaType, r => MapRequestSection(r, actionSection, apiNamedTypes));
      var requestBodyObject = mediaTypesWithMediaTypeObjects.Count > 0
        ? new RequestBodyObject { Content = mediaTypesWithMediaTypeObjects }
        : null;
      return (requestBodyObject, parameters);
    }

    private static IParameterOrReferenceObject MapHeaderSectionKeyValueToParameter(string key, object value)
    {
      var result = new ParameterObject
      {
        Name = key,
        In = "header",
        Description = $"e.g. {value}",
        Required = false,
        Example = value,
        Schema = MapperToSchemaObject.Map(value?.ToString())
      };
      return result;
    }

    public static MediaTypeObject MapRequestSection(RequestSection requestSection, ActionSection actionSection, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var content = requestSection.BodySection?.Content;
      var attributesSection = requestSection.AttributesSection ?? actionSection.AttributesSection;
      var result = MapperToMediaTypeObject.MapToMediaTypeObject(requestSection.MediaType, content, requestSection.SchemaSection, attributesSection, apiNamedTypes, true);
      return result;
    }
  }
}