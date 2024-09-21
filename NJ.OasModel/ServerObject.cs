using NJ.SharedModel;

namespace NJ.OasModel;

public class ServerObject
{
  public Uri Url { get; init; }

  public Description? Description { get; init; }
  public IReadOnlyDictionary<string, ServerVariableObject>? Variables { get; init; }

  public ServerObject(string url) : this(new Uri(url))
  {
  }

  public ServerObject(Uri url)
  {
    Url = url;
    Variables = new Dictionary<string, ServerVariableObject>();
  }
}