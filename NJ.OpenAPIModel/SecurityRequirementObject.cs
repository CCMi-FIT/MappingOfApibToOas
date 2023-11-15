namespace NJ.OpenAPIModel;

public class SecurityRequirementObject
{
  public IReadOnlyDictionary<string, ICollection<string>> SchemeNamesWithScopeNames { get; init; }
}