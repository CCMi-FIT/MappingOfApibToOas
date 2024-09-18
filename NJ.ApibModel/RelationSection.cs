namespace NJ.ApibModel;

public class RelationSection
{
  public string Keyword { get; } = "Relation";
  public string Identifier { get; }

  // TODO: Check uniqueness of identifiersacross whole document
  public RelationSection(string identifier)
  {
    Identifier = identifier;
  }
}