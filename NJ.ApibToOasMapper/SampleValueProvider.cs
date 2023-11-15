using NJ.ApibToOasMapper.Model;

namespace NJ.ApibToOasMapper
{
  public static class SampleValueProvider
  {
    public static object GetSampleValue(ApiTypeProperty apiTypeProperty)
    {
      if (apiTypeProperty.SampleValue is not null)
        return apiTypeProperty.SampleValue;
      object result = apiTypeProperty.TypeName switch
      {
        "number" => 0,
        "string" => "",
        _ => null
      };
      return result;
    }
  }
}
