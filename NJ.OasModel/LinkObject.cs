using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;
using OneOf;

namespace NJ.OasModel;

public class LinkObject
{
  // TODO: OperationRef and OperationId are mutually exclusive
  // TODO: Must point to OperationObject
  public Uri? OperationRef { get; init; }
  // TODO: Name of existing, resolvable OAS Operation
  public string? OperationId { get; init; }
  // TODO: Key can be qualified (i.e. "path.id")
  // TODO: Validate parameters exist ?
  public IReadOnlyDictionary<string, OneOf<RuntimeExpression, dynamic>>? Parameters { get; init; }
  public OneOf<RuntimeExpression, dynamic>? RequestBody { get; init; }
  public Description? Description { get; init; }
  public ServerObject? Server { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}