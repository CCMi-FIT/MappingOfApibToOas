using Newtonsoft.Json;

namespace NJ.OasModel;

public class ParameterObject : IParameterOrReferenceObject
{
  [JsonProperty(PropertyName = "name")]
  public string Name { get; init; }
  [JsonProperty(PropertyName = "in")]
  public string In { get; init; }
  [JsonProperty(PropertyName = "description")]
  public string Description { get; init; }
  [JsonProperty(PropertyName = "required")]
  public bool Required { get; init; }
  [JsonProperty(PropertyName = "deprecated")]
  public bool Deprecated { get; init; }
  [JsonProperty(PropertyName = "allowEmptyValue")]
  public bool AllowEmptyValue { get; init; }

  // TODO: Advanced Object
  [JsonProperty(PropertyName = "style")]
  public string Style { get; init; }
  [JsonProperty(PropertyName = "explode")]
  public bool Explode { get; init; }

  [JsonProperty(PropertyName = "allowReserved")]
  public bool AllowReserved { get; init; }
  [JsonProperty(PropertyName = "schema")]
  public SchemaObject Schema { get; init; }
  [JsonProperty(PropertyName = "example")]
  // TODO: Potential Mutability
  public object Example { get; init; }
  [JsonProperty(PropertyName = "examples")]
  public IReadOnlyDictionary<string, IExampleOrReferenceObject> Examples { get; init; }
  [JsonProperty(PropertyName = "content")]
  public IReadOnlyDictionary<string, MediaTypeObject> Content { get; init; }
}