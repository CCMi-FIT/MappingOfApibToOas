using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi15AdvancedJsonSchemaTests
  {
    [Fact]
    public void ApiaryToOpenApi15AdvancedJsonSchemaTest()
    {
      var getNoteResponse = new ResponseSection(200, "application/json")
      {
        AttributesSection = new AttributesSection
        {
          Attributes = new List<AttributeSection>
          {
            new AttributeSection("id", null, true, "string", "abc123"),
            new AttributeSection("title", null, true, "string", "This is a note"),
            new AttributeSection("content", null, true, "string", "This is the note content."),
            new AttributeSection("tags", null, true, "array[string]", new[] {"todo", "home"}),
          }
        }
      };
      var getNoteAction = new ActionSection("Get a note", "Gets a single note by its unique identifier.", HttpRequestMethod.Get)
      {
        ResponseSections = new List<ResponseSection> { getNoteResponse }
      };

      var updateNoteRequest = new RequestSection
      {
        MediaType = "application/json",
        AttributesSection = new AttributesSection
        {
          Attributes = new List<AttributeSection>
          {
            new AttributeSection("title", null, true, "string", "This is another note"),
            new AttributeSection("content", null, true),
            new AttributeSection("tags", null, true, "array[string]", new[] { "todo", "work" }),
          }
        },
        SchemaSection = new SchemaSection
        {
          Schema = @"{
              ""type"": ""object"",
              ""description"": ""This is a custom schema!"",
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
        Name = "Advanced JSON Schema",
        Description = @"The JSON body and JSON Schema for a request or response can be generated from
the attributes section MSON data structure. The generated schema can also be
overridden by providing an explicit schema, as you can see in the examples
below.

## API Blueprint

+ [Previous: JSON Schema](14.%20JSON%20Schema.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/15.%20Advanced%20JSON%20Schema.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/15. Advanced JSON Schema - 02.json");
    }
  }
}
