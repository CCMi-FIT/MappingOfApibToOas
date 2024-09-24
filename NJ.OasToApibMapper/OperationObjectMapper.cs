using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper
{
  public static class OperationObjectMapper
  {
    public static ActionSection Map(string httpMethod, OperationObject operationObject)
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
