using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel;

public class EncodingObject
{
  public string? ContentType { get; init; }
  public IReadOnlyDictionary<string, IHeaderOrReferenceObject>? Headers { get; init; }
  public StyleValue? Style { get; init; }
  public bool Explode { get; init; }
  public bool AllowReserved { get; init; }

  public EncodingObject(string contentType, StyleValue style, bool allowReserved = false, bool? explode = default)
  {
    ContentType = contentType;
    Style = style;
    Explode = explode ?? style == StyleValue.Form;
    AllowReserved = allowReserved;
  }

  public EncodingObject(string contentType, ParameterIn @in, bool allowReserved = false, bool? explode = default) : this(contentType, GetDefaultStyle(@in), allowReserved, explode)
  {
  }

  public EncodingObject(PropertyType propertyType, StyleValue style, bool allowReserved = false, bool? explode = default) : this(GetDefaultContentType(propertyType), style, allowReserved, explode)
  {
  }

  public EncodingObject(PropertyType propertyType, ParameterIn @in, bool allowReserved = false, bool? explode = default) : this(GetDefaultContentType(propertyType), GetDefaultStyle(@in), allowReserved, explode)
  {
  }

  // TODO: Cleaner propertyType?
  private static string GetDefaultContentType(PropertyType propertyType)
  {
    if (propertyType == PropertyType.Object)
      return "application/json";
    if (propertyType == PropertyType.Array)
      return "application/json"; // TODO: Based on inner type ??
    return "application/octet-stream";
  }
  private static StyleValue GetDefaultStyle(ParameterIn parameterIn)
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