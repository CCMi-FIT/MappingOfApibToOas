using NJ.ApibModel;
using NJ.Common.Extensions;
using NJ.OasModel;
using System.Net.WebSockets;

namespace NJ.OasToApibMapper
{
  public static class OperationObjectMapper
  {
    public static ActionSection Map(string httpMethod, OperationObject operationObject)
    {
      var apibHttpRequestMethod = ParseHttpMethod(httpMethod);
      var responseSections = MapResponsesObject(operationObject.Responses);
      var requestBodySection = MapRequestObject(operationObject.RequestBody);
      var result = new ActionSection(default, operationObject.Description, apibHttpRequestMethod)
      {
        ResponseSections = responseSections.ToList(),
        RequestSections = requestBodySection?.ToList()
      };
      return result;
    }

    private static IEnumerable<RequestSection> MapRequestObject(IRequestBodyOrReferenceObject requestBodyOrReferenceObject)
    {
      if (requestBodyOrReferenceObject is null)
        return null;
      if (requestBodyOrReferenceObject is not RequestBodyObject requestBodyObject)
        throw new NotSupportedException();
      var content = requestBodyObject.Content;
      var result = new List<RequestSection>();
      foreach (var contentItem in content)
      {
        // TODO Article - mention this constraint
        if (content?.Count != 1)
          throw new NotSupportedException();
        var requestSection = new RequestSection
        {
          MediaType = contentItem.Key,
          BodySection = new BodySection(contentItem.Value.Example)
        };
        result.Add(requestSection);
      }
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

      ResponseSection result;
      var content = responseObject.Content;
      if (content.IsNotNullOrEmpty())
      {
        // ARTICLE TODO: Conversion Constraint
        var contentItem = responseObject.Content.Single();
        var mediaType = contentItem.Key;
        var example = contentItem.Value.Example;
        result = new ResponseSection(httpStatusCodeInt, mediaType) { BodySection = new BodySection(example) };
      }
      else
      {
        result = new ResponseSection(httpStatusCodeInt);
      }
      return result;
    }
  }
}
