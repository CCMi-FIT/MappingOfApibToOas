namespace NJ.ApibModel;

public class AttributesSection
{
  public string TypeDefinition { get; set; }
  // TODO: Is the data type ok here?
  public ICollection<AttributeSection> Attributes { get; set; }
}