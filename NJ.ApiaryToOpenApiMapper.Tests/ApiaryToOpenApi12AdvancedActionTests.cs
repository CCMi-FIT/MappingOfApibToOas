using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi12AdvancedActionTests
  {
    [Fact]
    public void ApiaryToOpenApi12AdvancedActionTest()
    {
      var listAllTasksResponse = new ResponseSection(200, "application/json")
      {
        BodySection = new BodySection(@"[
            {
                ""id"": 123,
                ""name"": ""Exercise in gym"",
                ""done"": false,
                ""type"": ""task""
            },
            {
                ""id"": 124,
                ""name"": ""Shop for groceries"",
                ""done"": true,
                ""type"": ""task""
            }
        ]")
      };


      var listAllTasksAction = new ActionSection("List All Tasks", null, HttpRequestMethod.Get)
      {
        ResponseSections = new List<ResponseSection> { listAllTasksResponse }
      };

      var retrieveTaskAction = new ActionSection("Retrieve Task", "This is a state transition to another resource.", HttpRequestMethod.Get, new UriTemplate("/task/{id}"))
      {
        UriParametersSection = new UriParametersSection()
        {
          Parameters = new List<UriParameter> { new UriParameter("id", true, "string", "") }
        },
        ResponseSections = new List<ResponseSection>
        {
          new ResponseSection(200, "application/json")
          {
            BodySection = new BodySection(@"{
                ""id"": 123,
                ""name"": ""Go to gym"",
           ""done"": false,
                ""type"": ""task""
            }")
          }
        }
      };

      var deleteTaskResponse = new ResponseSection(204);
      var deleteTaskAction =
        new ActionSection("Delete Task", null, HttpRequestMethod.Delete, new UriTemplate("/task/{id}"))
        {
          UriParametersSection = new UriParametersSection(new List<UriParameter> { new UriParameter("id", true, "string", "") }),
          ResponseSections = new List<ResponseSection> { deleteTaskResponse }
        };

      var tasksResource = new ResourceSection("Tasks", new UriTemplate("/tasks/tasks{?status,priority}"))
      {
        ParametersSection = new UriParametersSection
        {
          Parameters = new List<UriParameter>
          {
            new UriParameter("status", true, "string", ""),
            new UriParameter("priority", true, "number", "")
          }
        },
        ActionSections = new List<ActionSection>
        {
          listAllTasksAction,
          retrieveTaskAction,
          deleteTaskAction
        }
      };
      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceSections = new[] { tasksResource };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Advanced Action API",
        Description = @"A resource action is – in fact – a state transition. This API example
demonstrates an action - state transition - to another resource.

## API Blueprint

+ [Previous: Resource Model](11.%20Resource%20Model.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/12.%20Advanced%20Action.md)

+ [Next: Named Endpoints](13.%20Named%20Endpoints.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/12. Advanced Action.json");
    }
  }
}
