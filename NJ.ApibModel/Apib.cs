namespace NJ.ApibModel;

public class Apib
{
  public MetadataSection MetadataSection { get; set; }
  public ApiNameAndOverviewSection ApiNameAndOverviewSection { get; set; }
  public ICollection<ResourceSection> ResourceSections { get; set; }
  public ICollection<ResourceGroupSection> ResourceGroupSections { get; set; }
  public ICollection<DataStructureSection> DataStructuresSections { get; set; }
}