using NJ.SharedModel;
using OneOf;

namespace NJ.OasModel;

public class PathItemObject : IPathItemOrReferenceObject
{
  public string? Ref { get; init; }
  public string? Summary { get; init; }
  public Description? Description { get; init; }
  public OperationObject? Get { get; init; }
  public OperationObject? Put { get; init; }
  public OperationObject? Post { get; init; }
  public OperationObject? Delete { get; init; }
  public OperationObject? Options { get; init; }
  public OperationObject? Head { get; init; }
  public OperationObject? Patch { get; init; }
  public OperationObject? Trace { get; init; }
  public IReadOnlyCollection<ServerObject> Servers { get; }
  public IReadOnlyCollection<OneOf<ParameterObject, ReferenceObject>> Parameters { get; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }

  public PathItemObject(IEnumerable<ServerObject> servers, IReadOnlyCollection<OneOf<ParameterObject, ReferenceObject>> parameters)
  {
    // TODO: Validate ref: 1) format 2) that it links to valid item
    Servers = servers?.ToList() ?? new List<ServerObject>();
    // TODO: Validate that there are no duplicities (i.e. combinations of name and location)
    Parameters = parameters?.ToList() ?? new List<OneOf<ParameterObject, ReferenceObject>>();
  }
}