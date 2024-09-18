using NJ.ApibModel.AdditionalDomainObjects;

namespace NJ.ApibModel;

public class ResourceSection : NamedSection
{
  public override string Keyword { get; } = "";

  public UriTemplate UriTemplate { get; }
  public HttpRequestMethod? HttpRequestMethod { get; }
  public UriParametersSection? ParametersSection { get; init; }
  public AttributesSection? AttributesSection { get; init; }
  public ResourceModelSection? ResourceModelSection { get; init; }
  public IReadOnlyCollection<ActionSection> ActionSections { get; }

  protected ResourceSection(UriTemplate uriTemplate, IEnumerable<ActionSection> actionSections)
  {
    UriTemplate = uriTemplate;
    var actionSectionsList = actionSections.ToList();
    if (actionSectionsList.Count == 0)
      throw new ArgumentException($"{nameof(actionSections)} must not be empty");
    ActionSections = actionSectionsList;
  }
}