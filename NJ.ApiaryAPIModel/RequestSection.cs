namespace NJ.ApiaryAPIModel;

public class RequestSection : PayloadSection
{
  public override string Keyword { get; set; } = "Request";
  public string MediaType { get; set; }

  public RequestSection(string identifier = null, string mediaType = null) : base(identifier)
  {
    MediaType = mediaType;
  }
}