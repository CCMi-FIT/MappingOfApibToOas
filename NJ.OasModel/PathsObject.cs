using Newtonsoft.Json;
using NJ.OasModel.JsonConverters;

namespace NJ.OasModel;

[JsonConverter(typeof(PathsObjectJsonConverter))]
public class PathsObject
{
  public IReadOnlyDictionary<string, PathItemObject> PathItems { get; init; }
}