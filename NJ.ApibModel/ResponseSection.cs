namespace NJ.ApibModel;

public class ResponseSection : PayloadSection
{
  public override string Keyword { get; set; } = "Response";
  public int HttpStatusCode { get; set; }

  public ResponseSection(int httpStatusCode = default, string mediaType = null)
  {
    HttpStatusCode = httpStatusCode;
    MediaType = mediaType;
  }
}