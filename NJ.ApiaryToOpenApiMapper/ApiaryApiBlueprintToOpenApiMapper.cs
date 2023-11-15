using NJ.ApiaryAPIModel;
using NJ.OpenAPIModel;

namespace NJ.ApiaryToOpenApiMapper;

public static class ApiaryApiBlueprintToOpenApiMapper
{
  public static OpenApiObject Map(ApiaryApiBlueprint apb)
  {
    var apiaryNamedTypes = ApiaryTypesProvider.GetNamedTypes(apb);
    var result = new OpenApiObject
    {
      OpenApi = "3.0.3",
      Servers = MetadataSectionToServersMapper.Map(apb.MetadataSection),
      Info = ApiNameAndOverviewSectionMapper.Map(apb.ApiNameAndOverviewSection),
      Paths = ResourceMapper.MapResources(apb, apiaryNamedTypes),
      Tags = ResourceGroupSectionToTagsMapper.MapToTagObjects(apb.ResourceGroupSections),
      Components = MapperToComponentsObject.Map(apb, apiaryNamedTypes)
    };
    return result;
  }
}