namespace NJ.OasModel;

public class RequestBodyObject : IRequestBodyOrReferenceObject
{
  public string Description { get; init; }
  public IReadOnlyDictionary<string, MediaTypeObject> Content { get; init; }
  public bool Required { get; init; }
}