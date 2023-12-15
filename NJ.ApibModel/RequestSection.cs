namespace NJ.ApibModel;

public class RequestSection : PayloadSection
{
  public override string Keyword { get; set; } = "Request";

  public RequestSection(string identifier = null, string mediaType = null) : base(identifier, mediaType)
  {
  }
}