using System.Collections;

namespace NJ.ApibModel;

public class MetadataSection : IEnumerable<KeyValuePair<string, string>>
{
  public IDictionary<string, string> KeysWithValues { get; set; }

  public MetadataSection(IDictionary<string, string> keysWithValues = null)
  {
    KeysWithValues = keysWithValues ?? new Dictionary<string, string>();
  }

  public void Add(string key, string value)
  {
    KeysWithValues.Add(key, value);
  }

  public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => KeysWithValues.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}