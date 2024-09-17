using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class ExampleObject : IExampleOrReferenceObject
{
  public string? Summary { get; init; }
  public Description? Description { get; init; }
  // TODO: value is expected to comply to associated schema
  // TODO: Value and External Value are mutually exclusive
  public dynamic? Value { get; init; }
  public Uri? ExternalValue { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}