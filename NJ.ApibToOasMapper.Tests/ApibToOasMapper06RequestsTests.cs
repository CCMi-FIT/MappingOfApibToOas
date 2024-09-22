using NJ.ApibModel;
using NJ.ApibModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
    public class ApibToOasMapper06RequestsTests
  {
    [Fact]
    public void ApibToOasMapper06RequestsTest()
    {
      var retrieveTextPlainRequest = new RequestSection("Plain Text Message")
      {
        HeadersSection = new HeadersSection(("Accept", "text/plain"))
      };
      var retrieveTextPlainResponse = new ResponseSection(200, "text/plain")
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection("Hello World!\n")
      };
      var retrieveJsonRequest = new RequestSection("JSON Message")
      {
        HeadersSection = new HeadersSection(("Accept", "application/json"))
      };
      var retrieveApplicationJsonResponse = new ResponseSection(200, "application/json")
      {
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection ("{ \"message\": \"Hello World!\" }")
      };
      var retrieveActionDescription = @"In API Blueprint, _requests_ can hold exactly the same kind of information and
can be described using exactly the same structure as _responses_, only with
different signature – using the `Request` keyword. The string that follows
after the `Request` keyword is a request identifier. Again, using explanatory
and simple naming is the best way to go.";
      var retrieveAction = new ActionSection("Retrieve a Message", retrieveActionDescription, HttpRequestMethod.Get, new[] { retrieveTextPlainRequest, retrieveJsonRequest }, new[] { retrieveTextPlainResponse, retrieveApplicationJsonResponse });

      var updateTextPlainRequest = new RequestSection("Update Plain Text Message", "text/plain")
      {
        BodySection = new BodySection ("All your base are belong to us.\n")
      };
      var updateJsonRequest = new RequestSection("Update JSON Message", "application/json")
      {
        BodySection = new BodySection("{ \"message\": \"All your base are belong to us.\" }")
      };
      var updateResponse = new ResponseSection(204);
      var updateAction = new ActionSection("Update a Message", default(string?), HttpRequestMethod.Put, new[] { updateTextPlainRequest, updateJsonRequest }, new[] { updateResponse });

      var resource = new ResourceSection("My Message", new UriTemplate("/message"), new[] { retrieveAction, updateAction });

      var messagesResourceGroup = new ResourceGroupSection("Messages", @"Group of all messages-related resources.", new[] { resource });

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection(("FORMAT", "1A"));
      apib.ResourceGroupSections = new[] { messagesResourceGroup };
      var apiNameAndOverviewDescription = @"Following the [Responses](05.%20Responses.md) example, this API will show you
how to define multiple requests and what data these requests can bear. Let's
demonstrate multiple requests on a trivial example of content negotiation.

## API Blueprint

+ [Previous: Responses](05.%20Responses.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/06.%20Requests.md)

+ [Next: Parameters](07.%20Parameters.md)";
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Requests API", apiNameAndOverviewDescription);

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/06. Requests.json");
    }
  }
}
