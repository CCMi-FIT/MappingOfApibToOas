namespace NJ.OpenAPIModel;

public class ReferenceObject : IPathItemOrReferenceObject, IResponseOrReferenceObject, IParameterOrReferenceObject, IExampleOrReferenceObject, IRequestBodyOrReferenceObject, IHeaderOrReferenceObject, ISecuritySchemeOrReferenceObject, ICallbackOrReferenceObject
{
  public string Ref { get; init; }
  public string Summary { get; init; }
  public string Description { get; init; }
}