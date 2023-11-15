using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi14JsonSchemaTests
  {
    [Fact]
    public void ApiaryToOpenApi14JsonSchemaTest()
    {
      var getNoteResponse = new ResponseSection(200, "application/json")
      {
        BodySection = new BodySection
        {
          Content = @"{
                ""id"": ""abc123"",
                ""title"": ""This is a note"",
                ""content"": ""This is the note content."",
                ""tags"": [
                    ""todo"",
                    ""home""
                ]
            }"
        },
        SchemaSection = new SchemaSection
        {
          Schema = @"{
                ""type"": ""object"",
                ""properties"": {
                    ""id"": {
                        ""type"": ""string""
                    },
                    ""title"": {
                        ""type"": ""string""
                    },
                    ""content"": {
                        ""type"": ""string""
                    },
                    ""tags"": {
                        ""type"": ""array"",
                        ""items"": {
                            ""type"": ""string""
                        }
                    }
                }
            }"
        }
      };
      var getNoteAction = new ActionSection("Get a note", "Gets a single note by its unique identifier.", HttpRequestMethod.Get)
      {
        ResponseSections = new List<ResponseSection> { getNoteResponse }
      };

      var updateNoteRequest = new RequestSection
      {
        MediaType = "application/json",
        BodySection = new BodySection(@"{
            ""title"": ""This is another note"",
            ""tags"": [
                ""todo"",
                ""work""
            ]
        }"),
        SchemaSection = new SchemaSection
        {
          Schema = @"{
              ""type"": ""object"",
              ""properties"": {
                  ""title"": {
                      ""type"": ""string""
                  },
                  ""content"": {
                      ""type"": ""string""
                  },
                  ""tags"": {
                      ""type"": ""array"",
                      ""items"": {
                          ""type"": ""string""
                      }
                  }
              },
              ""additionalProperties"": false
          }"
        }
      };
      var updateNoteResponse = new ResponseSection(204);
      var updateNoteAction = new ActionSection("Update a note", "Modify a note's data using its unique identifier. You can edit the `title`,\r\n`content`, and `tags`.", HttpRequestMethod.Patch)
      {
        RequestSections = new List<RequestSection> { updateNoteRequest },
        ResponseSections = new[] { updateNoteResponse }
      };
      var notesResource = new ResourceSection("Notes", new UriTemplate("/notes/{id}"))
      {
        ParametersSection = new UriParametersSection
        {
          Parameters = new List<UriParameter> { new UriParameter("id", true, "string", "Unique identifier for a note") { ExampleValue = "abc123" } }
        },
        ActionSections = new List<ActionSection> { getNoteAction, updateNoteAction }
      };

      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceSections = new[] { notesResource };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "JSON Schema",
        Description = @"Every request and response can have a schema. Below you will find examples
using [JSON Schema](http://json-schema.org/) to describe the format of request
and response body content.

## API Blueprint

+ [Previous: Named Endpoints](13.%20Named%20Endpoints.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/14.%20JSON%20Schema.md)

+ [Next: Advanced JSON Schema](15.%20Advanced%20JSON%20Schema.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/14. JSON Schema - 02.json");
    }
  }
}
