namespace NJ.ApibModel;

public class Apib
{
  // TODO: Constructor ? Nullability ?
  public MetadataSection? MetadataSection { get; init; }
  public ApiNameAndOverviewSection? ApiNameAndOverviewSection { get; init; }
  public ICollection<ResourceSection>? ResourceSections { get; init; }
  public ICollection<ResourceGroupSection>? ResourceGroupSections { get; init; }
  public ICollection<DataStructureSection>? DataStructuresSections { get; init; }
}