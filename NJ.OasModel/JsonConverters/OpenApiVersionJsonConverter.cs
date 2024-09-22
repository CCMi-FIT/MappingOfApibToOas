using Newtonsoft.Json;
using NJ.OasModel.AdditionalDomainObjects;

namespace NJ.OasModel.JsonConverters
{
  public class OpenApiVersionJsonConverter : JsonConverter<OpenApiVersion>
  {
    public override OpenApiVersion? ReadJson(JsonReader reader, Type objectType, OpenApiVersion? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var versionString = (string)reader.Value!;
      var versionParts = versionString.Split('.');
      if (versionParts.Length != 3)
        throw new JsonSerializationException("Invalid version string");
      var result = new OpenApiVersion(versionParts[0], versionParts[1], versionParts[2]);
      return result;
    }

    public override void WriteJson(JsonWriter writer, OpenApiVersion? value, JsonSerializer serializer)
    {
      // Write the version as a string in the "Major.Minor" format
      writer.WriteValue($"{value!.Major}.{value.Minor}.{value.Patch}");
    }
  }
}
