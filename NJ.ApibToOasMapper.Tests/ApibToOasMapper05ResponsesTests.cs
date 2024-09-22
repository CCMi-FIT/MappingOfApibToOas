using NJ.ApibModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper05ResponsesTests
  {
    [Fact]
    public void ApibToOasMapper05ResponsesTest()
    {
      var retrieveTextPlainResponse = new ResponseSection(200, "text/plain")
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection("Hello World!\n")
      };
      var retrieveApplicationJsonResponse = new ResponseSection(200, "application/json")
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection("{ \"message\": \"Hello World!\" }")
      };
      var retrieveActionDescription = @"This action has **two** responses defined: One returning plain text and the
other a JSON representation of our resource. Both have the same HTTP status
code. Also both responses bear additional information in the form of a custom
HTTP header. Note that both responses have set the `Content-Type` HTTP header
just by specifying `(text/plain)` or `(application/json)` in their respective
signatures.";
      var retrieveAction = new ActionSection("Retrieve a Message", retrieveActionDescription, HttpRequestMethod.Get, new[] { retrieveTextPlainResponse, retrieveApplicationJsonResponse });

      var updateRequest = new RequestSection(default, "text/plain")
      {
        BodySection = new BodySection("All your base are belong to us.\n")
      };
      var updateResponse = new ResponseSection(204);
      var updateAction = new ActionSection("Update a Message", default(string?), HttpRequestMethod.Put, new[] { updateRequest }, new[] { updateResponse });

      var resource = new ResourceSection("My Message", default(string?), new UriTemplate("/message"), new[] { retrieveAction, updateAction });

      var messagesResourceGroup = new ResourceGroupSection("Messages", @"Group of all messages-related resources.", new[] { resource });

      var apiNameAndOverviewDescription = @"In this API example we will discuss what information a response can bear and
how to define multiple responses. Technically a response is represented by a
payload that is sent back in response to a request.

## API Blueprint

+ [Previous: Grouping Resources](04.%20Grouping%20Resources.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/05.%20Responses.md)

+ [Next: Requests](06.%20Requests.md)";
      var apib = new Apib {
        MetadataSection = new MetadataSection(("FORMAT", "1A")),
        ResourceGroupSections = new[] { messagesResourceGroup },
        ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Responses API", apiNameAndOverviewDescription)
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/05. Responses.json");
    }
  }
}
