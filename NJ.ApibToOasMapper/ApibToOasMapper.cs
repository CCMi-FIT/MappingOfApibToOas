using NJ.ApibModel;
using NJ.OasModel;
using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.ApibToOasMapper;

public static class ApibToOasMapper
{
  public static OpenApiObject Map(Apib apib)
  {
    var namedTypes = ApiTypesProvider.GetNamedApiTypes(apib);
    var result = new OpenApiObject(new OpenApiVersion("3", "1", "0"), ApiNameAndOverviewSectionMapper.Map(apib.ApiNameAndOverviewSection))
    {
      Servers = MetadataSectionToServersMapper.Map(apib.MetadataSection),
      Paths = ResourceMapper.MapResources(apib, namedTypes),
      Tags = ResourceGroupSectionToTagsMapper.MapToTagObjects(apib.ResourceGroupSections),
      Components = MapperToComponentsObject.Map(apib, namedTypes)
    };
    return result;
  }
}