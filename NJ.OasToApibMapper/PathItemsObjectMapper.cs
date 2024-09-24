using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper
{
  public static class PathItemsObjectMapper
  {
    public static IEnumerable<ResourceSection> Map(PathsObject paths)
    {
      foreach (var pathItem in paths.PathItems)
      {
        var pathString = pathItem.Key;
        var pathItemObject = pathItem.Value;
        var uriTemplate = new UriTemplate(pathString);
        var actionSections = MapPathItemObject(pathItemObject, uriTemplate).ToList();
        var resourceSection = new ResourceSection
        {
          UriTemplate = uriTemplate,
          ActionSections = actionSections,
          Description = pathItemObject.Description
        };
        yield return resourceSection;
      }
    }

    private static IReadOnlyCollection<ActionSection> MapPathItemObject(PathItemObject pathItemObject, UriTemplate uriTemplate)
    {
      var httpMethodsWithOperationObjects = new (string HttpMethod, OperationObject OperationObject)[]
      {
        ("Get", pathItemObject.Get),
        ("Post", pathItemObject.Post),
        ("Put", pathItemObject.Put),
        ("Patch", pathItemObject.Patch),
        ("Delete", pathItemObject.Delete),
        ("Options", pathItemObject.Options),
        ("Head", pathItemObject.Head),
        ("Trace", pathItemObject.Trace)
      }.Where(i => i.OperationObject is not null);

      var result = httpMethodsWithOperationObjects.Select(i => OperationObjectMapper.Map(i.HttpMethod, i.OperationObject)).ToList();
      return result;
    }
  }
}