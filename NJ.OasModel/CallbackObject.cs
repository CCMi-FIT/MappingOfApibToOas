using OneOf;

namespace NJ.OasModel;

public class CallbackObject : ICallbackOrReferenceObject
{
  public IReadOnlyDictionary<string, OneOf<PathItemObject, ReferenceObject>> PathItemOrReferenceObjects { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}