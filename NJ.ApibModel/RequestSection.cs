namespace NJ.ApibModel;

public class RequestSection : PayloadSection
{
  public override string Keyword { get; } = "Request";

  public RequestSection(string? identifier = default, string? mediaType = default) : base(identifier, mediaType)
  {
  }
}