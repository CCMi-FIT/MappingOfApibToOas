namespace NJ.ApibModel
{
  public abstract class AssetSection
  {
    public abstract string Keyword { get; set; }
    public string Content { get; set; }

    protected AssetSection(string content = null)
    {
      Content = content;
    }
  }
}
