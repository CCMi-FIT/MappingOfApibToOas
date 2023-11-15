using Newtonsoft.Json;
using NJ.OasModel.JsonConverters;

namespace NJ.OasModel;

[JsonConverter(typeof(ResponsesObjectJsonConverter))]
public class ResponsesObject
{
  // TODO Default
  public IResponseOrReferenceObject Default { get; init; }
  public IReadOnlyDictionary<string, IResponseOrReferenceObject> HttpStatusCodesWithResponses { get; init; }
}