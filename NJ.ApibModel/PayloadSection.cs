namespace NJ.ApibModel;

public abstract class PayloadSection : NamedSection
{
  public sealed override string Identifier { get; set; }
  public string MediaType { get; set; }
  public HeadersSection HeadersSection { get; set; }
  public AttributesSection AttributesSection { get; set; }
  public BodySection BodySection { get; set; }
  public SchemaSection SchemaSection { get; set; }

  protected PayloadSection(string identifier = default, string mediaType = default)
  {
    Identifier = identifier;
    MediaType = mediaType;
  }
}