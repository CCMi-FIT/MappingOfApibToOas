namespace NJ.OasModel;

public class ServerVariableObject
{
  public IReadOnlyCollection<string> Enum { get; init; }
  public string Default { get; init; }
  public string Description { get; init; }

}