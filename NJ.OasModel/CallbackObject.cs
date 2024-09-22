namespace NJ.OasModel;

public class CallbackObject : ICallbackOrReferenceObject
{
  public IReadOnlyDictionary<string, IPathItemOrReferenceObject> PathItemOrReferenceObjects { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}