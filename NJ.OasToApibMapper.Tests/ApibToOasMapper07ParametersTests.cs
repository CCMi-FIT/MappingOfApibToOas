using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper07ParametersTests
  {
    [Fact]
    public void ApibToOasMapper07ParametersTest()
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
        BodySection = new BodySection { Content = "{ \"id\": 1, \"message\": \"Hello World!\" }" }
      };
      var retrieveAction = new ActionSection
      {
        Identifier = "Retrieve a Message",
        HttpRequestMethod = HttpRequestMethod.Get,
        RequestSections = new[] { retrieveTextPlainRequest, retrieveJsonRequest },
        ResponseSections = new[] { retrieveTextPlainResponse, retrieveApplicationJsonResponse }
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

      var myMessageResource = new ResourceSection
      {
        Identifier = "My Message",
        UriTemplate = new UriTemplate("/message/{id}"),
        ActionSections = new[] { retrieveAction, updateAction },
        Description = @"Here we have added the message `id` parameter as an 
[URI Template variable](http://tools.ietf.org/html/rfc6570) in the Message
resource's URI. Note the parameter name `id` is enclosed in curly brackets. We
will discuss this parameter in the `Parameters` section below, where we will
also set its example value to `1` and declare it of an arbitrary 'number' type.",
        ParametersSection = new UriParametersSection
        {
          Parameters = new List<UriParameter>
          {
            new UriParameter("id", true, "number", "An unique identifier of the message.")
            {
              ExampleValue = 1
            }
          }
        }
      };

      var retrieveAllMessagesResponse = new ResponseSection
      {
        MediaType = "application/json",
        HttpStatusCode = 200,
        BodySection = new BodySection(@"[
          {
            ""id"": 1,
            ""message"": ""Hello World!""
          },
          {
            ""id"": 2,
            ""message"": ""Time is an illusion. Lunchtime doubly so.""
          },
          {
            ""id"": 3,
            ""message"": ""So long, and thanks for all the fish.""
          }
        ]")
      };

      var retrieveAllMessagesAction = new ActionSection
      {
        Identifier = "Retrieve all Messages",
        HttpRequestMethod = HttpRequestMethod.Get,
        UriParametersSection = new UriParametersSection
        {
          Parameters = new List<UriParameter>
          {
            new UriParameter("limit", false, "number", "The maximum number of results to return.")
            {
              DefaultValue = 20
            }
          }
        },
        ResponseSections = new[] { retrieveAllMessagesResponse }
      };

      var allMyMessagesResource = new ResourceSection
      {
        Identifier = "All My Messages",
        HttpRequestMethod = HttpRequestMethod.Get,
        UriTemplate = new UriTemplate("/messages{?limit}"),
        Description = @"A resource representing all of my messages in the system.

We have added the query URI template parameter - `limit`. This parameter is
used for limiting the number of results returned by some actions on this
resource. It does not affect every possible action of this resource, therefore
we will discuss it only at the particular action level below.",
        ActionSections = new[] { retrieveAllMessagesAction }
      };

      var messagesResourceGroup = new ResourceGroupSection("Messages")
      {
        Description = @"Group of all messages-related resources.",
        ResourceSections = new[] { myMessageResource, allMyMessagesResource }
      };

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceGroupSections = new[] { messagesResourceGroup };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Parameters API",
        Description = @"In this installment of the API Blueprint course we will discuss how to describe URI parameters.

But first let's add more messages to our system. For that we would need
introduce an message identifier – id. This id will be our parameter when
communicating with our API about messages.

## API Blueprint

+ [Previous: Requests](06.%20Requests.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/07.%20Parameters.md)

+ [Next: Attributes](08.%20Attributes.md)"
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/07. Parameters - 02.json");
    }
  }
}
