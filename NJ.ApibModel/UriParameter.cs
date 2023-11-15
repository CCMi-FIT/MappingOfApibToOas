namespace NJ.ApibModel
{
  public class UriParameter
  {
    public string Name { get; set; }
    public object ExampleValue { get; set; }
    public string Description { get; set; }
    public string AdditionalDescription { get; set; }
    public object DefaultValue { get; set; }
    public string Type { get; set; }
    public bool Required { get; set; }

    // TODO Separate?
    public ICollection<object> Members { get; set; }

    public UriParameter(string name = default, bool required = default, string type = null, string description = null)
    {
      Name = name;
      Required = required;
      Type = type;
      Description = description;
    }
  }
}
