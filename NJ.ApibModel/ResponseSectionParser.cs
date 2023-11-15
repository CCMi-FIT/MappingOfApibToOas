using System.Text.RegularExpressions;

namespace NJ.ApibModel
{
  public static class ResponseSectionParser
  {
    private const string ResponsePattern = @"^\+Response (.*) \((.*)\)$";
    public static bool TryParseResponseHeader(string line, out string httpStatusCode, out string mediaType)
    {
      httpStatusCode = null;
      mediaType = null;

      var match = Regex.Match(line, ResponsePattern);
      if (!match.Success)
        return false;
      httpStatusCode = match.Groups[1].ToString();
      mediaType = match.Groups[2].ToString();
      return true;
    }
  }
}
