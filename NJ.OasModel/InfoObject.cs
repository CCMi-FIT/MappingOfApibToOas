using Newtonsoft.Json;

namespace NJ.OasModel;

public class InfoObject
{
  public string Title { get; init; }
  public string Version { get; init; }
  public string Summary { get; init; }
  public string Description { get; init; }
  public string TermsOfService { get; init; }
  public ContactObject Contact { get; init; }
  public LicenseObject License { get; init; }
}