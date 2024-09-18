using System.Collections;

namespace NJ.ApibModel;

public class HeadersSection : PayloadSection, IEnumerable<KeyValuePair<string, object>>
{
  public override string? Keyword { get; } = "Headers";

  public IReadOnlyDictionary<string, object> KeysWithValues { get; }

  public HeadersSection(IEnumerable<KeyValuePair<string, object>>? keysWithValues = default)
  {
    KeysWithValues = keysWithValues?.ToDictionary(kv => kv.Key, kv => kv.Value) ?? new Dictionary<string, object>();
  }

  public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => KeysWithValues.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}