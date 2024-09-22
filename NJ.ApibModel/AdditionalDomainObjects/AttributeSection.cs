namespace NJ.ApibModel.AdditionalDomainObjects
{
  // TODO: Use MsonTypeDefinition Intead ?
  public class AttributeSection
  {
    public string Name { get; }
    public string TypeName { get; }
    public bool Required { get; init; }
    public dynamic? SampleValue { get; init; }
    public string? Description { get; init; }
    public dynamic? Default { get; init; }

    public AttributeSection(string name, string typeName, string? description = default, bool required = default, dynamic? sampleValue = default, dynamic? @default = default)
    {
      Name = name;
      Description = description;
      Required = required;
      TypeName = typeName;
      SampleValue = sampleValue;
      Default = @default;
    }
  }
}
