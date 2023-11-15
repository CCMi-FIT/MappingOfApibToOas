namespace NJ.OasModel;

public class ServerObject
{
  public string Url { get; init; }
  public string Description { get; init; }
  public IReadOnlyDictionary<string, ServerVariableObject> Variables { get; init; }

  public ServerObject(string url = default)
  {
    Url = url;
  }
}