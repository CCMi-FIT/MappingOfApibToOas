using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper10DataStructuresTests
  {
    [Fact]
    public void ApibToOasMapper10DataStructuresTest()
    {
      var attributes = new AttributesSection
      {
        TypeDefinition = "Coupon"
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
        ActionSections = new[] { retrieveCouponAction },
        ParametersSection = new UriParametersSection(new[] { new UriParameter("id", true, "string", "The ID of the desired coupon.") }),
        AttributesSection = new AttributesSection
        {
          TypeDefinition = "Coupon Base",
          Attributes = new List<AttributeSection>
          {
            new("id", null, true, "string", "250FF"),
            new("created", "Time stamp", false, "number", 1415203908)
          }
        }
      };

      var listAllCouponsAction =
        new ActionSection("List all Coupons", "Returns a list of your coupons.", HttpRequestMethod.Get)
        {
          UriParametersSection = new UriParametersSection(new[] { new UriParameter("limit", false, "number", "A limit on the number of objects to be returned. Limit can range\nbetween 1 and 100 items.") { DefaultValue = 10 } }),
          ResponseSections = new List<ResponseSection>
          {
            new ResponseSection(200, "application/json")
            {
              AttributesSection = new AttributesSection { TypeDefinition = "Coupons" }
            }
          }
        };

      var createCouponAction = new ActionSection("Create a Coupon", "Creates a new Coupon.", HttpRequestMethod.Post)
      {
        AttributesSection = new AttributesSection
        {
          TypeDefinition = "Coupon Base"
        },
        RequestSections = new List<RequestSection> { new RequestSection(null, "application/json") },
        ResponseSections = new List<ResponseSection>
        {
          new ResponseSection(200, "application/json")
          {
            AttributesSection = new AttributesSection { TypeDefinition = "Coupon" }
          }
        }
      };

      var couponsResource = new ResourceSection("Coupons", new UriTemplate("/coupons{?limit}"))
      {
        AttributesSection = new AttributesSection
        {
          TypeDefinition = "array[Coupon]"
        },
        ActionSections = new[] { listAllCouponsAction, createCouponAction }
      };

      var resourceGroup = new ResourceGroupSection("Coupons")
      {
        ResourceSections = new[] { couponResource, couponsResource }
      };

      var dataStructures = new List<DataStructureSection> {
        new() {
          Identifier = "Coupon Base",
          TypeDefinition = "object",
          Attributes = new List<AttributeSection> {
            new("percent_off", "A positive integer between 1 and 100 that represents the discount the coupon will apply.", false, "number", 25),
            new("redeem_by", "Date after which the coupon can no longer be redeemed", false, "number")
          }
        }
      };

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceGroupSections = new[] { resourceGroup };
      apib.DataStructuresSections = dataStructures;
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Data Structures API",
        Description = @"Following [Advanced Attributes](09.%20Advanced%20Attributes.md), this example
demonstrates defining arbitrary data structure to be reused by various
attribute descriptions.

Since a portion of the `Coupon` data structure is shared between the `Coupon`
definition itself and the `Create a Coupon` action, it was separated into a
`Coupon Base` data structure in the `Data Structures` API Blueprint Section.
Doing so enables us to reuse it as a base-type of other attribute definitions.

## API Blueprint

+ [Previous: Advanced Attributes](09.%20Advanced%20Attributes.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/10.%20Data%20Structures.md)

+ [Next: Resource Model](11.%20Resource%20Model.md)"
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/10. Data Structures - 02.json");
    }
  }
}
