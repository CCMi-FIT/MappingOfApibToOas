using Newtonsoft.Json;

namespace NJ.OasModel;

public class MediaTypeObject
{
  public dynamic Example { get; init; }
  public SchemaObject Schema { get; init; }
  public IReadOnlyDictionary<string, IExampleOrReferenceObject> Examples { get; init; }
  public IReadOnlyDictionary<string, EncodingObject> Encoding { get; init; }
}