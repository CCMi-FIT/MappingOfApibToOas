using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class PathsObject
{
  // TODO: Validate distinct PathString (including templates)
  public IReadOnlyDictionary<PathString, PathItemObject>? PathItems { get; init; }
}