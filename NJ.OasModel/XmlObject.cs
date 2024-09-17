namespace NJ.OasModel;

public class XmlObject
{
  public string? Name { get; init; }
  public Uri? Namespace { get; init; }
  public string? Prefix { get; init; }
  public bool Attribute { get; init; }
  public bool Wrapped { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}