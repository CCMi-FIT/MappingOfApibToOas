using NJ.SharedModel;

namespace NJ.OasModel;

public class OperationObject
{
  public IReadOnlyCollection<string> Tags { get; }
  public string? Summary { get; init; }
  public Description? Description { get; init; }
  public ExternalDocumentationObject? ExternalDocs { get; init; }
  public string? OperationId { get; init; }
  public IReadOnlyCollection<IParameterOrReferenceObject> Parameters { get; }
  public IRequestBodyOrReferenceObject? RequestBody { get; init; }
  public ResponsesObject? Responses { get; init; }
  public IReadOnlyDictionary<string, ICallbackOrReferenceObject> Callbacks { get; }
  public bool Deprecated { get; init; }
  public IReadOnlyCollection<SecurityRequirementObject>? Security { get; init; }
  public IReadOnlyCollection<ServerObject>? Servers { get; init; }


  public OperationObject(
    IEnumerable<string>? tags = default,
    IEnumerable<IParameterOrReferenceObject>? parameters = default,
    IEnumerable<KeyValuePair<string, ICallbackOrReferenceObject>>? callbacks = default
  )
  {
    // TODO: Validate OperationId (must be unique across whole API)
    Tags = tags?.ToList() ?? new List<string>();
    // TODO: Validate that there are no duplicite name+location combinations
    Parameters = parameters?.ToList() ?? new List<IParameterOrReferenceObject>();
    // TODO: Ensure that keys are unique
    Callbacks = callbacks?.ToDictionary(c => c.Key, c => c.Value) ?? new Dictionary<string, ICallbackOrReferenceObject>();
  }
}