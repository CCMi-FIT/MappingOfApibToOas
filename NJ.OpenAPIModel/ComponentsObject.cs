using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class ComponentsObject
{
  [JsonProperty(PropertyName = "schemas")]
  public IReadOnlyDictionary<string, SchemaObject> Schemas { get; init; }
  public IReadOnlyDictionary<string, IResponseOrReferenceObject> Responses { get; init; }
  public IReadOnlyDictionary<string, IParameterOrReferenceObject> Parameters { get; init; }
  public IReadOnlyDictionary<string, IRequestBodyOrReferenceObject> RequestBodies { get; init; }
  public IReadOnlyDictionary<string, IHeaderOrReferenceObject> Headers { get; init; }
  public IReadOnlyDictionary<string, ISecuritySchemeOrReferenceObject> SecuritySchemes { get; init; }
  public IReadOnlyDictionary<string, ILinkOrReferenceObject> Links { get; init; }
  public IReadOnlyDictionary<string, ICallbackOrReferenceObject> Callbacks { get; init; }
  public IReadOnlyDictionary<string, IPathItemOrReferenceObject> PathItems { get; init; }
}