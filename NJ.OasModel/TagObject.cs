using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class TagObject
{
  public string Name { get; init; }
  public Description? Description { get; init; }
  public ExternalDocumentationObject? ExternalDocs { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
  public TagObject(string name, Description? description = default, ExternalDocumentationObject? externalDoc = default)
  {
    Name = name;
    Description = description;
    ExternalDocs = externalDoc;
  }
}