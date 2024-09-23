using System.Text.RegularExpressions;

namespace NJ.ApibToOasMapper
{
  public static class UriTemplateParametersParser
  {
    private const string PathParameterPattern = @"\{\s*([^\}]+)\s*\}";
    public static IEnumerable<string> ParsePathParameters(string uriTemplatePath)
    {
      var emptyEnumerable = Enumerable.Empty<string>();
      if (uriTemplatePath is null)
        return emptyEnumerable;
      var matches = Regex.Matches(uriTemplatePath, PathParameterPattern).ToList();
      var result = matches.Select(m => m.Groups[1].ToString());
      return result;
    }
  }
}
