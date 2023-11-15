using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi03NamedResourceAndActionsTests
  {
    [Fact]
    public void ApiaryToOpenApi03NamesResourceAndActionsTest()
    {
      var retrieveResponse = new ResponseSection
      {
        HttpStatusCode = 200,
        MediaType = "text/plain",
        BodySection = new BodySection { Content = "Hello World!\n" }
      };
      var retrieveAction = new ActionSection
      {
        Identifier = "Retrieve a Message",
        HttpRequestMethod = HttpRequestMethod.Get,
        Description = "Now this is informative! No extra explanation needed here. This action clearly\r\nretrieves the message.",
        ResponseSections = new[] { retrieveResponse }
      };

      var updateRequest = new RequestSection
      {
        MediaType = "text/plain",
        BodySection = new BodySection { Content = "All your base are belong to us.\n" }
      };
      var updateResponse = new ResponseSection
      {
        HttpStatusCode = 204
      };
      var updateAction = new ActionSection
      {
        Identifier = "Update a Message",
        HttpRequestMethod = HttpRequestMethod.Put,
        Description = "`Update a message` - nice and simple naming is the best way to go.",
        RequestSections = new[] { updateRequest },
        ResponseSections = new[] { updateResponse }
      };

      var resource = new ResourceSection
      {
        Identifier = "My Message",
        UriTemplate = new UriTemplate("/message"),
        Description = @"OK, `My Message` probably isn't the best name for our resource but it will do for now. Note the URI `/message` is enclosed in square brackets.",
        ActionSections = new[] { retrieveAction, updateAction }
      };

      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceSections = new[] { resource };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Named Resource and Actions API",
        Description =
          @"This API example demonstrates how to name a resource and its actions, to give
the reader a better idea about what the resource is used for.

## API Blueprint

+ [Previous: Resource and Actions](02.%20Resource%20and%20Actions.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/03.%20Named%20Resource%20and%20Actions.md)

+ [Next: Grouping Resources](04.%20Grouping%20Resources.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/03. Named Resource and Actions.json");
    }
  }
}
