using NJ.ApiaryAPIModel;
using NJ.OpenAPIModel;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class MetadataSectionToServersMapper
  {
    public static ICollection<ServerObject> Map(MetadataSection metadataSection)
    {
      var metadataDictionary = metadataSection?.KeysWithValues;
      if (metadataDictionary is null)
        return null;
      if (!metadataDictionary.TryGetValue("HOST", out var host))
        return null;
      var result = new List<ServerObject> { new ServerObject { Url = host } };
      return result;
    }
  }
}
