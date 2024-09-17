namespace NJ.OasModel;

public class SecurityRequirementObject
{
  // TODO: Each key must correspond to a security scheme declared in Security Schemes of Components Object
  // TODO: For "oauth2" or "openIdConnect", value is a list of scope names required for the execution (may be empty if authorization doesnt required specified scope); for other security scheme types, the aay may contain a list of role names which are required for the execution, but are not otherwise defined or exchanged in-band
  public IReadOnlyDictionary<string, IReadOnlyCollection<string>>? SchemeNamesWithScopeNames { get; init; }
}