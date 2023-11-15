namespace NJ.ApibModel;

public class ResourceGroupSection : NamedSection
{
  public override string Keyword { get; set; } = "Group";
  public sealed override string Identifier { get; set; }
  public string Description { get; set; }
  public ICollection<ResourceSection> ResourceSections { get; set; }

  public ResourceGroupSection(string identifier = null, string description = null, IEnumerable<ResourceSection> resourceSections = null)
  {
    Identifier = identifier;
    Description = description;
    if (resourceSections is not null)
      ResourceSections = resourceSections.ToList();
    else
      ResourceSections = new List<ResourceSection>();
  }
}