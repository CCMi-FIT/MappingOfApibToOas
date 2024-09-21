using NJ.SharedModel;

namespace NJ.OasModel;

public class RequestBodyObject : IRequestBodyOrReferenceObject
{
  public Description? Description { get; init; }
  public IReadOnlyDictionary<MediaRange, MediaTypeObject> Content { get; }
  public bool Required { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }

  public RequestBodyObject(IEnumerable<KeyValuePair<MediaRange, MediaTypeObject>> content)
  {
    Content = content.ToDictionary(c => c.Key, c => c.Value);
  }
}