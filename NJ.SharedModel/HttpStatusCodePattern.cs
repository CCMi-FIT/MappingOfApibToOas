namespace NJ.SharedModel
{
  public struct HttpStatusCodePattern
  {
    public string Pattern { get; }

    public HttpStatusCodePattern(string pattern)
    {
      if (!Validate(pattern))
        throw new ArgumentException($"Invalid {nameof(pattern)} '{pattern}'");
      Pattern = pattern;
    }

    public HttpStatusCodePattern(int code)
    {
      Pattern = code.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    private static bool Validate(string pattern)
    {
      if (pattern.Length != 3 || pattern[0] < '1' || pattern[0] > '5')
        return false;
      if (pattern[1] == 'x' && pattern[2] == 'x')
        return true;
      if (pattern[1] == 'x' != (pattern[2] == 'x'))
        return false;
      if (pattern[1] < '0' || pattern[1] > '9')
        return false;
      if (pattern[2] < '0' || pattern[1] > '9')
        return false;
      return true;
    }
  }
}
