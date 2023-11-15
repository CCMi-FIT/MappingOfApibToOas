namespace NJ.ApibModel;

public class ResourceModelSection : PayloadSection
{
  public override string Keyword { get; set; } = "Model";
  public string Description { get; set; }
}