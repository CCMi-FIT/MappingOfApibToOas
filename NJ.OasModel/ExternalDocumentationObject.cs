using NJ.SharedModel;

namespace NJ.OasModel;

public class ExternalDocumentationObject
{
  public Description? Description { get; init; }
  public Uri Url { get; }

  public ExternalDocumentationObject(string url) : this(new Uri(url))
  {
  }

  public ExternalDocumentationObject(Uri url)
  {
    Url = url;
  }
}