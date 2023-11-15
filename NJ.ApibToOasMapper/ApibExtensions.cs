using NJ.ApibModel;

namespace NJ.ApibToOasMapper
{
  public static class ApibExtensions
  {
    public static IEnumerable<ResourceSection> GetAllResourceSections(this Apib apib)
    {
      var resourceGroups = apib.ResourceGroupSections;

      IEnumerable<ResourceSection> resourcesFromGroups;
      if (resourceGroups is null)
        resourcesFromGroups = Array.Empty<ResourceSection>();
      else
        resourcesFromGroups = resourceGroups.SelectMany(g => g.ResourceSections);

      var resources = apib.ResourceSections;
      IEnumerable<ResourceSection> directResources;
      if (resources is null)
        directResources = Array.Empty<ResourceSection>();
      else
        directResources = resources;

      var result = resourcesFromGroups.Concat(directResources);
      return result;
    }
  }
}
