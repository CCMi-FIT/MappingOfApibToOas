using NJ.ApibModel;
using NJ.ApibModel.AdditionalDomainObjects;
using NJ.SharedModel;
using System.ComponentModel.DataAnnotations;

namespace NJ.ApibToOasMapper.Tests
{
    public class ApibToOasMapper15AdvancedJsonSchemaTests
  {
    [Fact]
    public void ApibToOasMapper15AdvancedJsonSchemaTest()
    {
      var getNoteResponse = new ResponseSection(200, "application/json")
      {
        AttributesSection = new AttributesSection(new[] {
            new AttributeSection("id", "string", null, true, "abc123"),
            new AttributeSection("title", "string", null, true, "This is a note"),
            new AttributeSection("content", "string", null, true, "This is the note content."),
            new AttributeSection("tags", "array[string]", null, true, new[] {"todo", "home"}),
          }
        )
      };
      var getNoteAction = new ActionSection("Get a note", "Gets a single note by its unique identifier.", HttpRequestMethod.Get, new[] { getNoteResponse });

      var updateNoteRequest = new RequestSection(default, "application/json")
      {
        AttributesSection = new AttributesSection(new[] {
            new AttributeSection("title", "string", null, true, "This is another note"),
            new AttributeSection("content", "object", description: null, required: true),
            new AttributeSection("tags", "array[string]", null, true, new[] { "todo", "work" }),
          }
        ),
        SchemaSection = new SchemaSection
        {
          Content = @"{
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
      var updateNoteAction = new ActionSection("Update a note", "Modify a note's data using its unique identifier. You can edit the `title`,\r\n`content`, and `tags`.", HttpRequestMethod.Patch, new[] { updateNoteRequest }, new[] { updateNoteResponse });
      var notesResource = new ResourceSection("Notes", default(Description), new UriTemplate("/notes/{id}"), new[] { getNoteAction, updateNoteAction })
      {
        ParametersSection = new UriParametersSection(new[] { new UriParameter("id", true, "string", "Unique identifier for a note") { ExampleValue = "abc123" } })
      };

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection(("FORMAT", "1A"));
      apib.ResourceSections = new[] { notesResource };
      var apiNameAndOverviewDescription = @"The JSON body and JSON Schema for a request or response can be generated from
the attributes section MSON data structure. The generated schema can also be
overridden by providing an explicit schema, as you can see in the examples
below.

## API Blueprint

+ [Previous: JSON Schema](14.%20JSON%20Schema.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/15.%20Advanced%20JSON%20Schema.md)";
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Advanced JSON Schema", apiNameAndOverviewDescription);

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/15. Advanced JSON Schema - 02.json");
    }
  }
}
