using NJ.SharedModel;

namespace NJ.ApibModel;

public abstract class NamedSection
{
  public abstract string? Keyword { get; }
  public string? Identifier { get; init; }
  public Description? Description { get; init; }
  public string? Name => Identifier;

  protected NamedSection(string? identifier, string? description) : this(identifier, description is not null ? new PlainTextDescription(description) : null)
  {
  }

  protected NamedSection(string? identifier, Description? description) : this(identifier)
  {
    Description = description;
  }

  protected NamedSection(string? identifier = default)
  {
    Identifier = identifier;
  }
}