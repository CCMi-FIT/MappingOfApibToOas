using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

// TODO: Different properties based on type - "apiKey", "http", "mutualTLS", "oauth2", "openIdConnect"
public class SecuritySchemeObject : ISecuritySchemeOrReferenceObject
{
  public SecuritySchemeType Type { get; }

  public Description? Description { get; init; }
  // TODO: Applies to apiKey, Required
  public string? Name { get; init; }
  // TODO: Applies to apiKey, Required, ParameterIn.Path is invalid
  public ParameterIn? In { get; init; }
  // TODO: Applies to http, required, should be registered with https://www.iana.org/assignments/http-authschemes/http-authschemes.xhtml
  public string? Scheme { get; init; }
  // TODO: Applies to http
  public string? BearerFormat { get; init; }
  // TODO: Applies to oauth2, required
  public OAuthFlowObject? Flows { get; init; }
  // TODO: Applies to openIdConnectUrl, required
  public Uri? OpenIdConnectUrl { get; init; }

  public SecuritySchemeObject(SecuritySchemeType type)
  {
    Type = type;
  }
}