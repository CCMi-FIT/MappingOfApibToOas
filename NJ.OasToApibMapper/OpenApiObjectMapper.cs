using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper
{
  public static class OpenApiObjectMapper
  {
    public static Apib Map(OpenApiObject openApiObject)
    {
      var apiNameAndOverviewSection = InfoObjectMapper.Map(openApiObject.Info);
      var rgInfos = TagObjectMapper.Map(openApiObject.Tags);
      var resourceSections = PathItemsObjectMapper.Map(openApiObject.Paths);
      var result = new Apib
      {
        ApiNameAndOverviewSection = apiNameAndOverviewSection,
        ResourceSections = resourceSections.ToList()
      };
      return result;
    }
  }
}
