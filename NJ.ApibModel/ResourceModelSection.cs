namespace NJ.ApibModel;

public class ResourceModelSection : PayloadSection
{
  public override string Keyword { get; } = "Model";

  public ResourceModelSection(string? identifier = default, string? mediaType = default, string? description = default) : base(default, mediaType, description)
  {
  }
}