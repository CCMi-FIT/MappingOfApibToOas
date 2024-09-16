using NJ.OasModel.AdditionalDomainObjects;
using OneOf;

namespace NJ.OasModel;

public class OperationObject
{
  public IReadOnlyCollection<string> Tags { get; }
  public string? Summary { get; init; }
  public Description? Description { get; init; }
  public ExternalDocumentationObject? ExternalDocs { get; init; }
  public string? OperationId { get; init; }
  public IReadOnlyCollection<OneOf<ParameterObject, ReferenceObject>> Parameters { get; }
  public OneOf<RequestBodyObject, ReferenceObject>? RequestBody { get; init; }
  public ResponsesObject? Responses { get; init; }
  public IReadOnlyDictionary<string, OneOf<CallbackObject, ReferenceObject>> Callbacks { get; }
  public bool Deprecated { get; init; }
  public IReadOnlyCollection<SecurityRequirementObject>? Security { get; init; }
  public IReadOnlyCollection<ServerObject>? Servers { get; init; }


  public OperationObject(
    IEnumerable<string>? tags = default,
    IEnumerable<OneOf<ParameterObject, ReferenceObject>>? parameters = default,
    IEnumerable<KeyValuePair<string, OneOf<CallbackObject, ReferenceObject>>>? callbacks = default
  )
  {
    // TODO: Validate OperationId (must be unique across whole API)
    Tags = tags?.ToList() ?? new List<string>();
    // TODO: Validate that there are no duplicite name+location combinations
    Parameters = parameters?.ToList() ?? new List<OneOf<ParameterObject, ReferenceObject>>();
    // TODO: Ensure that keys are unique
    Callbacks = callbacks?.ToDictionary(c => c.Key, c => c.Value) ?? new Dictionary<string, OneOf<CallbackObject, ReferenceObject>>();
  }
}