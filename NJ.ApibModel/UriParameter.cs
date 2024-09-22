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

    public UriParameter(string name, Description? description = default, string type = "string", ICollection<dynamic?>? members = default)
    {
      Name = name;
      Description = description;
      Type = type;
      if (members is not null)
      {
        Members = members.ToList();
        // TODO: Validate that type is "enum[...]"
      }
    }

    public UriParameter(string name, string? description = default, string type = "string", ICollection<dynamic?>? members = default) : this(name, description is not null ? new PlainTextDescription(description) : default, type, members)
    {
    }

    public UriParameter(string name, bool required, string type = "string", string? description = default, ICollection<dynamic?>? members = default) : this(name, description, type, members)
    {
      Required = required;
    }
  }
}
