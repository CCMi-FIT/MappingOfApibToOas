using NJ.SharedModel;

namespace NJ.ApibModel
{
  public class UriParameter
  {
    public string Name { get; }
    public Description? Description { get; init; }
    public Description? AdditionalDescription { get; init; }
    public dynamic? ExampleValue { get; init; }
    public dynamic? DefaultValue { get; init; }
    public string Type { get; }
    public IReadOnlyCollection<dynamic?>? Members { get; }
    public bool Required { get; init; }
    public bool Optional { get; init; }

    public UriParameter(string name, string type = "string", ICollection<dynamic?>? members = default)
    {
      Name = name;
      Type = type;
      if (members is not null)
      {
        Members = members.ToList();
        // TODO: Validate that type is "enum[...]"
      }
    }
  }
}
