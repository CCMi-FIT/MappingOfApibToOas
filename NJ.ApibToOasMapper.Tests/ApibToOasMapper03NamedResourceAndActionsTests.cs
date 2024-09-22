using NJ.ApibModel;
using NJ.ApibModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
    public class ApibToOasMapper03NamedResourceAndActionsTests
  {
    [Fact]
    public void ApibToOasMapper03NamesResourceAndActionsTest()
    {
      var retrieveResponse = new ResponseSection(200, "text/plain")
      {
        BodySection = new BodySection("Hello World!\n")
      };
      var retrieveActionDescription = "Now this is informative! No extra explanation needed here. This action clearly\r\nretrieves the message.";
      var retrieveAction = new ActionSection("Retrieve a Message", retrieveActionDescription, HttpRequestMethod.Get, new[] { retrieveResponse });

      var updateRequest = new RequestSection(default, "text/plain")
      {
        BodySection = new BodySection("All your base are belong to us.\n")
      };
      var updateResponse = new ResponseSection(204);
      var updateActionDescription = "`Update a message` - nice and simple naming is the best way to go.";
      var updateAction = new ActionSection("Update a Message", updateActionDescription, HttpRequestMethod.Put, new[] { updateRequest }, new[] { updateResponse });

      var resourceDescription = @"OK, `My Message` probably isn't the best name for our resource but it will do for now. Note the URI `/message` is enclosed in square brackets.";
      var resource = new ResourceSection("My Message", resourceDescription, new UriTemplate("/message"), new[] { retrieveAction, updateAction });

      var apiNameAndOverviewDescription = @"This API example demonstrates how to name a resource and its actions, to give
the reader a better idea about what the resource is used for.

## API Blueprint

+ [Previous: Resource and Actions](02.%20Resource%20and%20Actions.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/03.%20Named%20Resource%20and%20Actions.md)

+ [Next: Grouping Resources](04.%20Grouping%20Resources.md)";
      var apib = new Apib
      {
        MetadataSection = new MetadataSection(("FORMAT", "1A")),
        ResourceSections = new[] { resource },
        ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Named Resource and Actions API", apiNameAndOverviewDescription)
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/03. Named Resource and Actions.json");
    }
  }
}
