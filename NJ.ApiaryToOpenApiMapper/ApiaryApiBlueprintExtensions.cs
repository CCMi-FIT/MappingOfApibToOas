using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class ApiaryApiBlueprintExtensions
  {
    public static IEnumerable<ResourceSection> GetAllResourceSections(this ApiaryApiBlueprint apb)
    {
      var resourceGroups = apb.ResourceGroupSections;

      IEnumerable<ResourceSection> resourcesFromGroups;
      if (resourceGroups is null)
        resourcesFromGroups = Array.Empty<ResourceSection>();
      else
        resourcesFromGroups = resourceGroups.SelectMany(g => g.ResourceSections);

      var resources = apb.ResourceSections;
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
