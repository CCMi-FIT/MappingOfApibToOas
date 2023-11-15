namespace NJ.OpenAPIModel;

public class ExampleObject : IExampleOrReferenceObject
{
  public string Summary { get; init; }
  public string Description { get; init; }
  // TODO: Potential Mutability?
  public object Value { get; init; }
  public string ExternalValue { get; init; }
}