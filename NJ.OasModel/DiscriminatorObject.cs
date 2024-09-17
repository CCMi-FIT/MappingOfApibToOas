namespace NJ.OasModel;

// TODO: Discriminator Object is legal only when using oneOf / anyOf / allOf
public class DiscriminatorObject
{
  // Name of the property in the payload that will hold the discriminator value
  // TODO: Validate that property name is in response payload ?
  public string PropertyName { get; init; }
  // An object to hold mappings between payload values and schema names or referneces
  // TODO: Validate?
  public IReadOnlyDictionary<string, string>? Mapping { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}