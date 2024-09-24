using NJ.ApibModel;
using NJ.OasModel;

namespace NJ.OasToApibMapper.Tests
{
  public class OasToApibMapper01SimplestApiTests
  {
    [Fact]
    public void ApibToOasMapper01SimplestApiTest()
    {
      var openApiObject = CreateOpenApiObject();
      var expectedApib = CreateExpectedApib();
      OasToApibMapperTestRunner.RunTest(openApiObject, expectedApib);
    }

    private static OpenApiObject CreateOpenApiObject()
    {
      var infoObject = new InfoObject
      {
        Title = "The Simplest API",
        Version = "1.0.0",
        Description = @"This is one of the simplest APIs written in the **API Blueprint**. One plain
resource combined with a method and that's it! We will explain what is going on
in the next installment - 
[Resource and Actions](02.%20Resource%20and%20Actions.md).

**Note:** As we progress through the examples, do not also forget to view the
[Raw](https://raw.github.com/apiaryio/api-blueprint/master/examples/01.%20Simplest%20API.md)
code to see what is really going on in the API Blueprint, as opposed to just
seeing the output of the Github Markdown parser.

Also please keep in mind that every single example in this course is a **real
API Blueprint** and as such you can **parse** it with the 
[API Blueprint parser](https://github.com/apiaryio/drafter) or one of its
[bindings](https://github.com/apiaryio/drafter#bindings).

## API Blueprint
+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/01.%20Simplest%20API.md)
+ [Next: Resource and Actions](02.%20Resource%20and%20Actions.md)"
      };
      var pathsObject = new PathsObject
      {
        PathItems = new Dictionary<string, PathItemObject>
        {
          {
            "/message",
            new PathItemObject
            {
              Get = new OperationObject
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
                Parameters = new IParameterOrReferenceObject[0]
              }
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
        Name = "The Simplest API",
        Description =
          @"This is one of the simplest APIs written in the **API Blueprint**. One plain
resource combined with a method and that's it! We will explain what is going on
in the next installment - 
[Resource and Actions](02.%20Resource%20and%20Actions.md).

**Note:** As we progress through the examples, do not also forget to view the
[Raw](https://raw.github.com/apiaryio/api-blueprint/master/examples/01.%20Simplest%20API.md)
code to see what is really going on in the API Blueprint, as opposed to just
seeing the output of the Github Markdown parser.

Also please keep in mind that every single example in this course is a **real
API Blueprint** and as such you can **parse** it with the 
[API Blueprint parser](https://github.com/apiaryio/drafter) or one of its
[bindings](https://github.com/apiaryio/drafter#bindings).

## API Blueprint
+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/01.%20Simplest%20API.md)
+ [Next: Resource and Actions](02.%20Resource%20and%20Actions.md)"
      };
      expectedApib.ResourceSections = new[]
      {
        new ResourceSection
        {
          // TODO ?
          //HttpRequestMethod = HttpRequestMethod.Get,
          UriTemplate = new UriTemplate("/message"),
          //Description = "Hello World!",
          ActionSections = new []
          {
            new ActionSection
            {
              ResponseSections = new[]
              {
                new ResponseSection(200, "text/plain")
                {
                  BodySection = new BodySection("Hello World!\n")
                }
              },
              HttpRequestMethod = HttpRequestMethod.Get
            }
          }
        }
      };
      return expectedApib;
    }
  }
}