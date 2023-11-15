using System.Collections;

namespace NJ.ApiaryAPIModel;

public class HeadersSection : IEnumerable<KeyValuePair<string, object>>
{
  public IDictionary<string, object> KeysWithValues { get; set; }

  public HeadersSection(IDictionary<string, object> keysWithValues = null)
  {
    KeysWithValues = keysWithValues ?? new Dictionary<string, object>();
  }

  public void Add(string key, string value)
  {
    KeysWithValues.Add(key, value);
  }

  public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => KeysWithValues.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}