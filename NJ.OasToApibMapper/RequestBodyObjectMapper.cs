using NJ.ApibModel;
using NJ.Common.Extensions;
using NJ.OasModel;

namespace NJ.OasToApibMapper
{
  public static class RequestBodyObjectMapper
  {
    public static RequestSection Map(RequestBodyObject requestBodyObject)
    {
      var result = new RequestSection();

      var content = requestBodyObject.Content;
      if (!content.IsNullOrEmpty())
      {
        var contentItem = content.Single();
        var mediaType = contentItem.Key;
        var example = contentItem.Value.Example;
        result.BodySection = new BodySection(example);
      }

      return result;
    }
  }
}
