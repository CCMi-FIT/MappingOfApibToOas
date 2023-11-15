using NJ.ApiaryAPIModel;
using NJ.OpenAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Model
{
  public record OperationInfo(OperationObject OperationObject, string Path, HttpRequestMethod HttpRequestMethod);
}
