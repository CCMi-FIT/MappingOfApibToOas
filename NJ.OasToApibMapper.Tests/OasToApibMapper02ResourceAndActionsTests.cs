using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper.Tests
{
  public class OasToApibMapper02ResourceAndActionsTests
  {
    [Fact]
    public void ApibToOasMapper02ResourceAndActionsTest()
    {
      var openApiObject = CreateOpenApiObject();
      var expectedApib = CreateExpectedApib();

      OasToApibMapperTestRunner.RunTest(openApiObject, expectedApib);
    }

    private static OpenApiObject CreateOpenApiObject()
    {
      var infoObject = new InfoObject
      {
        Title = "Resource and Actions API",
        Version = "1.0.0",
        Description = @"This API example demonstrates how to define a resource with multiple actions.

## API Blueprint

+ [Previous: The Simplest API](01.%20Simplest%20API.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/02.%20Resource%20and%20Actions.md)

+ [Next: Named Resource and Actions](03.%20Named%20Resource%20and%20Actions.md)"
      };
      var getOperationObject = new OperationObject
      {
        Responses = new ResponsesObject
        {
          HttpStatusCodesWithResponses = new Dictionary<string, IResponseOrReferenceObject>
          {
            {
              "200",
              new ResponseObject
              {
                Description = "OK",
                Headers = new Dictionary<string, IHeaderOrReferenceObject>(),
                Content = new Dictionary<string, MediaTypeObject>
                {
                  {
                    "text/plain",
                    new MediaTypeObject
                    {
                      Example = "Hello World!\n"
                    }
                  }
                }
              }
            }
          }
        },
        Summary = "",
        OperationId = "",
        Tags = new string[0],
        Parameters = new IParameterOrReferenceObject[0],
        Description = "Here we define an action using the `GET` [HTTP request method](http://www.w3schools.com/tags/ref_httpmethods.asp) for our resource `/message`.\n\nAs with every good action it should return a\n[response](http://www.w3.org/TR/di-gloss/#def-http-response). A response always\nbears a status code. Code 200 is great as it means all is green. Responding\nwith some data can be a great idea as well so let's add a plain text message to\nour response."
      };
      var putOperationObject = new OperationObject
      {
        Responses = new ResponsesObject
        {
          HttpStatusCodesWithResponses = new Dictionary<string, IResponseOrReferenceObject>
          {
            {
              "204",
              new ResponseObject
              {
                Description = "No Content",
                Headers = new Dictionary<string, IHeaderOrReferenceObject>(),
                Content = new Dictionary<string, MediaTypeObject>()
              }
            }
          }
        },
        Summary = "",
        OperationId = "",
        Tags = new string[0],
        Parameters = new IParameterOrReferenceObject[0],
        Description = "OK, let's add another action. This time to put new data to our resource\n(essentially an update action). We will need to send something in a\n[request](http://www.w3.org/TR/di-gloss/#def-http-request) and then send a\nresponse back confirming the posting was a success (_HTTP Status Code 204 ~\nResource updated successfully, no content is returned_).",
        RequestBody = new RequestBodyObject
        {
          Content = new Dictionary<string, MediaTypeObject>
          {
            { "text/plain", new MediaTypeObject { Example = "All your base are belong to us.\n" } }
          }
        }
      };
      var pathsObject = new PathsObject
      {
        PathItems = new Dictionary<string, PathItemObject>
        {
          {
            "/message",
            new PathItemObject
            {
              Get = getOperationObject,
              Put = putOperationObject,
              Description = "This is our [resource](http://www.w3.org/TR/di-gloss/#def-resource). It is\ndefined by its\n[URI](http://www.w3.org/TR/di-gloss/#def-uniform-resource-identifier) or, more\nprecisely, by its [URI Template](http://tools.ietf.org/html/rfc6570).\n\nThis resource has no actions specified but we will fix that soon."
            }
          }
        }
      };

      var components = new ComponentsObject { Schemas = new Dictionary<string, SchemaObject>() };

      var openApiObject = new OpenApiObject
      {
        OpenApi = "3.0.3",
        Info = infoObject,
        Paths = pathsObject,
        Components = components,
        Tags = new TagObject[0]
      };
      return openApiObject;
    }

    private static Apib CreateExpectedApib()
    {
      var expectedApib = new Apib();
      // TODO ?
      //expectedApib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      expectedApib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
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
        BodySection = new BodySection("Hello World!\n")
      };
      var getAction = new ActionSection
      {
        HttpRequestMethod = HttpRequestMethod.Get,
        Description =
          "Here we define an action using the `GET` [HTTP request method](http://www.w3schools.com/tags/ref_httpmethods.asp) for our resource `/message`.\n\nAs with every good action it should return a\n[response](http://www.w3.org/TR/di-gloss/#def-http-response). A response always\nbears a status code. Code 200 is great as it means all is green. Responding\nwith some data can be a great idea as well so let's add a plain text message to\nour response.",
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
        Description = "OK, let's add another action. This time to put new data to our resource\n(essentially an update action). We will need to send something in a\n[request](http://www.w3.org/TR/di-gloss/#def-http-request) and then send a\nresponse back confirming the posting was a success (_HTTP Status Code 204 ~\nResource updated successfully, no content is returned_).",
        RequestSections = new[] { putRequest },
        ResponseSections = new[] { putResponse }
      };
      var messageResource = new ResourceSection
      {
        UriTemplate = new UriTemplate("/message"),
        Description = "This is our [resource](http://www.w3.org/TR/di-gloss/#def-resource). It is\ndefined by its\n[URI](http://www.w3.org/TR/di-gloss/#def-uniform-resource-identifier) or, more\nprecisely, by its [URI Template](http://tools.ietf.org/html/rfc6570).\n\nThis resource has no actions specified but we will fix that soon.",
        ActionSections = new[] { getAction, putAction }
      };
      expectedApib.ResourceSections = new List<ResourceSection>
      {
        messageResource
      };
      return expectedApib;
    }
  }
}
