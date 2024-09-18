namespace NJ.ApibModel;

public class ResponseSection : PayloadSection
{
  public override string Keyword { get; } = "Response";
  public int HttpStatusCode { get; }

  public ResponseSection(int httpStatusCode = default, string? mediaType = default) : base(httpStatusCode.ToString(), mediaType)
  {
    HttpStatusCode = httpStatusCode;
  }
}