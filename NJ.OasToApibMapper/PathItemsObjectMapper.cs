using NJ.ApibModel;
using NJ.OasModel;
using System.Net.WebSockets;

namespace NJ.OasToApibMapper
{
  public static class PathItemsObjectMapper
  {
    public static IEnumerable<ResourceSection> Map(PathsObject paths)
    {
      foreach (var pathItem in paths.PathItems)
      {
        var pathString = pathItem.Key;
        var pathItemObject = pathItem.Value;
        var uriTemplate = new UriTemplate(pathString);
        var actionSections = MapPathItemObject(pathItemObject, uriTemplate).ToList();
        var resourceSection = new ResourceSection
        {
          UriTemplate = uriTemplate,
          ActionSections = actionSections
        };
        yield return resourceSection;
      }
    }

    private static IReadOnlyCollection<ActionSection> MapPathItemObject(PathItemObject pathItemObject, UriTemplate uriTemplate)
    {
      var httpMethodsWithOperationObjects = new (string HttpMethod, OperationObject OperationObject)[]
      {
        ("Get", pathItemObject.Get),
        ("Post", pathItemObject.Post),
        ("Put", pathItemObject.Put),
        ("Patch", pathItemObject.Patch),
        ("Delete", pathItemObject.Delete),
        ("Options", pathItemObject.Options),
        ("Head", pathItemObject.Head),
        ("Trace", pathItemObject.Trace)
      }.Where(i => i.OperationObject is not null);

      var result = httpMethodsWithOperationObjects.Select(i => MapOperationObject(i.HttpMethod, i.OperationObject)).ToList();
      return result;
    }

    private static ActionSection MapOperationObject(string httpMethod, OperationObject operationObject)
    {
      var apibHttpRequestMethod = ParseHttpMethod(httpMethod);
      var responseSections = MapResponsesObject(operationObject.Responses);
      var result = new ActionSection(default, operationObject.Description, apibHttpRequestMethod)
      {
        ResponseSections = responseSections.ToList()
      };
      return result;
    }

    private static HttpRequestMethod ParseHttpMethod(string httpMethod)
    {
      var result = httpMethod switch
      {
        "Get" => HttpRequestMethod.Get,
        "Post" => HttpRequestMethod.Post,
        "Put" => HttpRequestMethod.Put,
        "Delete" => HttpRequestMethod.Delete,
        "Options" => HttpRequestMethod.Options,
        "Head" => HttpRequestMethod.Head,
        "Patch" => HttpRequestMethod.Patch,
        "Trace" => HttpRequestMethod.Trace,
        _ => throw new NotSupportedException()
      };
      return result;
    }

    private static IEnumerable<ResponseSection> MapResponsesObject(ResponsesObject responsesObject)
    {
      var result = responsesObject.HttpStatusCodesWithResponses.Select(i => MapResponseObject(i.Key, i.Value));
      return result;
    }

    private static ResponseSection MapResponseObject(string httpStatusCode, IResponseOrReferenceObject responseOrReferenceObject)
    {
      if (responseOrReferenceObject is not ResponseObject responseObject)
        throw new InvalidOperationException();

      var httpStatusCodeInt = int.Parse(httpStatusCode);

      // Conversion Constraint
      var contentItem = responseObject.Content.Single();
      var mediaType = contentItem.Key;
      var example = contentItem.Value.Example;
      var result = new ResponseSection(httpStatusCodeInt, mediaType) { BodySection = new BodySection(example) };
      return result;
    }
  }
}