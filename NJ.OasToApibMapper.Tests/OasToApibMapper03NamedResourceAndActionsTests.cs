using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper.Tests
{
  public class OasToApibMapper03NamedResourceAndActionsTests
  {
    [Fact]
    public void ApibToOasMapper03NamesResourceAndActionsTest()
    {
      var openApiObject = CreateOpenApiObject();
      var expectedApib = CreateExpectedApib();
      OasToApibMapperTestRunner.RunTest(openApiObject, expectedApib);
    }


    private static OpenApiObject CreateOpenApiObject()
    {
      var infoObject = new InfoObject
      {
        Title = "Named Resource and Actions API",
        Version = "1.0.0",
        Description = @"This API example demonstrates how to name a resource and its actions, to give
the reader a better idea about what the resource is used for.

## API Blueprint

+ [Previous: Resource and Actions](02.%20Resource%20and%20Actions.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/03.%20Named%20Resource%20and%20Actions.md)

+ [Next: Grouping Resources](04.%20Grouping%20Resources.md)"
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
        Summary = "Retrieve a Message",
        OperationId = "Retrieve a Message",
        Tags = new string[0],
        Parameters = new IParameterOrReferenceObject[0],
        Description = "Now this is informative! No extra explanation needed here. This action clearly\nretrieves the message."
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
        Tags = new string[0],
        Parameters = new IParameterOrReferenceObject[0],
        Description = "`Update a message` - nice and simple naming is the best way to go.",
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
              Summary = "My Message",
              Description = @"OK, `My Message` probably isn't the best name for our resource but it will do for now. Note the URI `/message` is enclosed in square brackets."
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
      var retrieveResponse = new ResponseSection
      {
        HttpStatusCode = 200,
        MediaType = "text/plain",
        BodySection = new BodySection { Content = "Hello World!\n" }
      };
      var retrieveAction = new ActionSection
      {
        Identifier = "Retrieve a Message",
        HttpRequestMethod = HttpRequestMethod.Get,
        Description = "Now this is informative! No extra explanation needed here. This action clearly\nretrieves the message.",
        ResponseSections = new[] { retrieveResponse }
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
        Description = "`Update a message` - nice and simple naming is the best way to go.",
        RequestSections = new[] { updateRequest },
        ResponseSections = new[] { updateResponse }
      };

      var resource = new ResourceSection
      {
        Identifier = "My Message",
        UriTemplate = new UriTemplate("/message"),
        Description = @"OK, `My Message` probably isn't the best name for our resource but it will do for now. Note the URI `/message` is enclosed in square brackets.",
        ActionSections = new[] { retrieveAction, updateAction }
      };

      var apib = new Apib();
      // TODO ?
      //apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceSections = new[] { resource };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Named Resource and Actions API",
        Description =
          @"This API example demonstrates how to name a resource and its actions, to give
the reader a better idea about what the resource is used for.

## API Blueprint

+ [Previous: Resource and Actions](02.%20Resource%20and%20Actions.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/03.%20Named%20Resource%20and%20Actions.md)

+ [Next: Grouping Resources](04.%20Grouping%20Resources.md)"
      };
      return apib;
    }
  }
}
