using NJ.SharedModel;
using OneOf;

namespace NJ.OasModel;

public class ResponseObject : IResponseOrReferenceObject
{
  public Description Description { get; }

  public IReadOnlyDictionary<string, OneOf<HeaderObject, ReferenceObject>>? Headers { get; init; }
  public IReadOnlyDictionary<MediaType, MediaTypeObject>? Content { get; init; }
  // TODO: Validate that key is a short name for the link
  public IReadOnlyDictionary<string, OneOf<LinkObject, ReferenceObject>>? Links { get; init; }

  public ResponseObject(Description description)
  {
    Description = description;
  }
}