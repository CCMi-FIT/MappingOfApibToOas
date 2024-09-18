namespace NJ.ApibModel
{
  public abstract class AssetSection
  {
    public abstract string Keyword { get; }
    // TODO: Ensure that Content is preformated code block ?
    public string? Content { get; init; }

    protected AssetSection(string? content = default)
    {
      Content = content;
    }
  }
}
