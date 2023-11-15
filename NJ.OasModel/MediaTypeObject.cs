using Newtonsoft.Json;

namespace NJ.OasModel;

public class MediaTypeObject
{
  // TODO: Potential Mutability
  [JsonProperty(PropertyName = "example")]
  public dynamic Example { get; init; }
  [JsonProperty(PropertyName = "schema")]
  public SchemaObject Schema { get; init; }
  [JsonProperty(PropertyName = "examples")]
  public IReadOnlyDictionary<string, IExampleOrReferenceObject> Examples { get; init; }
  [JsonProperty(PropertyName = "encoding")]
  public IReadOnlyDictionary<string, EncodingObject> Encoding { get; init; }
}