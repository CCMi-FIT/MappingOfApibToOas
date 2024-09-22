using NJ.ApibModel.AdditionalDomainObjects;

namespace NJ.ApibModel
{
  public class DataStructuresSection
  {
    public string Keyword { get; } = "Data Structures";
    IReadOnlyCollection<AttributesSection> Attributes { get; }
    public DataStructuresSection(IEnumerable<AttributesSection>? attributes = default)
    {
      if (attributes is null)
        Attributes = new List<AttributesSection>();
      else
        Attributes = attributes.ToList();
    }
  }
}
