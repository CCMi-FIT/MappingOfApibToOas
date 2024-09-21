using NJ.ApibModel.AdditionalDomainObjects;

namespace NJ.ApibModel
{
  public class DataStructureSection : AttributesSection
  {
    public override string Keyword { get; } = "Data Structures";
    public DataStructureSection(string identifier, IEnumerable<AttributeSection>? attributes = default) : base(identifier, attributes)
    {
    }
  }
}
