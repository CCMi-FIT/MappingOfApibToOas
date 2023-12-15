namespace NJ.OasModel;

public class TagObject
{
  public string Name { get; init; }
  public string Description { get; init; }
  public ExternalDocumentationObject ExternalDocs { get; init; }
  public TagObject(string name = null, string description = null, ExternalDocumentationObject externalDoc = null)
  {
    Name = name;
    Description = description;
    ExternalDocs = externalDoc;
  }
}