using NJ.SharedModel;

namespace NJ.ApibModel;

public class ResourceGroupSection : NamedSection
{
  public override string? Keyword { get; } = "Group";
  public ICollection<ResourceSection> ResourceSections { get; set; }

  public ResourceGroupSection(string identifier, Description? description = default, IEnumerable<ResourceSection>? resourceSections = default)
  {
    Identifier = identifier;
    Description = description;
    ResourceSections = resourceSections?.ToList() ?? new List<ResourceSection>();
  }
}