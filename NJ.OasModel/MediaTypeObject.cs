namespace NJ.OasModel;

public class MediaTypeObject
{
  public SchemaObject? Schema { get; init; }
  // TODO: Example should be in correct format as specified by the media type
  // TODO: Example and Examples are mutually exclusive
  public dynamic Example { get; init; }
  public IReadOnlyDictionary<string, IExampleOrReferenceObject>? Examples { get; init; }
  // TODO: Keys must exist in the schema as a property
  public IReadOnlyDictionary<string, EncodingObject>? Encoding { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}