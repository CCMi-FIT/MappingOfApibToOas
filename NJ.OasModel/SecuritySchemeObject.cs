namespace NJ.OasModel;

public class SecuritySchemeObject : ISecuritySchemeOrReferenceObject
{
  public string Type { get; init; }
  public string Description { get; init; }
  public string Name { get; init; }
  public string In { get; init; }
  public string Scheme { get; init; }
  public string BearerFormat { get; init; }
  public OAuthFlowObject Flows { get; init; }
  public string OpenIdConnectUrl { get; init; }
}