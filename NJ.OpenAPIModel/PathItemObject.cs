using Newtonsoft.Json;

namespace NJ.OpenAPIModel;

public class PathItemObject : IPathItemOrReferenceObject
{
  public string Ref { get; init; }
  public string Summary { get; init; }
  public string Description { get; init; }

  [JsonProperty(PropertyName = "get")]
  public OperationObject Get { get; init; }
  [JsonProperty(PropertyName = "put")]
  public OperationObject Put { get; init; }
  [JsonProperty(PropertyName = "post")]
  public OperationObject Post { get; init; }
  [JsonProperty(PropertyName = "delete")]
  public OperationObject Delete { get; init; }
  [JsonProperty(PropertyName = "options")]
  public OperationObject Options { get; init; }
  [JsonProperty(PropertyName = "head")]
  public OperationObject Head { get; init; }
  [JsonProperty(PropertyName = "patch")]
  public OperationObject Patch { get; init; }
  [JsonProperty(PropertyName = "trace")]
  public OperationObject Trace { get; init;}
  public IReadOnlyCollection<ServerObject> Servers { get; init; }
  public IReadOnlyCollection<IParameterOrReferenceObject> Parameters { get; init; }
}