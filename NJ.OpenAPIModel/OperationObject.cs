using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class OperationObject
{
  [JsonProperty(PropertyName = "summary")]
  public string Summary { get; init; }

  [JsonProperty(PropertyName = "operationId")]
  public string OperationId { get; init; }
  [JsonProperty(PropertyName = "description")]
  public string Description { get; init; }
  public ExternalDocumentationObject ExternalDocs { get; init; }
  [JsonProperty(PropertyName = "tags")]
  public IReadOnlyCollection<string> Tags { get; init; }
  [JsonProperty(PropertyName = "parameters")]
  public IReadOnlyCollection<IParameterOrReferenceObject> Parameters { get; init; }
  [JsonProperty(PropertyName = "requestBody")]
  public IRequestBodyOrReferenceObject RequestBody { get; init; }

  [JsonProperty(Order = -100, PropertyName = "responses")]
  public ResponsesObject Responses { get; init; }
  public IReadOnlyDictionary<string, ICallbackOrReferenceObject> Callbacks { get; init; }
  public bool Deprecated { get; init; }
  public IReadOnlyCollection<SecurityRequirementObject> Security { get; init; }
  public IReadOnlyCollection<ServerObject> Servers { get; init; }
}