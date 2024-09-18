namespace NJ.ApibModel.AdditionalDomainObjects;

public class UriTemplate
{
    public string Path { get; }

    public UriTemplate(string path)
    {
        Path = path;
    }
}