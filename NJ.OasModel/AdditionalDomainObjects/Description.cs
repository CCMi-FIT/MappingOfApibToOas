namespace NJ.OasModel.AdditionalDomainObjects
{
  public abstract class Description
  {
  }

  public class PlainTextDescription : Description
  {
    public string Description { get; init; }
  }

  // TODO: Better ?
  public class MarkdownDescription : Description
  {
    public string Description { get; init; }
  }
}
