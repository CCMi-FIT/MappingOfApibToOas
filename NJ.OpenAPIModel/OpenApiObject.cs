using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class OpenApiObject
{
  // major.minor.patch
  [JsonProperty(Order = -100, PropertyName = "openapi")]
  public string OpenApi { get; init; }

  // info
  // required
  [JsonProperty(Order = -90, PropertyName = "info")]
  public InfoObject Info { get; init; }

  public string JsonSchemaDialect { get; init; }

  public ICollection<ServerObject> Servers { get; init; }

  [JsonProperty(Order = -80, PropertyName = "paths")]
  public PathsObject Paths { get; init; }

  public IReadOnlyDictionary<string, IPathItemOrReferenceObject> WebHooks { get; init; }
  public IReadOnlyCollection<SecurityRequirementObject> Security { get; init; }
  public ExternalDocumentationObject ExternalDocs { get; init; }
  public SpecificationExtensions SpecificationExtensions { get; init; }
  [JsonProperty(PropertyName = "components")]
  public ComponentsObject Components { get; init; }

  [JsonProperty(PropertyName = "tags")]
  public IReadOnlyCollection<TagObject> Tags { get; init; }
}