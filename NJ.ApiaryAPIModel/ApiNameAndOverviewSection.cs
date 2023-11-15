namespace NJ.ApiaryAPIModel;

public class ApiNameAndOverviewSection : NamedSection
{
  public override string Keyword { get; set; } = "";
  public override string Identifier { get; set; }

  // TODO: What about the inherited stuff?
  public string Name { get; set; }
  public string Description { get; set; }
}