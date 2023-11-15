using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class SchemaObject
{
  // TODO: Where is this in specification?
  [JsonProperty(PropertyName = "type")]
  public string Type { get; init; }
  [JsonProperty(PropertyName = "discriminator")]
  public DiscriminatorObject Discriminator { get; init; }
  [JsonProperty(PropertyName = "xml")]
  public XmlObject Xml { get; init; }
  [JsonProperty(PropertyName = "externalDocs")]
  public ExternalDocumentationObject ExternalDocs { get; init; }
  //[JsonProperty(PropertyName = "example")]
  //public object Example { get; init; }
  // TODO: Potential Mutability?
  [JsonProperty(PropertyName = "properties")]
  public dynamic Properties { get; init; }
  [JsonProperty(PropertyName = "default")]
  // TODO: Not in Specification?
  // TODO: Potential Mutability?
  public dynamic Default { get; init; }
  [JsonProperty(PropertyName = "required")]
  public ICollection<string> Required { get; init; }
  // TODO: Potential Mutability?
  [JsonProperty(PropertyName = "items")]
  public dynamic Items { get; init; }
  [JsonProperty(PropertyName = "additionalProperties")]
  public bool AdditionalProperties { get; init; }
  [JsonProperty(PropertyName = "description")]
  public string Description { get; init; }
}