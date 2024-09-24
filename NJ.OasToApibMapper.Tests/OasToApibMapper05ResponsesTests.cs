using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper.Tests
{
  public class OasToApibMapper05ResponsesTests
  {
    [Fact]
    public void ApibToOasMapper05ResponsesTest()
    {
      var openApiObject = CreateOpenApiObject();
      var expectedApib = CreateExpectedApib();

      OasToApibMapperTestRunner.RunTest(openApiObject, expectedApib);
    }

    private OpenApiObject CreateOpenApiObject()
    {
      var infoObject = new InfoObject
      {
        Title = "Responses API",
        Version = "1.0.0",
        Description = @"In this API example we will discuss what information a response can bear and
how to define multiple responses. Technically a response is represented by a
payload that is sent back in response to a request.

## API Blueprint
+ [Previous: Grouping Resources](04.%20Grouping%20Resources.md)
+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/05.%20Responses.md)
+ [Next: Requests](06.%20Requests.md)
"
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
                Headers = new Dictionary<string, IHeaderOrReferenceObject>()
                {
                  {
                    "X-My-Message-Header",
                    new HeaderObject
                    {
                      Schema = new SchemaObject { Type = "string" }
                    }
                  }
                },
                Content = new Dictionary<string, MediaTypeObject>
                {
                  {
                    "text/plain",
                    new MediaTypeObject
                    {
                      Example = "Hello World!\n"
                    }
                  },
                  {
                    "application/json",
                    new MediaTypeObject
                    {
                      Schema = new SchemaObject
                      {
                        Type = "object",
                        Properties = new { message = new { Type = "string" } }
                      },
                      Example = new { message = "Hello World!" }
                    }
                  }
                }
              }
            }
          }
        },
        Summary = "Retrieve a Message",
        OperationId = "Retrieve a Message",
        Description = @"This action has **two** responses defined: One returning plain text and the
other a JSON representation of our resource. Both have the same HTTP status
code. Also both responses bear additional information in the form of a custom
HTTP header. Note that both responses have set the `Content-Type` HTTP header
just by specifying `(text/plain)` or `(application/json)` in their respective
signatures.",
        Parameters = new IParameterOrReferenceObject[0]
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
        Summary = "Update a Message",
        OperationId = "Update a Message",
        Tags = new[] { "Messages" },
        Parameters = new IParameterOrReferenceObject[0],
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
              Summary = "My Message"
            }
          }
        }
      };

      var components = new ComponentsObject { Schemas = new Dictionary<string, SchemaObject>() };

      var tags = new[]
      {
        new TagObject
        {
          Name = "Messages",
          Description = "Group of all messages-related resources.\n\nThis is the first group of resources in this document. It is **recognized** by\nthe **keyword `group`** and its name is `Messages`.\n\nAny following resource definition is considered to be a part of this group\nuntil another group is defined. It is **customary** to increase header level of\nresources (and actions) nested under a resource."
        },
        new TagObject
        {
          Name = "Users",
          Description = "Group of all user-related resources.\n\nThis is the second group in this blueprint. For now, no resources were defined\nhere and as such we will omit it from the next installment of this course."
        }
      };

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

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceSections = new[] { resource };
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
      return apib;
    }
  }
}
