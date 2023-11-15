using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi13NamedEndpointsTests
  {
    [Fact]
    public void ApiaryToOpenApi13NamedEndpointsTest()
    {
      var createMessageRequest = new RequestSection
      {
        MediaType = "application/json",
        BodySection = new BodySection("{ \"message\": \"Hello World!\" }")
      };
      var createMessageResponse = new ResponseSection(201)
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { ["Location"] = "/messages/1337" })
      };
      var createMessageAction =
        new ActionSection("Create message", "Start out by creating a message for the world to see.", HttpRequestMethod.Post, new UriTemplate("/messages"))
        {
          RequestSections = new List<RequestSection> { createMessageRequest },
          ResponseSections = new List<ResponseSection> { createMessageResponse }
        };
      var createMessageResource = new ResourceSection("Create message", new UriTemplate("/messages"))
      {
        HttpRequestMethod = HttpRequestMethod.Post,
        ActionSections = new List<ActionSection> { createMessageAction }
      };

      var createNewTaskRequest = new RequestSection
      {
        MediaType = "application/json",
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

      var createNewTaskAction = new ActionSection("Create a new task", "Now create a task that you need to do at a later date.", HttpRequestMethod.Post, new UriTemplate("/tasks"))
      {
        RequestSections = new List<RequestSection> { createNewTaskRequest },
        ResponseSections = new List<ResponseSection> { createNewTaskResponse }
      };

      var createNewTaskResource = new ResourceSection("Create a new task", new UriTemplate("/tasks"))
      {
        HttpRequestMethod = HttpRequestMethod.Post,
        ActionSections = new List<ActionSection> { createNewTaskAction }
      };
      var quickStartGroup = new ResourceGroupSection("Quick start") { ResourceSections = new List<ResourceSection> { createMessageResource, createNewTaskResource } };

      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceGroupSections = new[] { quickStartGroup };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Named Endpoints API",
        Description = @"This API example demonstrates how to define a standalone endpoint with an identifier.

## API Blueprint

+ [Previous: Advanced Action](12.%20Advanced%20Action.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/13.%20Named%20Endpoints.md)

+ [Next: JSON Schema](14.%20JSON%20Schema.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/13. Named Endpoints.json");
    }
  }
}
