using NJ.SharedModel;

namespace NJ.ApibModel;

public class ResourceSection : NamedSection
{
  public override string Keyword { get; } = "";

  public UriTemplate UriTemplate { get; }
  public HttpRequestMethod? HttpRequestMethod { get; init; }
  public UriParametersSection? ParametersSection { get; init; }
  public AttributesSection? AttributesSection { get; init; }
  public ResourceModelSection? ResourceModelSection { get; init; }
  public IReadOnlyCollection<ActionSection> ActionSections { get; }


  public ResourceSection(string? identifier, string? description, UriTemplate uriTemplate, HttpRequestMethod? httpRequestMethod, IEnumerable<ActionSection> actionSections) : this(identifier, description is not null ? new PlainTextDescription(description) : default, uriTemplate, httpRequestMethod, actionSections)
  {
  }

  public ResourceSection(string? identifier, Description? description, UriTemplate uriTemplate, HttpRequestMethod? httpRequestMethod, IEnumerable<ActionSection> actionSections) : this(identifier, description, uriTemplate, actionSections)
  {
    HttpRequestMethod = httpRequestMethod;
  }

  public ResourceSection(string? identifier, Description? description, UriTemplate uriTemplate, IEnumerable<ActionSection> actionSections) : this(identifier, uriTemplate, actionSections)
  {
    Description = description;
  }

  public ResourceSection(string? identifier, string? description, UriTemplate uriTemplate, IEnumerable<ActionSection> actionSections) : this(identifier, description is not null ? new PlainTextDescription(description) : default, uriTemplate, actionSections)
  {
  }

  public ResourceSection(string? identifier, UriTemplate uriTemplate, IEnumerable<ActionSection> actionSections) : this(uriTemplate, actionSections)
  {
    Identifier = identifier;
  }

  public ResourceSection(UriTemplate uriTemplate, IEnumerable<ActionSection> actionSections)
  {
    UriTemplate = uriTemplate;
    var actionSectionsList = actionSections.ToList();
    if (actionSectionsList.Count == 0)
      throw new ArgumentException($"{nameof(actionSections)} must not be empty");
    ActionSections = actionSectionsList;
  }
}