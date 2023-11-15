using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.ApibToOasMapper;

public static class ApibToOasMapper
{
  public static OpenApiObject Map(Apib apb)
  {
    var namedTypes = ApiTypesProvider.GetNamedApiTypes(apb);
    var result = new OpenApiObject
    {
      OpenApi = "3.0.3",
      Servers = MetadataSectionToServersMapper.Map(apb.MetadataSection),
      Info = ApiNameAndOverviewSectionMapper.Map(apb.ApiNameAndOverviewSection),
      Paths = ResourceMapper.MapResources(apb, namedTypes),
      Tags = ResourceGroupSectionToTagsMapper.MapToTagObjects(apb.ResourceGroupSections),
      Components = MapperToComponentsObject.Map(apb, namedTypes)
    };
    return result;
  }
}