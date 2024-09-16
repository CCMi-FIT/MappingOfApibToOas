namespace NJ.OasModel.AdditionalDomainObjects
{
  public class ComponentsObjectKey
  {
    private const string _regexPattern = @"^[a-zA-Z0-9\.\-_]+$";

    public string Key { get; }

    public ComponentsObjectKey(string key)
    {
      if (!System.Text.RegularExpressions.Regex.IsMatch(key, _regexPattern))
        throw new ArgumentException($"Unsupported key '{key}'");
      Key = key;
    }
  }
}
