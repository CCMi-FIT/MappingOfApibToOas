using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi06RequestsTests
  {
    [Fact]
    public void ApiaryToOpenApi06RequestsTest()
    {
      var retrieveTextPlainRequest = new RequestSection("Plain Text Message")
      {
        HeadersSection = new HeadersSection { { "Accept", "text/plain" } }
      };
      var retrieveTextPlainResponse = new ResponseSection
      {
        HttpStatusCode = 200,
        MediaType = "text/plain",
        HeadersSection = new HeadersSection(new Dictionary<string, object> { { "X-My-Message-Header", 42 } }),
        BodySection = new BodySection { Content = "Hello World!\n" }
      };
      var retrieveJsonRequest = new RequestSection("JSON Message")
      {
        HeadersSection = new HeadersSection { { "Accept", "application/json" } }
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
        RequestSections = new[] { retrieveTextPlainRequest, retrieveJsonRequest },
        ResponseSections = new[] { retrieveTextPlainResponse, retrieveApplicationJsonResponse },
        Description = @"In API Blueprint, _requests_ can hold exactly the same kind of information and
can be described using exactly the same structure as _responses_, only with
different signature – using the `Request` keyword. The string that follows
after the `Request` keyword is a request identifier. Again, using explanatory
and simple naming is the best way to go."
      };

      var updateTextPlainRequest = new RequestSection("Update Plain Text Message")
      {
        MediaType = "text/plain",
        BodySection = new BodySection { Content = "All your base are belong to us.\n" }
      };
      var updateJsonRequest = new RequestSection("Update JSON Message")
      {
        MediaType = "application/json",
        BodySection = new BodySection { Content = "{ \"message\": \"All your base are belong to us.\" }" }
      };
      var updateResponse = new ResponseSection
      {
        HttpStatusCode = 204
      };
      var updateAction = new ActionSection
      {
        Identifier = "Update a Message",
        HttpRequestMethod = HttpRequestMethod.Put,
        RequestSections = new[] { updateTextPlainRequest, updateJsonRequest },
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

      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceGroupSections = new[] { messagesResourceGroup };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Requests API",
        Description = @"Following the [Responses](05.%20Responses.md) example, this API will show you
how to define multiple requests and what data these requests can bear. Let's
demonstrate multiple requests on a trivial example of content negotiation.

## API Blueprint

+ [Previous: Responses](05.%20Responses.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/06.%20Requests.md)

+ [Next: Parameters](07.%20Parameters.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/06. Requests.json");
    }
  }
}
