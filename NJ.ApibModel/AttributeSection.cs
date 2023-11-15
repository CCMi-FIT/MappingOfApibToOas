namespace NJ.ApibModel
{
  public class AttributeSection
  {
    public string Name { get; set; }
    public bool Required { get; set; }
    public string TypeName { get; set; }
    public object SampleValue { get; set; }
    public string Description { get; set; }
    public object Default { get; set; }

    public AttributeSection(string name = default, string description = default, bool required = default, string type = default, object sampleValue = default, object @default = null)
    {
      Name = name;
      Description = description;
      Required = required;
      TypeName = type;
      SampleValue = sampleValue;
      Default = @default;
    }
  }
}
