namespace NJ.ApibToOasMapper.Model
{
  public record ApiArrayType(string Name, ApiType ItemType) : ApiType(Name);
}
