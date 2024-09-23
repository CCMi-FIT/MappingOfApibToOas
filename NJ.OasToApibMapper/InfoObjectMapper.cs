using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper
{
  public static class InfoObjectMapper
  {
    public static ApiNameAndOverviewSection Map(InfoObject info)
    {
      var result = new ApiNameAndOverviewSection
      {
        Name = info.Title,
        Description = info.Description
      };
      return result;
    }
  }
}