using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.OasModel;

public class HeaderObject : IHeaderOrReferenceObject
{
  public Description? Description { get; init; }
  public bool Required { get; init; }
  public bool Deprecated { get; init; }
  public bool AllowEmptyValue { get; init; }
  public ParameterRulesForSerialization RulesForSerialization { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}