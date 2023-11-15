namespace NJ.OpenAPIModel;

public class EncodingObject
{
  public string ContentType { get; init; }
  public IReadOnlyDictionary<string, IHeaderOrReferenceObject> Headers { get; init; }
  // TODO: Better object?
  public string Style { get; init; }
  public bool Explode { get; init; }
  public bool AllowReserved { get; init; }
}