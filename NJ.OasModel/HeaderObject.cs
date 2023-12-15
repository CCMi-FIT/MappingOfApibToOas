using Newtonsoft.Json;

namespace NJ.OasModel;

public class HeaderObject : IHeaderOrReferenceObject
{
  public string Description { get; init; }
  public bool Required { get; init; }
  public bool Deprecated { get; init; }
  public bool AllowEmptyValue { get; init; }
  public string Style { get; init; }
  public bool Explode { get; init; }
  public bool AllowReserved { get; init; }
  public SchemaObject Schema { get; init; }
  public object Example { get; init; }
  public IReadOnlyDictionary<string, IExampleOrReferenceObject> Examples { get; init; }
  public IReadOnlyDictionary<string, MediaTypeObject> Content { get; init; }
}