namespace NJ.OpenAPIModel;

public class OAuthFlowObject
{
  public string AuthorizationUrl { get; init; }
  public string TokenUrl { get; init; }
  public string RefreshUrl { get; init; }
  public IReadOnlyDictionary<string, string> Scopes { get; init; }
}