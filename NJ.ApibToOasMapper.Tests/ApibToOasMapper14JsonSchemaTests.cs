using NJ.ApibModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper14JsonSchemaTests
  {
    [Fact]
    public void ApibToOasMapper14JsonSchemaTest()
    {
      var getNoteResponse = new ResponseSection(200, "application/json")
      {
        BodySection = new BodySection(@"{
            ""id"": ""abc123"",
            ""title"": ""This is a note"",
            ""content"": ""This is the note content."",
            ""tags"": [
                ""todo"",
                ""home""
            ]
        }"),
        SchemaSection = new SchemaSection
        {
          Content = @"{
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
      var getNoteAction = new ActionSection("Get a note", "Gets a single note by its unique identifier.", HttpRequestMethod.Get, new[] { getNoteResponse })
      {
      };

      var updateNoteRequest = new RequestSection(default, "application/json")
      {
        BodySection = new BodySection(@"{
            ""title"": ""This is another note"",
            ""tags"": [
                ""todo"",
                ""work""
            ]
        }"),
        SchemaSection = new SchemaSection
        {
          Content = @"{
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
      var updateNoteAction = new ActionSection("Update a note", "Modify a note's data using its unique identifier. You can edit the `title`,\r\n`content`, and `tags`.", HttpRequestMethod.Patch, new[] { updateNoteRequest }, new[] { updateNoteResponse });
      var notesResource = new ResourceSection("Notes", default(Description?), new UriTemplate("/notes/{id}"), new[] { getNoteAction, updateNoteAction })
      {
        ParametersSection = new UriParametersSection(new[] { new UriParameter("id", true, "string", "Unique identifier for a note") { ExampleValue = "abc123" } }),
      };

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection(("FORMAT", "1A"));
      apib.ResourceSections = new[] { notesResource };
      var apiNameAndOverviewDescription = @"Every request and response can have a schema. Below you will find examples
using [JSON Schema](http://json-schema.org/) to describe the format of request
and response body content.

## API Blueprint

+ [Previous: Named Endpoints](13.%20Named%20Endpoints.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/14.%20JSON%20Schema.md)

+ [Next: Advanced JSON Schema](15.%20Advanced%20JSON%20Schema.md)";
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection("JSON Schema", apiNameAndOverviewDescription);

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/14. JSON Schema - 02.json");
    }
  }
}
