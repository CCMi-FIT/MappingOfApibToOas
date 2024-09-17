namespace NJ.OasModel
{
  public class OAuthFlowsObject
  {
    public OAuthFlowObject? Implicit { get; init; }
    public OAuthFlowObject? Password { get; init; }
    public OAuthFlowObject? ClientCredentials { get; init; }
    public OAuthFlowObject? AuthorizationCode { get; init; }
    public SpecificationExtensions? SpecificationExtensions { get; init; }
  }
}
