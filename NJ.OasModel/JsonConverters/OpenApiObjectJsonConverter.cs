using Newtonsoft.Json;
using NJ.Common.Extensions;

namespace NJ.OasModel.JsonConverters
{
  public class OpenApiObjectJsonConverter : JsonConverter<OpenApiObject>
  {
    public override OpenApiObject? ReadJson(JsonReader reader, Type objectType, OpenApiObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, OpenApiObject? value, JsonSerializer serializer)
    {
      if (value is null)
        throw new ArgumentNullException(nameof(value));

      if (value == null)
        throw new ArgumentNullException(nameof(value));

      writer.WriteStartObject();

      writer.WritePropertyName(nameof(value.OpenApi));
      serializer.Serialize(writer, value.OpenApi);

      writer.WritePropertyName(nameof(value.Info));
      serializer.Serialize(writer, value.Info);

      if (ShouldSerializeServers(value.Servers))
      {
        writer.WritePropertyName(nameof(value.Servers));
        serializer.Serialize(writer, value.Servers);
      }

      writer.WritePropertyName(nameof(value.Paths));
      serializer.Serialize(writer, value.Paths);

      writer.WritePropertyName(nameof(value.Components));
      serializer.Serialize(writer, value.Components);

      if (ShouldSerializeSecurity(value.Security))
      {
        writer.WritePropertyName(nameof(value.Security));
        serializer.Serialize(writer, value.Security);
      }

      writer.WritePropertyName(nameof(value.Tags));
      serializer.Serialize(writer, value.Tags);

      writer.WriteEndObject();
    }

    private static bool ShouldSerializeServers(IReadOnlyCollection<ServerObject>? servers)
    {
      if (servers.IsNullOrEmpty())
        return false;
      if (servers!.Count != 1)
        return true;
      var server = servers.Single();
      if (server.Url.String != "/" && server.Variables.IsNullOrEmpty())
        return false;
      return true;
    }

    private static bool ShouldSerializeSecurity(IReadOnlyCollection<SecurityRequirementObject>? security)
    {
      if (security.IsNullOrEmpty())
        return false;
      if (security!.Count != 1)
        return true;
      var securityRequirement = security.Single();
      if (securityRequirement.SchemeNamesWithScopeNames.IsNullOrEmpty())
        return false;
      return true;
    }
  }
}
