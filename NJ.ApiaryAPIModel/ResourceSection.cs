namespace NJ.ApiaryAPIModel;

public class ResourceSection : NamedSection
{
  public UriTemplate UriTemplate { get; set; }
  public HttpRequestMethod HttpRequestMethod { get; set; }

  public string Description { get; set; }
  public UriParametersSection ParametersSection { get; set; }
  public AttributesSection AttributesSection { get; set; }
  public ResourceModelSection ResourceModelSection { get; set; }
  public ICollection<ActionSection> ActionSections { get; set; }

  public override string Keyword { get; set; } = "";
  public sealed override string Identifier { get; set; }

  public ResourceSection(string identifier = null, UriTemplate uriTemplate = null)
  {
    Identifier = identifier;
    UriTemplate = uriTemplate;
  }
}