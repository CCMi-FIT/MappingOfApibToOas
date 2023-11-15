using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper01SimplestApiTests
  {
    [Fact]
    public void ApibToOasMapper01SimplestApiTest()
    {
      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "The Simplest API",
        Description =
          @"This is one of the simplest APIs written in the **API Blueprint**. One plain
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
+ [Next: Resource and Actions](02.%20Resource%20and%20Actions.md)"
      };
      apib.ResourceSections = new[]
      {
        new ResourceSection
        {
          HttpRequestMethod = HttpRequestMethod.Get,
          UriTemplate = new UriTemplate("/message"),
          Description = "Hello World!",
          ActionSections = new []
          {
            new ActionSection
            {
              ResponseSections = new[]
              {
                new ResponseSection(200, "text/plain")
                {
                  BodySection = new BodySection("Hello World!\r\n")
                }
              },
              HttpRequestMethod = HttpRequestMethod.Get
            }
          }
        }
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/01. Simplest API.json");
    }
  }
}