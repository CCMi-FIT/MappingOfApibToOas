namespace NJ.OasModel;

public class OperationObject
{
  public string Summary { get; init; }
  public string OperationId { get; init; }
  public string Description { get; init; }
  public ExternalDocumentationObject ExternalDocs { get; init; }
  public IReadOnlyCollection<string> Tags { get; init; }
  public IReadOnlyCollection<IParameterOrReferenceObject> Parameters { get; init; }
  public IRequestBodyOrReferenceObject RequestBody { get; init; }
  public ResponsesObject Responses { get; init; }
  public IReadOnlyDictionary<string, ICallbackOrReferenceObject> Callbacks { get; init; }
  public bool Deprecated { get; init; }
  public IReadOnlyCollection<SecurityRequirementObject> Security { get; init; }
  public IReadOnlyCollection<ServerObject> Servers { get; init; }
}