namespace NJ.ApibToOasMapper.Model
{
  public record ApiPropertyType(string Name, ApiType Parent = null) : ApiType(Name)
  {
    public IReadOnlyCollection<ApiTypeProperty> Properties { get; }

    public ApiPropertyType(string name, IEnumerable<ApiTypeProperty> properties, ApiType parent = null) : this(name, parent)
    {
      if (properties is null)
        Properties = new List<ApiTypeProperty>();
      else
        Properties = properties.ToList();
    }

    public IEnumerable<ApiTypeProperty> PropertiesWithParentProperties
    {
      get
      {
        foreach (var prop in Properties)
          yield return prop;
        if (Parent is not ApiPropertyType namedParentPropertyType)
          yield break;
        var parentProperties = namedParentPropertyType.PropertiesWithParentProperties;
        foreach (var prop in parentProperties)
          yield return prop;
      }
    }
  }
}
