namespace NJ.OpenAPIModel;

public class CallbackObject : ICallbackOrReferenceObject
{
  public IReadOnlyDictionary<string, IPathItemOrReferenceObject> PathItemOrReferenceObjects { get; init; }
}