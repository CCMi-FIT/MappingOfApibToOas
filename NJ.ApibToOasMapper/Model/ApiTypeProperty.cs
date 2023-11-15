namespace NJ.ApibToOasMapper.Model
{
  public record ApiTypeProperty(string TypeName = default, string PropertyName = default, bool Required = default, object SampleValue = default, object DefaultValue = default, string Description = default);
}
