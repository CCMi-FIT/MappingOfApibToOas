namespace NJ.OasModel;

public class PathItemObject : IPathItemOrReferenceObject
{
  public string Ref { get; init; }
  public string Summary { get; init; }
  public string Description { get; init; }
  public OperationObject Get { get; init; }
  public OperationObject Put { get; init; }
  public OperationObject Post { get; init; }
  public OperationObject Delete { get; init; }
  public OperationObject Options { get; init; }
  public OperationObject Head { get; init; }
  public OperationObject Patch { get; init; }
  public OperationObject Trace { get; init; }
  public IReadOnlyCollection<ServerObject> Servers { get; init; }
  public IReadOnlyCollection<IParameterOrReferenceObject> Parameters { get; init; }
}