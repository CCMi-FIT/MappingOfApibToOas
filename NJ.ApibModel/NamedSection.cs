namespace NJ.ApibModel;

public abstract class NamedSection
{
  public abstract string? Keyword { get; }
  public string? Identifier { get; init; }
  // TODO: Markdown ?
  public string? Description { get; init; }
  public string? Name => Identifier;
}