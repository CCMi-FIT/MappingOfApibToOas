namespace NJ.ApiaryToOpenApiMapper.Model
{
  public record ApiArrayType(string Name, ApiType ItemType) : ApiType(Name);
}
