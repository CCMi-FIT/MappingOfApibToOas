using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;
using System.Text.RegularExpressions;

namespace NJ.ApibToOasMapper
{
  public static class ResourceMapper
  {
    private const string ResourceQueryPathParameterPattern = @"\{\?[^\}]+\}";

    public static PathsObject MapResources(Apib apb, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var result = new PathsObject
      {
        PathItems = ResourceMapper.MapResourcesToDictionary(apb, apiNamedTypes)
      };
      return result;
    }

    public static IReadOnlyDictionary<string, PathItemObject> MapResourcesToDictionary(Apib apb, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var resourcesWithExistingParentSections = apb.ResourceGroupSections?.SelectMany(g => g.ResourceSections.Select(r => new ResourceInfo(r, g))) ?? Enumerable.Empty<ResourceInfo>();
      var resourcesWithNullParentSections = apb.ResourceSections?.Select(r => new ResourceInfo(r)) ?? Enumerable.Empty<ResourceInfo>();
      var resourceInfos = resourcesWithExistingParentSections.Concat(resourcesWithNullParentSections);
      var operationInfos = resourceInfos.SelectMany(ri => MapResourceSectionToOperationInfo(ri, apiNamedTypes));
      var operationInfosGroupedByPath = operationInfos.GroupBy(o => o.Path);
      var result = operationInfosGroupedByPath.ToDictionary(g => g.Key, MapOperationInfosToPathItemObject);
      return result;
    }

    private static PathItemObject MapOperationInfosToPathItemObject(IEnumerable<OperationInfo> samePathOperationInfos)
    {
      OperationObject get = default;
      OperationObject post = default;
      OperationObject put = default;
      OperationObject delete = default;
      OperationObject head = default;
      OperationObject options = default;
      OperationObject patch = default;
      OperationObject trace = default;

      get = samePathOperationInfos.FirstOrDefault(o => o.HttpRequestMethod == HttpRequestMethod.Get).OperationObject;

      foreach (var operationInfo in samePathOperationInfos)
      {
        switch (operationInfo.HttpRequestMethod)
        {
          case HttpRequestMethod.Get:
            get = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Post:
            post = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Put:
            put = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Delete:
            delete = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Head:
            head = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Options:
            options = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Patch:
            patch = operationInfo.OperationObject;
            break;
          case HttpRequestMethod.Trace:
            trace = operationInfo.OperationObject;
            break;
          default:
            throw new NotSupportedException();
        }
      }

      var result = new PathItemObject
      {
        Get = get,
        Post = post,
        Put = put,
        Delete = delete,
        Head = head,
        Options = options,
        Patch = patch,
        Trace = trace
      };
      return result;
    }

    private static IEnumerable<OperationInfo> MapResourceSectionToOperationInfo(ResourceInfo resourceInfo, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var resourceSection = resourceInfo.ResourceSection;
      if (resourceSection?.UriTemplate?.Path is null)
        return Array.Empty<OperationInfo>();
      var result = resourceSection.ActionSections.Select(a =>  MapActionSectionToOperationInfo(a, resourceInfo, apiNamedTypes));
      return result;
    }

    private static OperationInfo MapActionSectionToOperationInfo(ActionSection actionSection, ResourceInfo resourceInfo, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var resourceSection = resourceInfo.ResourceSection;
      var resourceGroupSection = resourceInfo.ResourceGroupSection;
      var actionCoercedPath = GetActionUrlPath(actionSection.UriTemplate?.Path, resourceSection?.UriTemplate?.Path);
      var operationObject = ActionSectionMapper.Map(actionSection, resourceSection, resourceGroupSection, apiNamedTypes);
      var result = new OperationInfo(operationObject, actionCoercedPath, actionSection.HttpRequestMethod);
      return result;
    }

    private static string GetActionUrlPath(string actionPath, string resourcePath)
    {
      string result;
      if (actionPath is null)
        result = Regex.Replace(resourcePath, ResourceQueryPathParameterPattern, "");
      else
        result = Regex.Replace(actionPath, ResourceQueryPathParameterPattern, "");
      return result;
    }
  }
}