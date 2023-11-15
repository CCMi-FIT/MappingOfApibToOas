﻿namespace NJ.OpenAPIModel;

public class LinkObject
{
  public string OperationRef { get; init; }
  public string OperationId { get; init; }
  // TODO: object | {expression} ???
  public IReadOnlyDictionary<string, object> Parameters { get; init; }
  // TODO: Potential Mutability?
  public object RequestBody { get; init; }
  public string Description { get; init; }
  public ServerObject Server { get; init; }

  // TODO: Extension with Specification Extensions?
}