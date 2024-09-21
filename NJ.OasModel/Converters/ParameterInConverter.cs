using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel.Converters
{
  public static class ParameterInConverter
  {
    public static ParameterIn Convert(string @in)
    {
      var result = @in switch
      {
        "query" => ParameterIn.Query,
        "header" => ParameterIn.Header,
        "path" => ParameterIn.Path,
        "cookie" => ParameterIn.Cookie,
        _ => throw new NotSupportedException()
      };
      return result;
    }

    public static string Convert(ParameterIn @in)
    {
      var result = @in switch
      {
        ParameterIn.Query => "query",
        ParameterIn.Header => "header",
        ParameterIn.Path => "path",
        ParameterIn.Cookie => "cookie",
        _ => throw new NotSupportedException()
      };
      return result;
    }
  }
}
