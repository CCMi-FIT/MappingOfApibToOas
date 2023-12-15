namespace NJ.OasModel;

public class ResponseObject : IResponseOrReferenceObject
{
  public string Description { get; init; }
  public IReadOnlyDictionary<string, IHeaderOrReferenceObject> Headers { get; init; }
  public IReadOnlyDictionary<string, MediaTypeObject> Content { get; init; }
  public IReadOnlyDictionary<string, ILinkOrReferenceObject> Links { get; init; }
}