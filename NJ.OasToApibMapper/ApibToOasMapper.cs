using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.ApibToOasMapper;

public static class ApibToOasMapper
{
  public static OpenApiObject Map(Apib apib)
  {
    var namedTypes = ApiTypesProvider.GetNamedApiTypes(apib);
    var result = new OpenApiObject
    {
      OpenApi = "3.0.3",
      Servers = MetadataSectionToServersMapper.Map(apib.MetadataSection),
      Info = ApiNameAndOverviewSectionMapper.Map(apib.ApiNameAndOverviewSection),
      Paths = ResourceMapper.MapResources(apib, namedTypes),
      Tags = ResourceGroupSectionToTagsMapper.MapToTagObjects(apib.ResourceGroupSections),
      Components = MapperToComponentsObject.Map(apib, namedTypes)
    };
    return result;
  }
}