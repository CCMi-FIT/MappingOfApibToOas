using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class InfoObject
{
  public string Title { get; init; }
  public string? Summary { get; init; }
  // TODO: Can be plain text or markdown (TODO?)
  public Description? Description { get; init; }
  // Url
  public Uri? TermsOfService { get; init; }
  public ContactObject? Contact { get; init; }
  public LicenseObject? License { get; init; }
  public string Version { get; init; }
}