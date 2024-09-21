using NJ.OasModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Model
{
  public record OperationInfo(OperationObject OperationObject, string Path, HttpRequestMethod HttpRequestMethod);
}
