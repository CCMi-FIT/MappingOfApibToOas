using Newtonsoft.Json;
using NJ.OasModel.JsonConverters;
using NJ.SharedModel;

namespace NJ.OasModel;

// TODO: Must contain at least one ReposneObject, if only one, it should be response for valid call - any from Default / HttpStatusCodesWithResponse ?
[JsonConverter(typeof(ResponsesObjectJsonConverter))]
public class ResponsesObject
{
  public IResponseOrReferenceObject? Default { get; init; }
  // TODO: only keys 100-599 + 'x' support (like 1xx) are supported
  public IReadOnlyDictionary<HttpStatusCodePattern, IResponseOrReferenceObject>? HttpStatusCodesWithResponses { get; init; }
}