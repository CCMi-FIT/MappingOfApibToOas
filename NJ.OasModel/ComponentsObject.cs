using OneOf;

namespace NJ.OasModel;

public class ComponentsObject
{
  public IReadOnlyDictionary<string, SchemaObject>? Schemas { get; init; }
  public IReadOnlyDictionary<string, OneOf<ResponseObject, ReferenceObject>>? Responses { get; init; }
  public IReadOnlyDictionary<string, OneOf<ParameterObject, ReferenceObject>>? Parameters { get; init; }
  public IReadOnlyDictionary<string, OneOf<RequestBodyObject, ReferenceObject>>? RequestBodies { get; init; }
  public IReadOnlyDictionary<string, OneOf<HeaderObject, ReferenceObject>>? Headers { get; init; }
  public IReadOnlyDictionary<string, OneOf<SecuritySchemeObject, ReferenceObject>>? SecuritySchemes { get; init; }
  public IReadOnlyDictionary<string, OneOf<LinkObject, ReferenceObject>>? Links { get; init; }
  public IReadOnlyDictionary<string, OneOf<CallbackObject, ReferenceObject>>? Callbacks { get; init; }
  public IReadOnlyDictionary<string, OneOf<PathItemObject, ReferenceObject>>? PathItems { get; init; }

  public SpecificationExtensions? SpecificationExtensions { get; init; }
}