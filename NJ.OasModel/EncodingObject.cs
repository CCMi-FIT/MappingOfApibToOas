namespace NJ.OasModel;

public class EncodingObject
{
  public string ContentType { get; init; }
  public IReadOnlyDictionary<string, IHeaderOrReferenceObject> Headers { get; init; }
  public string Style { get; init; }
  public bool Explode { get; init; }
  public bool AllowReserved { get; init; }
}