using NJ.SharedModel;

namespace NJ.ApibModel
{
  public class DataStructureSection : AttributesSection
  {
    public override string Keyword { get; } = "Data Structures";
    public DataStructureSection(MsonTypeDefinition typeDefinition) : base(typeDefinition)
    {
    }
  }
}
