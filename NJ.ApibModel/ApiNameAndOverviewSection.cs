namespace NJ.ApibModel;

public class ApiNameAndOverviewSection : NamedSection
{
  public override string? Keyword { get; } = "";

  public ApiNameAndOverviewSection(string identifier, string descriptionText) : base(identifier, descriptionText)
  {
  }
}