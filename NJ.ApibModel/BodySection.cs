namespace NJ.ApibModel;

public class BodySection : AssetSection
{
  public override string Keyword { get; } = "Body";
  public BodySection(string content) : base(content)
  {
  }
}