using NJ.ApibModel;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper07ParametersTests
  {
    [Fact]
    public void ApibToOasMapper07ParametersTest()
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
        BodySection = new BodySection("{ \"id\": 1, \"message\": \"Hello World!\" }")
      };
      var retrieveAction = new ActionSection("Retrieve a Message", default(string?), HttpRequestMethod.Get, new[] { retrieveTextPlainRequest, retrieveJsonRequest }, new[] { retrieveTextPlainResponse, retrieveApplicationJsonResponse });

      var updateTextPlainRequest = new RequestSection("Update Plain Text Message", "text/plain")
      {
        BodySection = new BodySection("All your base are belong to us.\n")
      };
      var updateJsonRequest = new RequestSection("Update JSON Message", "application/json")
      {
        BodySection = new BodySection("{ \"message\": \"All your base are belong to us.\" }")
      };
      var updateResponse = new ResponseSection(204);
      var updateAction = new ActionSection("Update a Message", default(string?), HttpRequestMethod.Put, new[] { updateTextPlainRequest, updateJsonRequest }, new[] { updateResponse });

      var myMessageResourceDescription = @"Here we have added the message `id` parameter as an 
[URI Template variable](http://tools.ietf.org/html/rfc6570) in the Message
resource's URI. Note the parameter name `id` is enclosed in curly brackets. We
will discuss this parameter in the `Parameters` section below, where we will
also set its example value to `1` and declare it of an arbitrary 'number' type.";
      var myMessageResource = new ResourceSection("My Message", myMessageResourceDescription, new UriTemplate("/message/{id}"), default, new[] { retrieveAction, updateAction })
      {
        ParametersSection = new UriParametersSection(new[] {
            new UriParameter("id", true, "number", "An unique identifier of the message.")
            {
              ExampleValue = 1
            }
          }
        )
      };

      var retrieveAllMessagesResponse = new ResponseSection(200, "application/json")
      {
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

      var retrieveAllMessagesAction = new ActionSection("Retrieve all Messages", default(string?), HttpRequestMethod.Get, new[] { retrieveAllMessagesResponse })
      {
        ParametersSection = new UriParametersSection(new[] {
            new UriParameter("limit", false, "number", "The maximum number of results to return.")
            {
              DefaultValue = 20
            }
          }
        )
      };

      var allMyMessagesResourceDescription = @"A resource representing all of my messages in the system.

We have added the query URI template parameter - `limit`. This parameter is
used for limiting the number of results returned by some actions on this
resource. It does not affect every possible action of this resource, therefore
we will discuss it only at the particular action level below.";
      var allMyMessagesResource = new ResourceSection("All My Messages", allMyMessagesResourceDescription, new UriTemplate("/messages{?limit}"), HttpRequestMethod.Get, new[] { retrieveAllMessagesAction });

      var messagesResourceGroup = new ResourceGroupSection("Messages", @"Group of all messages-related resources.", new[] { myMessageResource, allMyMessagesResource });

      var apiNameAndOverviewDescription = @"In this installment of the API Blueprint course we will discuss how to describe URI parameters.

But first let's add more messages to our system. For that we would need
introduce an message identifier – id. This id will be our parameter when
communicating with our API about messages.

## API Blueprint

+ [Previous: Requests](06.%20Requests.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/07.%20Parameters.md)

+ [Next: Attributes](08.%20Attributes.md)";
      var apib = new Apib
      {
        MetadataSection = new MetadataSection(("FORMAT", "1A")),
        ResourceGroupSections = new[] { messagesResourceGroup },
        ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Parameters API", apiNameAndOverviewDescription)
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/07. Parameters - 02.json");
    }
  }
}
