namespace NJ.ApibModel;

public class UriParametersSection
{
  public string Keyword { get; } = "Parameters";

  public UriParametersSection(IEnumerable<UriParameter> parameters)
  {
    Parameters = parameters.ToList();
  }

  public IReadOnlyCollection<UriParameter> Parameters { get; set; }

}