namespace NJ.ApibModel;

public class AttributesSection
{
  public string TypeDefinition { get; set; }
  public ICollection<AttributeSection> Attributes { get; set; }
}