using Newtonsoft.Json;
using NJ.OpenAPIModel.JsonConverters;

namespace NJ.OpenAPIModel;

[JsonConverter(typeof(ResponsesObjectJsonConverter))]
public class ResponsesObject
{
  // TODO Default
  public IResponseOrReferenceObject Default { get; init; }
  public IReadOnlyDictionary<string, IResponseOrReferenceObject> HttpStatusCodesWithResponses { get; init; }
}