namespace NJ.OasModel;

public class OpenApiObject
{
  public string OpenApi { get; init; }
  public InfoObject Info { get; init; }
  public string JsonSchemaDialect { get; init; }
  public ICollection<ServerObject> Servers { get; init; }
  public PathsObject Paths { get; init; }
  public IReadOnlyDictionary<string, IPathItemOrReferenceObject> WebHooks { get; init; }
  public IReadOnlyCollection<SecurityRequirementObject> Security { get; init; }
  public ExternalDocumentationObject ExternalDocs { get; init; }
  public SpecificationExtensions SpecificationExtensions { get; init; }
  public ComponentsObject Components { get; init; }
  public IReadOnlyCollection<TagObject> Tags { get; init; }
}