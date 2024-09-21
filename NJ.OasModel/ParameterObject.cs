using NJ.OasModel.AdditionalDomainObjects;
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

  public ParameterObject(string name, string @in) : this(name, ParseIn(@in))
  {
  }

  public ParameterObject(string name, ParameterIn @in)
  {
    // TODO: Validate according to in - https://swagger.io/specification/#parameter-object
    In = @in;
    Name = name;
  }

  private static ParameterIn ParseIn(string @in)
  {
    var result = @in switch
    {
      "query" => ParameterIn.Query,
      "header" => ParameterIn.Header,
      "path" => ParameterIn.Path,
      "cookie" => ParameterIn.Cookie,
      _ => throw new NotSupportedException()
    };
    return result;
  }
}