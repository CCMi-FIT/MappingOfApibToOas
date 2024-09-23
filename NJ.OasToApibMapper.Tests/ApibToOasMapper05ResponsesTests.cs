using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper05ResponsesTests
  {
    [Fact]
    public void ApibToOasMapper05ResponsesTest()
    {
      var retrieveTextPlainResponse = new ResponseSection
      {
        HttpStatusCode = 200,
        MediaType = "text/plain",
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection { Content = "Hello World!\n" }
      };
      var retrieveApplicationJsonResponse = new ResponseSection
      {
        HttpStatusCode = 200,
        MediaType = "application/json",
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection { Content = "{ \"message\": \"Hello World!\" }" }
      };
      var retrieveAction = new ActionSection
      {
        Identifier = "Retrieve a Message",
        HttpRequestMethod = HttpRequestMethod.Get,
        ResponseSections = new[] { retrieveTextPlainResponse, retrieveApplicationJsonResponse },
        Description = @"This action has **two** responses defined: One returning plain text and the
other a JSON representation of our resource. Both have the same HTTP status
code. Also both responses bear additional information in the form of a custom
HTTP header. Note that both responses have set the `Content-Type` HTTP header
just by specifying `(text/plain)` or `(application/json)` in their respective
signatures."
      };

      var updateRequest = new RequestSection
      {
        MediaType = "text/plain",
        BodySection = new BodySection { Content = "All your base are belong to us.\n" }
      };
      var updateResponse = new ResponseSection
      {
        HttpStatusCode = 204
      };
      var updateAction = new ActionSection
      {
        Identifier = "Update a Message",
        HttpRequestMethod = HttpRequestMethod.Put,
        RequestSections = new[] { updateRequest },
        ResponseSections = new[] { updateResponse }
      };

      var resource = new ResourceSection
      {
        Identifier = "My Message",
        UriTemplate = new UriTemplate("/message"),
        ActionSections = new[] { retrieveAction, updateAction }
      };

      var messagesResourceGroup = new ResourceGroupSection("Messages")
      {
        Description = @"Group of all messages-related resources.",
        ResourceSections = new[] { resource }
      };

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceGroupSections = new[] { messagesResourceGroup };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Responses API",
        Description = @"In this API example we will discuss what information a response can bear and
how to define multiple responses. Technically a response is represented by a
payload that is sent back in response to a request.

## API Blueprint

+ [Previous: Grouping Resources](04.%20Grouping%20Resources.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/05.%20Responses.md)

+ [Next: Requests](06.%20Requests.md)"
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/05. Responses.json");
    }
  }
}
