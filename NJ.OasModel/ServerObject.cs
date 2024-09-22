using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.OasModel;

public class ServerObject
{
  public PathString Url { get; init; }

  public Description? Description { get; init; }
  public IReadOnlyDictionary<string, ServerVariableObject>? Variables { get; init; }

  public ServerObject(string pathString) : this(new PathString(pathString))
  {
  }

  public ServerObject(PathString pathString)
  {
    Url = pathString;
    Variables = new Dictionary<string, ServerVariableObject>();
  }
}