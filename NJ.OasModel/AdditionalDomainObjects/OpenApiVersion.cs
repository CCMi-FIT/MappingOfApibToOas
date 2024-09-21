namespace NJ.OasModel.AdditionalDomainObjects
{
  public class OpenApiVersion
  {
    public string Major { get; }
    public string Minor { get; }
    public string Patch { get; }

    public OpenApiVersion(string major, string minor, string patch)
    {
      Major = major;
      Minor = minor;
      Patch = patch;
    }
  }
}
