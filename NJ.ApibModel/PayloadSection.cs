namespace NJ.ApibModel;

public abstract class PayloadSection : NamedSection
{
  public string? MediaType { get; init; }
  // TODO: At least one of the following should be present
  public HeadersSection? HeadersSection { get; init; }
  public AttributesSection? AttributesSection { get; init; }
  public BodySection? BodySection { get; init; }
  public SchemaSection? SchemaSection { get; init; }

  protected PayloadSection(string? identifier = default, string? mediaType = default)
  {
    Identifier = identifier;
    MediaType = mediaType;
  }
}