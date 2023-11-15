namespace NJ.ApibModel;

public class UriTemplate
{
  public string Path { get; set; }

  public UriTemplate(string path = null)
  {
    Path = path;
  }
}