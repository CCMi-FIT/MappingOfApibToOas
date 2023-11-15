using Newtonsoft.Json;

namespace NJ.OasModel;

public class InfoObject
{
  [JsonProperty(Order = -100, PropertyName = "title")]
  public string Title { get; init; }
  [JsonProperty(Order = -90, PropertyName = "version")]
  public string Version { get; init; }
  public string Summary { get; init; }
  [JsonProperty(Order = -80, PropertyName = "description")]
  public string Description { get; init; }
  public string TermsOfService { get; init; }
  public ContactObject Contact { get; init; }
  public LicenseObject License { get; init; }
}