using Newtonsoft.Json;

namespace NJ.OasModel;

public class ResponseObject : IResponseOrReferenceObject
{

  [JsonProperty(Order = -100, PropertyName = "description")]
  public string Description { get; init; }

  [JsonProperty(Order = -90, PropertyName = "headers")]
  public IReadOnlyDictionary<string, IHeaderOrReferenceObject> Headers { get; init; }

  [JsonProperty(Order = -80, PropertyName = "content")]
  public IReadOnlyDictionary<string, MediaTypeObject> Content { get; init; }
  public IReadOnlyDictionary<string, ILinkOrReferenceObject> Links { get; init; }
}