namespace NJ.OpenAPIModel;

public class SpecificationExtensions
{
  public IReadOnlyDictionary<string, object> PatternsWithValues { get; init; }

  public SpecificationExtensions()
  {
    PatternsWithValues = new Dictionary<string, object>();
  }
}