using Newtonsoft.Json;

namespace NJ.OasToApibMapper.Tests.JsonHelpers
{
  public class LowerCaseJsonReader : JsonTextReader
  {
    public LowerCaseJsonReader(TextReader textReader) : base(textReader)
    {
    }

    public override object Value
    {
      get
      {
        var valueAsString = (string)base.Value;
        if (valueAsString is not null)
          return valueAsString.ToLower();
        return base.Value;
      }
    }
  }
}
