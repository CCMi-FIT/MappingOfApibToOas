namespace NJ.ApiaryToOpenApiMapper
{
  public static class Constants
  {
    public static readonly IReadOnlyDictionary<int, string> StatusCodesWithDescriptions = new Dictionary<int, string>
    {
      { 200, "OK" },
      { 201, "Created" },
      { 204, "No Content" },
      { 401, "Unauthorized" },
      { 500, "Internal Server Error" }
    };
  }
}
