namespace NJ.SharedModel
{
  // TODO: Support all MSON Specificaiton ? - https://github.com/apiaryio/mson/blob/master/MSON%20Specification.md
  public class MsonTypeDefinition
  {
    public string Identifier { get; }
    public string ParentTypeIdentifier { get; }

    public IReadOnlyDictionary<string, MsonTypeDefinition>? Members { get; init; }

    public MsonTypeDefinition(string identifier, string parentTypeIdentifier = "object")
    {
      Identifier = identifier;
      ParentTypeIdentifier = parentTypeIdentifier;
    }
  }
}
