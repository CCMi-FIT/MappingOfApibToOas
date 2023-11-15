using Newtonsoft.Json;
using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApiMapperEdumeTests
  {
    [Fact]
    public void ApiaryTopOpenApiMapperEdumeTest()
    {
      var apiaryApiBlueprint = CreateMockApiaryApiBlueprint();
      var openApiObject = ApiaryApiBlueprintToOpenApiMapper.Map(apiaryApiBlueprint);
      var tmp = JsonConvert.SerializeObject(openApiObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }

    private ApiaryApiBlueprint CreateMockApiaryApiBlueprint()
    {
      var metadataSection = new MetadataSection
      {
        { "FORMAT", "1A" },
        { "HOST", "https://api.edume.com" }
      };

      var apiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "eduMe API",
        Description =
          "###  Join the eduMe Slack Community\n\nIf you are getting started with integrating eduMe into your product or even if\nyou have already implemented and are looking at ways to further improve your\nintegrations then join our Slack community for support, updates and best practices.\n\nTo join the community, [click here](https://communityinviter.com/apps/edumeondemand/edume-on-demand) to get started.\n\n###  Authorization\n\nAuthorization in the eduMe API is handled through the use of API keys, which will look something like `5908ff10-9e8e-4523-a418-2305c0d1ef0b`.\nThese can be unique to a specific group or apply to the whole company.\nFor all requests to the eduMe API, you should include this key in the `X-API-KEY` header.\n\nTo receive an API key, <a href = \"mailto:support@edume.com?subject=API Key Request&body = Message\">\ncontact us</a>."
      };

      var statusResourceGroup = CreateStatusResourceGroup();
      var usersResourceGroup = CreateUsersResourceGroup();

      var apiaryApiBlueprint = new ApiaryApiBlueprint();
      apiaryApiBlueprint.MetadataSection = metadataSection;
      apiaryApiBlueprint.ApiNameAndOverviewSection = apiNameAndOverviewSection;
      apiaryApiBlueprint.ResourceGroupSections = new[] { statusResourceGroup, usersResourceGroup };
      return apiaryApiBlueprint;
    }

    private ResourceGroupSection CreateStatusResourceGroup()
    {
      var healthCheckResource = CreateHealthCheckResource();
      return new ResourceGroupSection("Status", null, new List<ResourceSection> { healthCheckResource }
      );
    }

    private ResourceSection CreateHealthCheckResource()
    {
      var uriTemplate = new UriTemplate("/public/healthCheck");
      var result = new ResourceSection("Health Check", uriTemplate);

      var healthCheckAction = GetHealthCheckAction();
      result.ActionSections = new List<ActionSection> { healthCheckAction };

      return result;
    }

    private ActionSection GetHealthCheckAction()
    {
      var result = new ActionSection("Get API Status", "**Endpoint**: <code>**GET** /public/healthCheck</code>\n\n**Response Type**: `JSON`\n\n<br />\n<br />\n<br />\n<br />\n\nReturns the status of the backend.\n\n**Use Cases**:\n\n+ Check if the service is running\n\n+ Confirm your API key is correct\n\n**Considerations**:\n\n+ Can also check the [status](https://edume.statuspage.io/) page for incident history\n\n**Related Endpoints**:\n\n+ All endpoints\n\n**Auth**:\n\n+ x-api-key: `YOUR_API_KEY` (Required)", HttpRequestMethod.Get);

      var requestSection = new RequestSection(null, "application/json");
      requestSection.HeadersSection = new HeadersSection(new Dictionary<string, object> { { "x-api-key", "YOUR_API_KEY" } });
      result.RequestSections = new[] { requestSection };

      var _200ResponseContent =
        @"{
    ""status"": ""ok""
}";

      result.ResponseSections = new List<ResponseSection>
      {
        new ResponseSection(200, "application/json") { BodySection = new BodySection(_200ResponseContent) }
      };

      return result;
    }

    private ResourceGroupSection CreateUsersResourceGroup()
    {
      var userCreationResource = CreateUserCreationResource();
      return new ResourceGroupSection("Users", null, new List<ResourceSection> { userCreationResource }
      );
    }

    private ResourceSection CreateUserCreationResource()
    {
      var uriTemplate = new UriTemplate("/public/team/{teamId}/user");
      var result = new ResourceSection("User Creation", uriTemplate);

      var createUserAction = CreateCreateUserAction();
      result.ActionSections = new List<ActionSection> { createUserAction };

      return result;
    }

    private ActionSection CreateCreateUserAction()
    {
      var result = new ActionSection("Get API Status", "**Endpoint**: <code>**POST** /public/team/{teamId}/user</code>\n\n**Response Type**: `JSON`\n\n<br />\n<br />\n<br />\n<br />\n\nCreate a user for a given team\n\n**Use Cases**:\n\n+ Add users to account when getting started or onboarding a new user\n\n**Considerations**:\n\n+ Not required if using a seamless link flow (new users added by default with seamless links on first access)\n\n+ When the user is created, they are automatically assigned to the specified team, as well as the group\n\n**Related Endpoints**:\n\n+ Delete a User\n\n+ Get All Users\n\n+ Update a User\n\n**Auth**:\n\n+ x-api-key: `YOUR_API_KEY` (Required)", HttpRequestMethod.Get);

      var requestSection = new RequestSection(null, "application/json");
      requestSection.HeadersSection =
        new HeadersSection(new Dictionary<string, object> { { "x-api-key", "YOUR_API_KEY" } });
      var attributes = new[]
      {
        new AttributeSection("firstName", "The first name of the user. Minimum length: 2", true,
          "string", "Alexander"),
        new AttributeSection("lastName", "The last name of the user. Minimum length: 2", true,
          "string", "Corte"),
        new AttributeSection("employeeId", "An employee ID for the user, or other additional detail.",
          false, "string", "asfe2f"),
        new AttributeSection("sendInvite",
          "Whether to send an invitation email (or SMS) to the user. Set this to false if you prefer to contact your users yourself.", false, "boolean", "true", true)
      };
      requestSection.AttributesSection = new AttributesSection { Attributes = attributes };
      result.RequestSections = new[] { requestSection };

      var teamIdParameter = new UriParameter("teamId", true, "number", "The ID of the team to create the user in");
      teamIdParameter.ExampleValue = 312;
      var parametersSection = new UriParametersSection();
      parametersSection.Parameters.Add(teamIdParameter);

      var _200ResponseContent =
        @"{
    ""status"": ""ok""
}";

      result.ResponseSections = new List<ResponseSection>
      {
        new ResponseSection(200, "application/json") { BodySection = new BodySection(_200ResponseContent) }
      };

      return result;
    }
  }
}
