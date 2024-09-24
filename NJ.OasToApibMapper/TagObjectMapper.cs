using NJ.OasModel;
using NJ.OasToApibMapper.HelperModel;

namespace NJ.OasToApibMapper
{
  public static class TagObjectMapper
  {
    public static ResourceGroupInfo Map(TagObject tagObject)
    {
      var result = new ResourceGroupInfo(tagObject.Name, tagObject.Description);
      return result;
    }
    public static IEnumerable<ResourceGroupInfo> Map(IEnumerable<TagObject> tagObjects)
    {
      if (tagObjects is null)
        return default;
      var result = tagObjects.Select(t => Map(t));
      return result;
    }
  }
}
