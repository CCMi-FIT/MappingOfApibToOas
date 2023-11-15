using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class TagObject
{
  [JsonProperty(PropertyName = "name")]
  public string Name { get; init; }
  [JsonProperty(PropertyName = "description")]
  public string Description { get; init; }
  public ExternalDocumentationObject ExternalDocs { get; init; }

  public TagObject(string name = null, string description = null, ExternalDocumentationObject externalDoc = null)
  {
    Name = name;
    Description = description;
    ExternalDocs = externalDoc;
  }
}