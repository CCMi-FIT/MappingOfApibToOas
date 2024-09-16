using Newtonsoft.Json;
using NJ.OasModel.AdditionalDomainObjects;
using NJ.OasModel.JsonConverters;

namespace NJ.OasModel;

[JsonConverter(typeof(PathsObjectJsonConverter))]
public class PathsObject
{
  // TODO: Validate distinct PathString (including templates)
  public IReadOnlyDictionary<PathString, PathItemObject>? PathItems { get; init; }
}