using NJ.OasModel.AdditionalDomainObjects;
using NJ.OasModel.Converters;
using NJ.SharedModel;

namespace NJ.OasModel;

public class ParameterObject : IParameterOrReferenceObject
{
  public string Name { get; init; }
  public ParameterIn In { get; init; }
  public Description? Description { get; init; }
  public bool Required { get; init; }
  public bool Deprecated { get; init; }
  public bool AllowEmptyValue { get; init; }
  public ParameterRulesForSerialization RulesForSerialization { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }

  public ParameterObject(string name, string @in, ParameterRulesForSerialization rulesForSerialization) : this(name, ParameterInConverter.Convert(@in), rulesForSerialization)
  {
  }

  public ParameterObject(string name, ParameterIn @in, ParameterRulesForSerialization rulesForSerialization)
  {
    // TODO: Validate according to in - https://swagger.io/specification/#parameter-object
    In = @in;
    Name = name;
    RulesForSerialization = rulesForSerialization;
  }
}