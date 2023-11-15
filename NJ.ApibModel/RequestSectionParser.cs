using System.Text.RegularExpressions;

namespace NJ.ApibModel
{
  public static class RequestSectionParser
  {
    private const string RequestPattern = @"^\+ Request (.*) \((.*)\)$";

    public static bool TryParseRequestSectionHeader(string line, out string identifier, out string mediaType)
    {
      identifier = null;
      mediaType = null;

      var match = Regex.Match(line, RequestPattern);
      if (!match.Success)
        return false;
      identifier = match.Groups[1].ToString();
      mediaType = match.Groups[2].ToString();
      return true;
    }
  }
}
