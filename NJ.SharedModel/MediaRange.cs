namespace NJ.SharedModel
{
  public record MediaRange
  {
    private const string _regexPattern = @"^[^/]+/(?:\*|[^/]+)$";

    public string Pattern { get; }

    public MediaRange(string pattern)
    {
      if (!System.Text.RegularExpressions.Regex.IsMatch(pattern, _regexPattern))
        throw new ArgumentException($"Invalid {nameof(pattern)} '{pattern}'");
      Pattern = pattern;
    }
  }
}
