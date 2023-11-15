namespace NJ.ApibModel;

public class UriParametersSection
{
  public UriParametersSection(IEnumerable<UriParameter> parameters = null)
  {
    if (parameters is not null)
      Parameters = parameters.ToList();
    else
      Parameters = new List<UriParameter>();
  }

  public ICollection<UriParameter> Parameters { get; set; }

}