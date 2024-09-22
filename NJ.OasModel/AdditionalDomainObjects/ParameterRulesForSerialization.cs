using NJ.SharedModel;

namespace NJ.OasModel.AdditionalDomainObjects
{
  public abstract class ParameterRulesForSerialization
  {
  }

  public class ParameterSchemaStyleRulesForSerialization : ParameterRulesForSerialization
  {
    public StyleValue Style { get; }
    public bool Explode { get; }
    public bool AllowReserved { get; }
    public SchemaObject Schema { get; }
    // TODO: Validate that example matches schema and encoding properties
    // TODO: Validate that example and examples are mutually exclusive
    public object? Example { get; init; }
    public IReadOnlyDictionary<string, IExampleOrReferenceObject>? Examples { get; init; }

    public ParameterSchemaStyleRulesForSerialization(SchemaObject schema, ParameterIn parameterIn, bool allowReserved = default, bool? explode = default) : this(schema, GetDefaultStyleForIn(parameterIn), allowReserved, explode)
    {
    }

    public ParameterSchemaStyleRulesForSerialization(SchemaObject schema, StyleValue style, bool allowReserved = false, bool? explode = default)
    {
      Style = style;
      Explode = explode ?? Style == StyleValue.Form;
      AllowReserved = allowReserved;
      Schema = schema;
    }

    private static StyleValue GetDefaultStyleForIn(ParameterIn parameterIn)
    {
      var result = parameterIn switch
      {
        ParameterIn.Query => StyleValue.Form,
        ParameterIn.Header => StyleValue.Simple,
        ParameterIn.Path => StyleValue.Simple,
        ParameterIn.Cookie => StyleValue.Form,
        _ => throw new NotSupportedException(),
      };
      return result;
    }
  }

  public class ParameterContentRulesForSerialization : ParameterRulesForSerialization
  {
    public IReadOnlyDictionary<MediaRange, MediaTypeObject> Content { get; }

    public ParameterContentRulesForSerialization(IReadOnlyDictionary<MediaRange, MediaTypeObject> content)
    {
      if (content.Count != 1)
        throw new ArgumentException($"{nameof(content)} must have exactly 1 item");
      Content = content;
    }
  }
}
