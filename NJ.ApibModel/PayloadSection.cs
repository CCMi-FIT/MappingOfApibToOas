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

  protected PayloadSection(string? identifier, string? mediaType) : this(identifier, mediaType is not null ? new MediaType(mediaType) : default)
  {
  }

  protected PayloadSection(string? identifier, string? mediaType, string? description = default) : this(identifier, mediaType is not null ? new MediaType(mediaType) : default, description)
  {
  }

  protected PayloadSection(string? identifier, MediaType? mediaType, string? descriptionText) : this(identifier, mediaType, descriptionText is not null ? new PlainTextDescription(descriptionText) : default)
  {
  }

  protected PayloadSection(string? identifier = default, MediaType? mediaType = default, Description? description = default)
  {
    Identifier = identifier;
    MediaType = mediaType;
  }
}