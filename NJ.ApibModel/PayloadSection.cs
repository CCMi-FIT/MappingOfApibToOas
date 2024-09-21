using NJ.SharedModel;

namespace NJ.ApibModel;

public abstract class PayloadSection : NamedSection
{
  public MediaType? MediaType { get; init; }
  // TODO: At least one of the following should be present
  public HeadersSection? HeadersSection { get; init; }
  public AttributesSection? AttributesSection { get; init; }
  public BodySection? BodySection { get; init; }
  public SchemaSection? SchemaSection { get; init; }

  protected PayloadSection(string? identifier = default, string? mediaType = default) : this(identifier, mediaType is not null ? new MediaType(mediaType) : default)
  {
  }

  protected PayloadSection(string? identifier = default, MediaType? mediaType = default)
  {
    Identifier = identifier;
    MediaType = mediaType;
  }
}