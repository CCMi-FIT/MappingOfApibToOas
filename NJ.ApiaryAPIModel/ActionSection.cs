namespace NJ.ApiaryAPIModel;

public class ActionSection : NamedSection
{
  public override string Keyword { get; set; } = "";
  public sealed override string Identifier { get; set; }
  public string Description { get; set; }
  public HttpRequestMethod HttpRequestMethod { get; set; }

  public RelationSection RelationSection { get; set; }
  public UriTemplate UriTemplate { get; set; }
  public UriParametersSection UriParametersSection { get; set; }
  public AttributesSection AttributesSection { get; set; }

  public ICollection<RequestSection> RequestSections { get; set; }
  public ICollection<ResponseSection> ResponseSections { get; set; }

  public ActionSection(string identifier = null, string description = null, HttpRequestMethod httpRequestMethod = default, UriTemplate uriTemplate = null)
  {
    Identifier = identifier;
    Description = description;
    HttpRequestMethod = httpRequestMethod;
    UriTemplate = uriTemplate;
  }
}