namespace NJ.OasModel;

public class OAuthFlowObject
{
  // TODO: Applies to "implicit", "authorizationCode", required
  public string? AuthorizationUrl { get; init; }
  // TODO: Applies to "password", "clientCredentials", "authorizationCode", required
  public string? TokenUrl { get; init; }
  public string? RefreshUrl { get; init; }
  public IReadOnlyDictionary<string, string> Scopes { get; init; }

  public OAuthFlowObject(IEnumerable<KeyValuePair<string, string>>? scopes = default)
  {
    Scopes = scopes?.ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();
  }
}