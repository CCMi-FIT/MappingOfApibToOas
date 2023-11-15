namespace NJ.ApiaryToOpenApiMapper.Model
{
  internal record ApiTypeInternal(string Name, string ParentName = null)
  {
    public IReadOnlyCollection<ApiTypeProperty> Properties { get; }

    public ApiTypeInternal(string name, IEnumerable<ApiTypeProperty> properties, string parentName = null) : this(
      name, parentName)
    {
      if (properties is null)
        Properties = new List<ApiTypeProperty>();
      else
        Properties = properties.ToList();
    }
  }
}
