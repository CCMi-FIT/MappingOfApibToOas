using NJ.ApiaryAPIModel;

namespace NJ.ApiaryToOpenApiMapper.Tests
{
  public class ApiaryToOpenApi08AttributesTests
  {
    [Fact]
    public void ApiaryToOpenApi08AttributesTest()
    {
      var attributes = new AttributesSection
      {
        TypeDefinition = "object",
        Attributes = new List<AttributeSection>
        {
          new("id", null, true, "string", "250FF"),
          new("created", "Time stamp", false, "number", 1415203908),
          new("percent_off", "A positive integer between 1 and 100 that represents the discount\nthe coupon will apply.", false, "number", 25),
          new("redeem_by", "Date after which the coupon can no longer be redeemed", false, "number")
        }
      };

      var retrieveResponse = new ResponseSection(200, "application/json")
      {
        AttributesSection = attributes,
        BodySection = new BodySection(@"{
    ""id"": ""250FF"",
    ""created"": 1415203908,
    ""percent_off"": 25,
    ""redeem_by"": null
}")
      };

      var retrieveCouponAction = new ActionSection("Retrieve a Coupon", "Retrieves the coupon with the given ID.", HttpRequestMethod.Get)
      {
        ResponseSections = new[] { retrieveResponse }
      };

      var couponResource = new ResourceSection("Coupon", new UriTemplate("/coupons/{id}"))
      {
        Description =
          "A coupon contains information about a percent-off or amount-off discount you\r\nmight want to apply to a customer.",
        ActionSections = new[] { retrieveCouponAction }
      };

      var resourceGroup = new ResourceGroupSection("Coupons")
      {
        ResourceSections = new[] { couponResource }
      };

      var apb = new ApiaryApiBlueprint();
      apb.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apb.ResourceGroupSections = new[] { resourceGroup };
      apb.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Attributes API",
        Description = @"This API example demonstrates how to describe body attributes of a request or
response message.

In this case, the description is complementary (and duplicate!) to the provided
JSON example in the body section. The 
[Advanced Attributes](09.%20Advanced%20Attributes.md) API example will
demonstrate how to avoid duplicates and how to reuse attribute descriptions.

## API Blueprint

+ [Previous: Parameters](07.%20Parameters.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/08.%20Attributes.md)

+ [Next: Advanced Attributes](09.%20Advanced%20Attributes.md)"
      };

      ApiaryToOpenApiTestRunner.RunTest(apb, "TestFiles/08. Attributes - 02.json");
    }
  }
}
