using NJ.ApibModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper12AdvancedActionTests
  {
    [Fact]
    public void ApibToOasMapper12AdvancedActionTest()
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


      var listAllTasksAction = new ActionSection("List All Tasks", null, HttpRequestMethod.Get, new[] { listAllTasksResponse });
      {
      };

      var responseSections = new[] {
          new ResponseSection(200, "application/json")
          {
            BodySection = new BodySection(@"{
                ""id"": 123,
                ""name"": ""Go to gym"",
           ""done"": false,
                ""type"": ""task""
            }")
          }
        };
      var retrieveTaskAction = new ActionSection("Retrieve Task", "This is a state transition to another resource.", HttpRequestMethod.Get, responseSections)
      {
        UriTemplate = new UriTemplate("/task/{id}"),
        ParametersSection = new UriParametersSection(new[] { new UriParameter("id", true, "string", "") })
        {
          Parameters = new List<UriParameter> { new UriParameter("id", true, "string", "") }
        }
      };

      var deleteTaskResponse = new ResponseSection(204);
      var deleteTaskAction =
        new ActionSection("Delete Task", null, HttpRequestMethod.Delete, new[] { deleteTaskResponse })
        {
          UriTemplate = new UriTemplate("/task/{id}"),
          ParametersSection = new UriParametersSection(new List<UriParameter> { new UriParameter("id", true, "string", "") }),
        };

      var tasksResource = new ResourceSection("Tasks", default(string?), new UriTemplate("/tasks/tasks{?status,priority}"), new[] { listAllTasksAction, retrieveTaskAction, deleteTaskAction })
      {
        ParametersSection = new UriParametersSection(new[] {
            new UriParameter("status", true, "string", ""),
            new UriParameter("priority", true, "number", "")
          })
      };
      var apiNameAndOverviewDescription = @"A resource action is – in fact – a state transition. This API example
demonstrates an action - state transition - to another resource.

## API Blueprint

+ [Previous: Resource Model](11.%20Resource%20Model.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/12.%20Advanced%20Action.md)

+ [Next: Named Endpoints](13.%20Named%20Endpoints.md)";
      var apib = new Apib {
        MetadataSection = new MetadataSection (("FORMAT", "1A")),
        ResourceSections = new[] { tasksResource },
        ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Advanced Action API", apiNameAndOverviewDescription)
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/12. Advanced Action - 02.json");
    }
  }
}
