using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;
using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;
using OneOf;

namespace NJ.ApibToOasMapper
{
  public static class ActionSectionMapper
  {
    public static OperationObject Map(ActionSection actionSection, ResourceSection resourceSection, ResourceGroupSection resourceGroupSection, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var (requestBodyObject, requestBodyParameters) = MapRequestSections(actionSection, apiNamedTypes);
      var responseSectionsGroupedByStatusCode = actionSection.ResponseSections.GroupBy(s => s.HttpStatusCode);
      var statusCodesWithResponseObjects = responseSectionsGroupedByStatusCode.ToDictionary(g => new SharedModel.HttpStatusCodePattern(g.Key), g => (OneOf<ResponseObject, ReferenceObject>)MapToResponseObject(g.Key, g, apiNamedTypes));
      var responses = new ResponsesObject { HttpStatusCodesWithResponses = statusCodesWithResponseObjects };
      var parameterObjectsFromUriTemplate = MapUriTemplate(actionSection, resourceSection).Cast<OneOf<ParameterObject, ReferenceObject>>();

      var tags = ResourceGroupSectionToTagsMapper.MapToStrings(new[] { resourceGroupSection });
      var parameters = requestBodyParameters.Concat(parameterObjectsFromUriTemplate).ToList();
      var result = new OperationObject(tags, parameters)
      {
        Summary = actionSection.Identifier ?? "",
        Description = actionSection.Description,
        OperationId = actionSection.Identifier ?? "",
        RequestBody = requestBodyObject,
        Responses = responses
      };
      return result;
    }

    private static ResponseObject MapToResponseObject(int statusCode, IEnumerable<ResponseSection> responseSections, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      IReadOnlyCollection<ResponseSection> responseSectionCollection = responseSections.ToList();
      var descriptionString = Constants.StatusCodesWithDescriptions[statusCode];
      var description = new PlainTextDescription(descriptionString);
      var result = new ResponseObject(description)
      {
        Headers = responseSectionCollection.SelectMany(r => MapHeaderSectionToNamesWithHeaderObjects(r.HeadersSection)).DistinctBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value),
        Content = responseSectionCollection.Where(r => r.MediaType is not null).ToDictionary(r => r.MediaType, r => MapperToMediaTypeObject.MapToMediaTypeObject(r.MediaType, r.BodySection?.Content, r.SchemaSection, r.AttributesSection, apiNamedTypes, false))
      };

      return result;
    }

    private static IReadOnlyDictionary<string, OneOf<HeaderObject, ReferenceObject>> MapHeaderSectionToNamesWithHeaderObjects(HeadersSection headersSection)
    {
      if (headersSection is null)
        return new Dictionary<string, OneOf<HeaderObject, ReferenceObject>>();
      var result = headersSection.ToDictionary(h => h.Key, h => MapHeaderToHeaderObject(h.Value));
      return result;
    }

    private static OneOf<HeaderObject, ReferenceObject> MapHeaderToHeaderObject(object headerValue)
    {
      var typeString = headerValue switch
      {
        int => "string",
        string => "string",
        _ => "object"
      };
      // TODO: Can RulesForSerialization be different ?
      var schema = new SchemaObject { Type = typeString };
      var headerObject = new HeaderObject
      {
        RulesForSerialization = new ParameterSchemaStyleRulesForSerialization(schema, ParameterIn.Header)
      };
      return headerObject;
    }

    private static IEnumerable<ParameterObject> MapUriTemplate(ActionSection actionSection, ResourceSection resourceSection)
    {
      List<UriParameter> pathParameters;
      if (actionSection.ParametersSection?.Parameters is not null)
        pathParameters = actionSection.ParametersSection.Parameters.ToList();
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
      ParameterIn @in;
      if (parsedPathParameters.Contains(parameter.Name))
        @in = ParameterIn.Path;
      else
        @in = ParameterIn.Query;

      var schema = new SchemaObject { Type = parameter.Type, Default = parameter.DefaultValue };
      var rulesForSerialization = new ParameterSchemaStyleRulesForSerialization(schema, @in) { Example = parameter.ExampleValue?.ToString() };
      var result = new ParameterObject(parameter.Name, @in, rulesForSerialization)
      {
        Description = parameter.Description,
        Required = parameter.Required,
      };
      return result;
    }

    public static (RequestBodyObject RequestBodyObject, IList<OneOf<ParameterObject, ReferenceObject>> ParameterObjects) MapRequestSections(ActionSection actionSection, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      // TODO: APIB Transactions don't have direct equivalent in OAS
      var requestSections = actionSection.RequestSections;
      if (requestSections is null)
        return (null, new List<OneOf<ParameterObject, ReferenceObject>>());
      var parameters = requestSections.Where(r => r.HeadersSection is not null).SelectMany(r => r.HeadersSection.Select(kv => MapHeaderSectionKeyValueToParameter(kv.Key, kv.Value))).ToList();
      var mediaTypesWithMediaTypeObjects = requestSections.Where(r => r.MediaType is not null).ToDictionary(r => (MediaRange)r.MediaType, r => MapRequestSection(r, actionSection, apiNamedTypes));
      var requestBodyObject = mediaTypesWithMediaTypeObjects.Count > 0
        ? new RequestBodyObject(mediaTypesWithMediaTypeObjects)
        : null;
      return (requestBodyObject, parameters);
    }

    private static OneOf<ParameterObject, ReferenceObject> MapHeaderSectionKeyValueToParameter(string key, object value)
    {
      var @in = ParameterIn.Header;
      var schema = MapperToSchemaObject.Map(value?.ToString());
      var rulesForSerialization = new ParameterSchemaStyleRulesForSerialization(schema, @in)
      {
        Example = value
      };
      var descriptionText = $"e.g. {value}";
      var description = new PlainTextDescription(descriptionText);
      var result = new ParameterObject(key, @in, rulesForSerialization)
      {
        Description = description,
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