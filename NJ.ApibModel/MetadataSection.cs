using System.Collections;

namespace NJ.ApibModel;

public class MetadataSection : IEnumerable<KeyValuePair<string, string>>
{
  public IReadOnlyDictionary<string, string> KeysWithValues { get; set; }

  public MetadataSection(IEnumerable<KeyValuePair<string, string>>? keysWithValues = default)
  {
    KeysWithValues = keysWithValues?.ToDictionary(kv => kv.Key, kv => kv.Value) ?? new Dictionary<string, string>();
  }

  public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => KeysWithValues.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}