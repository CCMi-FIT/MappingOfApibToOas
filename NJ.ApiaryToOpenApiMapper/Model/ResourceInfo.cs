using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Model
{
  public record ResourceInfo(ResourceSection ResourceSection, ResourceGroupSection ResourceGroupSection = null);
}
