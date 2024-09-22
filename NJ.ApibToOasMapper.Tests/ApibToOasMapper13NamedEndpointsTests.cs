using NJ.ApibModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper13NamedEndpointsTests
  {
    [Fact]
    public void ApibToOasMapper13NamedEndpointsTest()
    {
      var createMessageRequest = new RequestSection(default, "application/json")
      {
        BodySection = new BodySection("{ \"message\": \"Hello World!\" }")
      };
      var createMessageResponse = new ResponseSection(201)
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { ["Location"] = "/messages/1337" })
      };
      var createMessageAction =
        new ActionSection("Create message", "Start out by creating a message for the world to see.", HttpRequestMethod.Post, new[] { createMessageRequest }, new[] { createMessageResponse })
        {
          UriTemplate = new UriTemplate("/messages")
        };
      var createMessageResource = new ResourceSection("Create message", default(Description?), new UriTemplate("/messages"), new[] { createMessageAction });

      var createNewTaskRequest = new RequestSection("application/json")
      {
        BodySection = new BodySection(@"{
            ""name"": ""Exercise in gym"",
            ""done"": false,
            ""type"": ""task""
        }")
      };
      var createNewTaskResponse = new ResponseSection(201)
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { ["Location"] = "/tasks/1992" })
      };

      var createNewTaskAction = new ActionSection("Create a new task", "Now create a task that you need to do at a later date.", HttpRequestMethod.Post, new[] { createNewTaskRequest }, new[] { createNewTaskResponse })
      {
        UriTemplate = new UriTemplate("/tasks")
      };

      var createNewTaskResource = new ResourceSection("Create a new task", default(Description?), new UriTemplate("/tasks"), HttpRequestMethod.Post, new[] { createNewTaskAction });
      var quickStartGroup = new ResourceGroupSection("Quick start", default(Description?), new[] { createMessageResource, createNewTaskResource });

      var apiNameAndOverviewDescription = @"This API example demonstrates how to define a standalone endpoint with an identifier.

## API Blueprint

+ [Previous: Advanced Action](12.%20Advanced%20Action.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/13.%20Named%20Endpoints.md)

+ [Next: JSON Schema](14.%20JSON%20Schema.md)";
      var apib = new Apib {
        MetadataSection = new MetadataSection(("FORMAT", "1A")),
        ResourceGroupSections = new[] { quickStartGroup },
        ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Named Endpoints API", apiNameAndOverviewDescription)
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/13. Named Endpoints.json");
    }
  }
}
