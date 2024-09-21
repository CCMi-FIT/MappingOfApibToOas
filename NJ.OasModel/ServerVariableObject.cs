using NJ.SharedModel;

namespace NJ.OasModel;

public class ServerVariableObject
{
  // TODO: If Enum is defined, Default must be in Enum
  public IReadOnlyCollection<string>? Enum { get; init; }
  public string Default { get; init; }
  public Description? Description { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }

  public ServerVariableObject(string @default)
  {
    Default = @default;
    Enum = new List<string>();
  }
}