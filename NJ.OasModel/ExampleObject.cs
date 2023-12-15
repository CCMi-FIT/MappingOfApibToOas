namespace NJ.OasModel;

public class ExampleObject : IExampleOrReferenceObject
{
  public string Summary { get; init; }
  public string Description { get; init; }
  public object Value { get; init; }
  public string ExternalValue { get; init; }
}