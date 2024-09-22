using NJ.ApibModel.AdditionalDomainObjects;

namespace NJ.ApibModel;

public class AttributesSection
{
  public virtual string Keyword { get; } = "Attributes";
  public string Identifier { get; }

  // TODO: Utilize MsonTypeDefinition instead ?
  public IReadOnlyCollection<AttributeSection> Attributes { get; }

  public AttributesSection(string identifier = "object", IEnumerable<AttributeSection>? attributes = default)
  {
    Identifier = identifier;
    if (attributes is null)
      Attributes = new List<AttributeSection>();
    else
      Attributes = attributes.ToList();
  }

  public AttributesSection(IEnumerable<AttributeSection> attributes) : this("object", attributes)
  {
  }
}