using NJ.ApiaryToOpenApiMapper.Model;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class SampleValueProvider
  {
    public static object GetSampleValue(ApiTypeProperty apiaryTypeProperty)
    {
      if (apiaryTypeProperty.SampleValue is not null)
        return apiaryTypeProperty.SampleValue;
      object result = apiaryTypeProperty.TypeName switch
      {
        "number" => 0,
        "string" => "",
        _ => null
      };
      return result;
    }
  }
}
