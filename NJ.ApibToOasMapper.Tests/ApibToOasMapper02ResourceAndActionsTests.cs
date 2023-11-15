using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper02ResourceAndActionsTests
  {
    [Fact]
    public void ApibToOasMapper02ResourceAndActionsTest()
    {
      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Resource and Actions API",
        Description =
@"This API example demonstrates how to define a resource with multiple actions.

## API Blueprint

+ [Previous: The Simplest API](01.%20Simplest%20API.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/02.%20Resource%20and%20Actions.md)

+ [Next: Named Resource and Actions](03.%20Named%20Resource%20and%20Actions.md)"
      };
      var getResponse = new ResponseSection(200, "text/plain")
      {
        BodySection = new BodySection("Hello World!\r\n")
      };
      var getAction = new ActionSection
      {
        HttpRequestMethod = HttpRequestMethod.Get,
        Description =
          "Here we define an action using the `GET` [HTTP request method](http://www.w3schools.com/tags/ref_httpmethods.asp) for our resource `/message`.\r\n\r\nAs with every good action it should return a\r\n[response](http://www.w3.org/TR/di-gloss/#def-http-response). A response always\r\nbears a status code. Code 200 is great as it means all is green. Responding\r\nwith some data can be a great idea as well so let's add a plain text message to\r\nour response.",
        ResponseSections = new[] { getResponse }
      };
      var putRequest = new RequestSection
      {
        MediaType = "text/plain",
        BodySection = new BodySection("All your base are belong to us.\n")
      };
      var putResponse = new ResponseSection
      {
        HttpStatusCode = 204
      };
      var putAction = new ActionSection
      {
        HttpRequestMethod = HttpRequestMethod.Put,
        Description = "OK, let's add another action. This time to put new data to our resource\r\n(essentially an update action). We will need to send something in a\r\n[request](http://www.w3.org/TR/di-gloss/#def-http-request) and then send a\r\nresponse back confirming the posting was a success (_HTTP Status Code 204 ~\r\nResource updated successfully, no content is returned_).",
        RequestSections = new[] { putRequest },
        ResponseSections = new[] { putResponse }
      };
      var messageResource = new ResourceSection
      {
        HttpRequestMethod = HttpRequestMethod.Get,
        UriTemplate = new UriTemplate("/message"),
        Description = "This is our [resource](http://www.w3.org/TR/di-gloss/#def-resource). It is\r\ndefined by its\r\n[URI](http://www.w3.org/TR/di-gloss/#def-uniform-resource-identifier) or, more\r\nprecisely, by its [URI Template](http://tools.ietf.org/html/rfc6570).\r\n\r\nThis resource has no actions specified but we will fix that soon.",
        ActionSections = new[] { getAction, putAction }
      };
      apib.ResourceSections = new List<ResourceSection>
      {
        messageResource
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/02. Resource and Actions.json");
    }
  }
}
