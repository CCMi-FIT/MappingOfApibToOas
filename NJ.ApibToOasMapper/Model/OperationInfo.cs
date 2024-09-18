using NJ.ApibModel.AdditionalDomainObjects;
using NJ.OasModel;

namespace NJ.ApibToOasMapper.Model
{
    public record OperationInfo(OperationObject OperationObject, string Path, HttpRequestMethod HttpRequestMethod);
}
