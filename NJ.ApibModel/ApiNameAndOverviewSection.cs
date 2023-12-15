namespace NJ.ApibModel;

public class ApiNameAndOverviewSection : NamedSection
{
  public override string Keyword { get; set; } = "";
  public override string Identifier { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
}