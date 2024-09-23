using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class ApiNameAndOverviewSectionMapper
  {
    public static InfoObject Map(ApiNameAndOverviewSection apiNameAndOverviewSection)
    {
      if (apiNameAndOverviewSection is null)
        return null;
      var result = new InfoObject
      {
        Title = apiNameAndOverviewSection.Name,
        Description = apiNameAndOverviewSection.Description,
        Version = "1.0.0"
      };
      return result;
    }
  }
}
