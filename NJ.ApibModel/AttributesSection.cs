using NJ.SharedModel;

namespace NJ.ApibModel;

public class AttributesSection
{
  public virtual string Keyword { get; } = "Attributes";

  public MsonTypeDefinition TypeDefinition { get; }

  // TODO: Get Attributes from TypeDefinition ?
  //public ICollection<AttributeSection> Attributes { get; }

  public AttributesSection(MsonTypeDefinition typeDefinition)
  {
    TypeDefinition = typeDefinition;
  }

  public AttributesSection(string typeDefinition) : this(new MsonTypeDefinition(typeDefinition))
  {
  }
}