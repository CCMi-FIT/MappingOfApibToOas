using NJ.ApibModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper01SimplestApiTests
  {
    [Fact]
    public void ApibToOasMapper01SimplestApiTest()
    {
      var metadataSection = new MetadataSection(("FORMAT", "1A"));
      var apiNameAndOverviewDescription = @"This is one of the simplest APIs written in the **API Blueprint**. One plain
resource combined with a method and that's it! We will explain what is going on
in the next installment - 
[Resource and Actions](02.%20Resource%20and%20Actions.md).

**Note:** As we progress through the examples, do not also forget to view the
[Raw](https://raw.github.com/apiaryio/api-blueprint/master/examples/01.%20Simplest%20API.md)
code to see what is really going on in the API Blueprint, as opposed to just
seeing the output of the Github Markdown parser.

Also please keep in mind that every single example in this course is a **real
API Blueprint** and as such you can **parse** it with the 
[API Blueprint parser](https://github.com/apiaryio/drafter) or one of its
[bindings](https://github.com/apiaryio/drafter#bindings).

## API Blueprint
+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/01.%20Simplest%20API.md)
+ [Next: Resource and Actions](02.%20Resource%20and%20Actions.md)";

      var actionSections = new[]
          {
            new ActionSection(default, default(string?), HttpRequestMethod.Get, new[]
              {
                new ResponseSection(200, "text/plain")
                {
                  BodySection = new BodySection("Hello World!\r\n")
                }
              })
          };
      var resourceSections = new[]
      {
        new ResourceSection(default, "Hello World!", new UriTemplate("/message"), HttpRequestMethod.Get, actionSections)
      };
      var apiNameAndOverviewSection = new ApiNameAndOverviewSection("The Simplest API", apiNameAndOverviewDescription);

      var apib = new Apib
      {
        MetadataSection = metadataSection,
        ApiNameAndOverviewSection = apiNameAndOverviewSection,
        ResourceSections = resourceSections
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/01. Simplest API.json");
    }
  }
}