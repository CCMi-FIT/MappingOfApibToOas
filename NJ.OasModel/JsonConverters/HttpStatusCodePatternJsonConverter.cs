using Newtonsoft.Json;
using NJ.OasModel.AdditionalDomainObjects;
using NJ.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJ.OasModel.JsonConverters
{
  public class HttpStatusCodePatternJsonConverter : JsonConverter<HttpStatusCodePattern>
  {
    public override HttpStatusCodePattern ReadJson(JsonReader reader, Type objectType, HttpStatusCodePattern existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var @string = serializer.Deserialize<string>(reader)!;
      var result = new HttpStatusCodePattern(@string);
      return result;
    }

    public override void WriteJson(JsonWriter writer, HttpStatusCodePattern value, JsonSerializer serializer)
    {
      serializer.Serialize(writer, value.Pattern, typeof(string));
    }
  }
}
