namespace NJ.SharedModel
{
  public class MediaType : MediaRange
  {
    private const string _regexPattern = @"^[^/]+/(?:\*|[^/]+)$";
    public MediaType(string pattern) : base(pattern)
    {
      if (System.Text.RegularExpressions.Regex.IsMatch(pattern, _regexPattern))
        throw new ArgumentException($"Invalid {nameof(pattern)} '{pattern}'");
    }
  }
}
