using System.Collections.Generic;
using Newtonsoft.Json;

namespace NJ.Common.EqComparers
{
  public class NewtonsoftSerializationEqComparer : IEqualityComparer<object?>
  {
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
      TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
      ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
      TypeNameHandling = TypeNameHandling.All,
      PreserveReferencesHandling = PreserveReferencesHandling.All
    };

    public new bool Equals(object? x, object? y)
    {
      var refsEqual = ReferenceEquals(x, y);
      if (refsEqual)
        return true;

      if (x is null || y is null)
        return false;

      var serializedX = JsonConvert.SerializeObject(x, _settings);
      var serializedY = JsonConvert.SerializeObject(y, _settings);
      var result = serializedX == serializedY;
      return result;
    }

    public int GetHashCode(object? obj)
    {
      if (obj is null)
        return 0;

      var result = JsonConvert.SerializeObject(obj, _settings).GetHashCode();
      return result;
    }
  }
}
