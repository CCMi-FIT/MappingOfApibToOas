using NJ.SharedModel;

namespace NJ.ApibModel;

public abstract class NamedSection
{
  public abstract string? Keyword { get; }
  public string? Identifier { get; init; }
  public Description? Description { get; init; }
  public string? Name => Identifier;
}