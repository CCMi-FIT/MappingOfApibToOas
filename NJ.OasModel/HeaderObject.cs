using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.OasModel;

public class HeaderObject : IHeaderOrReferenceObject
{
  // TODO: Name is implicitly given in corresponding headers map
  // TODO: in is implicitly in header
  // TODO: All traits that are affected by the location must be applicable to a location of header
  public Description? Description { get; init; }
  public bool Required { get; init; }
  public bool Deprecated { get; init; }
  public bool AllowEmptyValue { get; init; }
  public ParameterRulesForSerialization RulesForSerialization { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}