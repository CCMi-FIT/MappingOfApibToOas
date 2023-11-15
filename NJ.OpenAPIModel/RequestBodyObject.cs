using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class RequestBodyObject : IRequestBodyOrReferenceObject
{
  public string Description { get; init; }
  [JsonProperty(PropertyName = "content")]
  public IReadOnlyDictionary<string, MediaTypeObject> Content { get; init; }
  public bool Required { get; init; }
}