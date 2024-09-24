using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper.Tests
{
  public class OasToApibMapper04GroupingResourcesTests
  {
    [Fact]
    public void ApibToOasMapper04GroupingResourcesTest()
    {
      var openApiObject = CreateOpenApiObject();
      var expectedApib = CreateExpectedApib();

      OasToApibMapperTestRunner.RunTest(openApiObject, expectedApib);
    }
    private static OpenApiObject CreateOpenApiObject()
    {
      var infoObject = new InfoObject
      {
        Title = "Grouping Resources API",
        Version = "1.0.0",
        Description = @"This API example demonstrates how to group resources and form **groups of
resources**. You can create as many or as few groups as you like. If you do not
create any group all your resources will be part of an ""unnamed"" group.

## API Blueprint

+ [Previous: Named Resource and Actions](03.%20Named%20Resource%20and%20Actions.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/04.%20Grouping%20Resources.md)

+ [Next: Responses](05.%20Responses.md)"
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
        Tags = new[] { "Messages" },
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
      apib.ResourceSections = new[] { resource };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Grouping Resources API",
        Description = @"This API example demonstrates how to group resources and form **groups of
resources**. You can create as many or as few groups as you like. If you do not
create any group all your resources will be part of an ""unnamed"" group.

## API Blueprint

+ [Previous: Named Resource and Actions](03.%20Named%20Resource%20and%20Actions.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/04.%20Grouping%20Resources.md)

+ [Next: Responses](05.%20Responses.md)"
      };
      return apib;
    }
  }
}
