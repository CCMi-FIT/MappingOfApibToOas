using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper04GroupingResourcesTests
  {
    [Fact]
    public void ApibToOasMapper04GroupingResourcesTest()
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

      var messagesResourceGroup = new ResourceGroupSection("Messages")
      {
        Description = @"Group of all messages-related resources.

This is the first group of resources in this document. It is **recognized** by
the **keyword `group`** and its name is `Messages`.

Any following resource definition is considered to be a part of this group
until another group is defined. It is **customary** to increase header level of
resources (and actions) nested under a resource.",
        ResourceSections = new[] { resource }
      };

      var usersResourceGroup = new ResourceGroupSection("Users")
      {
        Description = @"Group of all user-related resources.

This is the second group in this blueprint. For now, no resources were defined
here and as such we will omit it from the next installment of this course."
      };

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceGroupSections = new[] { messagesResourceGroup, usersResourceGroup };
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

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/04. Grouping Resources.json");
    }
  }
}
