using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class ResourceGroupSectionToTagsMapper
  {
    public static IReadOnlyCollection<TagObject> MapToTagObjects(IEnumerable<ResourceGroupSection> resourceGroupSections)
    {
      if (resourceGroupSections is null)
        return Array.Empty<TagObject>();
      var result = resourceGroupSections.Select(r => new TagObject(r.Identifier, r.Description)).ToList();
      return result;
    }

    public static IReadOnlyCollection<string> MapToStrings(IEnumerable<ResourceGroupSection> resourceGroupSections)
    {
      if (resourceGroupSections is null)
        return Array.Empty<string>();
      var result = resourceGroupSections.Where(r => r is not null).Select(r => r.Identifier).ToList();
      return result;
    }
  }
}
