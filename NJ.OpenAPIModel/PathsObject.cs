using Newtonsoft.Json;
using NJ.OpenAPIModel.JsonConverters;

namespace NJ.OpenAPIModel;

[JsonConverter(typeof(PathsObjectJsonConverter))]
public class PathsObject
{
  public IReadOnlyDictionary<string, PathItemObject> PathItems { get; init; }
}