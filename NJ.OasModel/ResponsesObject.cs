using Newtonsoft.Json;
using NJ.OasModel.JsonConverters;
using NJ.SharedModel;
using OneOf;

namespace NJ.OasModel;

// TODO: Must contain at least one ReposneObject, if only one, it should be response for valid call - any from Default / HttpStatusCodesWithResponse ?
[JsonConverter(typeof(ResponsesObjectJsonConverter))]
public class ResponsesObject
{
  public OneOf<ResponseObject, ReferenceObject>? Default { get; init; }
  // TODO: only keys 100-599 + 'x' support (like 1xx) are supported
  public IReadOnlyDictionary<HttpStatusCodePattern, OneOf<ResponseObject, ReferenceObject>>? HttpStatusCodesWithResponses { get; init; }
}