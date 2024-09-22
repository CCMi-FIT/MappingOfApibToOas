namespace NJ.OasModel.AdditionalDomainObjects
{
  public struct PathString
  {
    private const string _segmentRegex = @"[a-zA-Z0-9\.\-_]+";
    private const string _templatedSegmentRegex = $@"{{{_segmentRegex}}}";
    private const string _pathRegex = $@"^/(?:(?:{_segmentRegex})|(?:{_templatedSegmentRegex})/)*$";

    public string String { get; }

    public PathString(string @string)
    {
      if (!System.Text.RegularExpressions.Regex.IsMatch(@string, _pathRegex))
        throw new ArgumentException($"Invalid {nameof(@string)}: '{@string}'");
      String = @string;
    }
  }
}
