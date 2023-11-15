using Newtonsoft.Json;
using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApiMapperBasicTests
  {
    [Fact]
    public void ApiaryToOpenApiMapperBasicCSharpTest()
    {
      var apiaryApi = CreateMockApiaryApiBlueprint();
      var openApiObject = ApiaryApiBlueprintToOpenApiMapper.Map(apiaryApi);
      var tmp = JsonConvert.SerializeObject(openApiObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }

    [Fact]
    public void ApiaryToOpenApiMapperBasicFSharpTest()
    {
      var apiaryApiBlueprint = CreateMockApiaryApiBlueprint();
      //var openApiObject = FSharp.Mapper.MapApiaryToOpenApi(apiaryApiBlueprint);
      //var tmp = JsonConvert.SerializeObject(openApiObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }

    private ApiaryApiBlueprint CreateMockApiaryApiBlueprint()
    {
      var metadataSection = new MetadataSection
      {
        { "FORMAT", "1A" },
        { "HOST", "http://sample.pandurangpatil.com" }
      };

      var apiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Sample API Documentation",
        Description =
          "Sample api documentation for sample project.\n\n# Allowed HTTPs requests:\n\n<pre>\nPUT     : To create resource \nPOST    : Update resource\nGET     : Get a resource or list of resources\nDELETE  : To delete resource\n</pre>\n\n# Description Of Usual Server Responses:\n\n- 200 `OK` - the request was successful (some API calls may return 201 instead).\n\n- 201 `Created` - the request was successful and a resource was created.\n\n- 204 `No Content` - the request was successful but there is no representation to return (i.e. the response is empty).\n\n- 400 `Bad Request` - the request could not be understood or was missing required parameters.\n\n- 401 `Unauthorized` - authentication failed or user doesn't have permissions for requested operation.\n\n- 403 `Forbidden` - access denied.\n\n- 404 `Not Found` - resource was not found.\n\n- 405 `Method Not Allowed` - requested method is not supported for resource.\n\n# Some sample\n\nTest sample                 |   test column two\n    First column sadf asdfads   |   sfsdsadf\n\n---\n\ntest one                    | Another column\n\n---\n\n# Table sample\n\n<table>\n    <tr>\n        <td> First Column </td>\n        <td> Second Column </td>    \n    </tr>\n    <tr>\n        <td> First Column </td>\n        <td> Second Column </td> \n    </tr>\n</table>\n\n# Code Sample\n\n<pre>\n    Some code here\n</pre>"
      };

      var resourcesGroup = CreateUserResourceGroup();

      var apiaryApiBlueprint = new ApiaryApiBlueprint();
      apiaryApiBlueprint.MetadataSection = metadataSection;
      apiaryApiBlueprint.ApiNameAndOverviewSection = apiNameAndOverviewSection;
      apiaryApiBlueprint.ResourceGroupSections = new[] { resourcesGroup };
      return apiaryApiBlueprint;
    }

    private ResourceGroupSection CreateUserResourceGroup()
    {
      var userCollectionResource = CreateUserCollectionResource();
      var userResource = CreateUserResource();
      return new ResourceGroupSection(
        "User",
        @"Represents user details.

---
**User attributes:**

- id `(Number)` : unique identifier. 
- fname `(String)` : First Name.
- lname `(String)` : Last Name.
- email `(String)` : email id of the user.

---",
        new List<ResourceSection> { userCollectionResource, userResource }
      );
    }

    private ResourceSection CreateUserResource()
    {
      var uriTemplate = new UriTemplate("/users/{id}");
      var result = new ResourceSection("User", uriTemplate);
      var idParameter = new UriParameter("id", true, "Number", "Numeric `id` of the User to perform action with.");
      idParameter.ExampleValue = "1";
      result.ParametersSection = new UriParametersSection(new[] { idParameter });

      var retrieveUserAction = CreateRetrieveUserAction();
      var updateUserAction = CreateUpdateUserAction();
      var removeUserAction = CreateRemoveUserAction();

      result.ActionSections = new[] { retrieveUserAction, updateUserAction, removeUserAction };
      return result;
    }

    private ResourceSection CreateUserCollectionResource()
    {
      var userCollectionUriTemplate = new UriTemplate("/users(?since,limit)");
      var result = new ResourceSection("User Collection", userCollectionUriTemplate);

      var listAllUsersAction = CreateListAllUsersAction();
      var createUserAction = CreateCreateUserAction();
      result.ActionSections = new List<ActionSection> { listAllUsersAction, createUserAction };

      return result;
    }

    private ActionSection CreateListAllUsersAction()
    {
      var result = new ActionSection("List all users", "Retrieve paginated list of users.", HttpRequestMethod.Get);

      var parameters = new List<UriParameter>
      {
        new UriParameter("since", false, "String", "Timestamp in ISO 8601 format: `YYYY-MM-DDTHH:MM:SSZ` Only users updated at or after this time are returned."),
        new UriParameter("limit", false, "Number", "maximum number of records expected by client.")
      };
      var parametersSection = new UriParametersSection(parameters);

      var _200ResponseContent =
@"[
    {
        ""id"": 1,
        ""fname"": ""Pandurang"",
        ""lname"": ""Patil"",
        ""email"": ""pandurang@email.com""
    },
    {
        ""id"": 2,
        ""fname"": ""Sangram"",
        ""lname"": ""Shinde"",
        ""email"": ""sangram@email.com""
    }
]";

      var _401ResponseContent =
@"{
    ""error"": ""error.unauthorized""
}";

      result.ResponseSections = new List<ResponseSection>
      {
        new ResponseSection(200, "application/json") { BodySection = new BodySection(_200ResponseContent) },
        new ResponseSection(401, "application/json") { BodySection = new BodySection(_401ResponseContent) }
      };

      return result;
    }

    private ActionSection CreateCreateUserAction()
    {
      var result = new ActionSection("Create a User", null, HttpRequestMethod.Put);

      var requestContent =
@"{
    ""fname"": ""Ram"",
    ""lname"": ""Jadhav"",
    ""email"": ""ram@email.com""
}";
      var request = new RequestSection(null, "application/json");
      request.BodySection = new BodySection(requestContent);
      result.RequestSections = new List<RequestSection> { request };

      var responseContent =
@"{
    ""id"": 3,
    ""fname"": ""Ram"",
    ""lname"": ""Jadhav"",
    ""email"": ""ram@email.com""
}";
      var response = new ResponseSection(201, "application/json");
      response.BodySection = new BodySection(responseContent);
      result.ResponseSections = new List<ResponseSection> { response };

      return result;
    }

    private ActionSection CreateRetrieveUserAction()
    {
      var _200ResponseContent =
@"{
    ""id"": 1,
    ""fname"": ""Pandurang"",
    ""lname"": ""Patil"",
    ""email"": ""pandurang@email.com""
}";
      var _200Headers = new HeadersSection { { "X-My-Header", "The Value" } };
      var _200Response = new ResponseSection(200, "application/json");
      _200Response.BodySection = new BodySection(_200ResponseContent);
      _200Response.HeadersSection = _200Headers;

      var _401ResponseContent =
@"{
    ""error"": ""error.unauthorized""
}";
      var _401Response = new ResponseSection(401, "application/json");
      _401Response.BodySection = new BodySection(_401ResponseContent);

      var result = new ActionSection("Retrieve a User", null, HttpRequestMethod.Get);
      result.ResponseSections = new[] { _200Response, _401Response };
      return result;
    }

    private ActionSection CreateUpdateUserAction()
    {
      var requestBody =
@"{
    ""id"": 1,
    ""fname"": ""Pandurang"",
    ""lname"": ""Patil"",
    ""email"": ""pandurangpatil@email.com""
}";
      var request = new RequestSection(null, "application/json");
      request.BodySection = new BodySection(requestBody);

      var _200ResponseContent =
@"{
    ""id"": 1,
    ""fname"": ""Pandurang"",
    ""lname"": ""Patil"",
    ""email"": ""pandurangpatil@email.com""
}";
      var _200Response = new ResponseSection(200, "application/json");
      _200Response.BodySection = new BodySection(_200ResponseContent);

      var _401ResponseContent =
@"{
    ""error"": ""error.unauthorized""
}";
      var _401Response = new ResponseSection(401, "application/json");
      _401Response.BodySection = new BodySection(_401ResponseContent);

      var result = new ActionSection("Update a User", "Update user details", HttpRequestMethod.Post);
      result.RequestSections = new[] { request };
      result.ResponseSections = new[] { _200Response, _401Response };
      return result;
    }

    private ActionSection CreateRemoveUserAction()
    {
      var _204Response = new ResponseSection(204);
      var _401ResponseContent =
@"{
    ""error"": ""error.unauthorized""
}";
      var _401Response = new ResponseSection(401, "application/json");
      _401Response.BodySection = new BodySection(_401ResponseContent);

      var result = new ActionSection("Remove a User", null, HttpRequestMethod.Delete);
      result.ResponseSections = new[] { _204Response, _401Response };
      return result;
    }
  }
}