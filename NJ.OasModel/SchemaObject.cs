namespace NJ.OasModel;

public class SchemaObject
{
  public DiscriminatorObject? Discriminator { get; init; }
  public XmlObject? Xml { get; init; }
  public ExternalDocumentationObject? ExternalDocs { get; init; }
  public dynamic? Example { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }

  // TODO: Better way to model following properties ?
  public string? Type { get; init; }
  public dynamic? Properties { get; init; }
  public dynamic? Default { get; init; }
  public ICollection<string>? Required { get; init; }
  public dynamic? Items { get; init; }
  public bool? AdditionalProperties { get; init; }
  public string? Description { get; init; }
}