using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi11ResourceModelTests
  {
    [Fact]
    public void ApiaryToOpenApi11ResourceModelTest()
    {
      var updateMessageAction = new ActionSection("Update a Message", null, HttpRequestMethod.Put)
      {
        RequestSections = new List<RequestSection>()
        {
          new RequestSection("Request Update Plain Text Message", "text/plain")
          {
            BodySection = new BodySection("All your base are belong to us.")
          },
          new RequestSection("Request Update JSON Message", "application/json")
          {
            BodySection = new BodySection("{ \"message\": \"All your base are belong to us.\" }")
          }
        },
        ResponseSections = new List<ResponseSection>
        {
          new ResponseSection(204)
        }
      };

      var retrieveMessageAction = new ActionSection("Retrieve a Message", "At this point we will utilize our `Message` resource model and reference it in\r\n`Response 200`.",
        HttpRequestMethod.Get)
      {
        ResponseSections = new List<ResponseSection>
        {
          new ResponseSection(200)
          {
            BodySection = new BodySection("[My Message][]")
          }
        }
      };

      var messageResource = new ResourceSection("My Message", new UriTemplate("/message"))
      {
        ResourceModelSection = new ResourceModelSection
        {
          MediaType = "application.vnd.siren+json",
          Description = "This is the `application/vnd.siren+json` message resource representation.",
          HeadersSection = new HeadersSection(new Dictionary<string, object> { ["Location"] = "http://api.acme.com/message" }),
          BodySection = new BodySection(@"{
              ""class"": [ ""message"" ],
              ""properties"": {
                    ""message"": ""Hello World!""
              },
              ""links"": [
                    { ""rel"": ""self"" , ""href"": ""/message"" }
              ]
            }")
        },
        ActionSections = new List<ActionSection> { retrieveMessageAction, updateMessageAction }
      };

      var messageResourceGroup = new ResourceGroupSection("Messages", "Group of all messages-related resources.")
      {
        ResourceSections = new List<ResourceSection> { messageResource }
      };

      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceGroupSections = new[] { messageResourceGroup };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Resource Model API",
        Description = @"Resource model is a [resource manifestation](http://www.w3.org/TR/di-gloss/#def-resource-manifestation).
One particular representation of your resource.

Furthermore, in API Blueprint, any `resource model` you have defined can be
referenced in a request or response section, saving you lots of time
maintaining your API blueprint. You simply define a resource model as any
payload (e.g. [request](https://github.com/apiaryio/api-blueprint/blob/master/examples/06.%20Requests.md)
or [response](https://github.com/apiaryio/api-blueprint/blob/master/examples/5.%20Responses.md))
and then reference it later where you would normally write a `request` or
`response`.

## API Blueprint

+ [Previous: Data Structures](10.%20Data%20Structures.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/11.%20Resource%20Model.md)

+ [Next: Advanced Action](12.%20Advanced%20Action.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/11. Resource Model.json");
    }
  }
}
