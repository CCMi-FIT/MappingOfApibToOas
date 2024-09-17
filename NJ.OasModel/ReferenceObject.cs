using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class ReferenceObject : IPathItemOrReferenceObject, IResponseOrReferenceObject, IParameterOrReferenceObject, IExampleOrReferenceObject, IRequestBodyOrReferenceObject, IHeaderOrReferenceObject, ISecuritySchemeOrReferenceObject, ICallbackOrReferenceObject
{
  public Uri Ref { get; }
  public string? Summary { get; init; }
  public Description? Description { get; init; }

  public ReferenceObject(string @ref) : this(new Uri(@ref))
  {
  }

  public ReferenceObject(Uri @ref)
  {
    Ref = @ref;
  }
}