namespace NJ.OasModel;

public class LinkObject
{
  public string OperationRef { get; init; }
  public string OperationId { get; init; }
  public IReadOnlyDictionary<string, object> Parameters { get; init; }
  public object RequestBody { get; init; }
  public string Description { get; init; }
  public ServerObject Server { get; init; }
}