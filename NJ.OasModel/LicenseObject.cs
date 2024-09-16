using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class LicenseObject
{
  public string Name { get; init; }
  // TODO: Identifier and URL are mutually exclusive
  // TODO: SPDX License Expression - https://spdx.org/licenses/
  public SpdxLicenseExpression? Identifier { get; init; }
  public string? Url { get; init; }
}