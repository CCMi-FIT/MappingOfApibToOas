using NJ.ApibModel;
using NJ.ApibModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper10DataStructuresTests
  {
    [Fact]
    public void ApibToOasMapper10DataStructuresTest()
    {
      var attributes = new AttributesSection("Coupon");

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

      var retrieveCouponAction = new ActionSection("Retrieve a Coupon", "Retrieves the coupon with the given ID.", HttpRequestMethod.Get, new[] { retrieveResponse });

      var couponResourceDescription = "A coupon contains information about a percent-off or amount-off discount you\r\nmight want to apply to a customer.";
      var couponResource = new ResourceSection("Coupon", couponResourceDescription, new UriTemplate("/coupons/{id}"), new[] { retrieveCouponAction })
      {
        ParametersSection = new UriParametersSection(new[] { new UriParameter("id", true, "string", "The ID of the desired coupon.") }),
        AttributesSection = new AttributesSection("Coupon", "Coupon Base", new AttributeSection[] {
          new("id", "string", null, true, "250FF"),
          new("created", "number", "Time stamp", false, 1415203908)
        })
      };

      var listAllCouponsActionResponse = new ResponseSection(200, "application/json")
      {
        AttributesSection = new AttributesSection("Coupons")
      };
      var listAllCouponsAction =
        new ActionSection("List all Coupons", "Returns a list of your coupons.", HttpRequestMethod.Get, new[] { listAllCouponsActionResponse })
        {
          ParametersSection = new UriParametersSection(new[] { new UriParameter("limit", false, "number", "A limit on the number of objects to be returned. Limit can range\nbetween 1 and 100 items.") { DefaultValue = 10 } })
        };

      var createCouponAction = new ActionSection("Create a Coupon", "Creates a new Coupon.", HttpRequestMethod.Post, new[] { new RequestSection(null, "application/json") }, new[] { new ResponseSection(200, "application/json")
      { AttributesSection = new AttributesSection("Coupon") } })
      {
        AttributesSection = new AttributesSection("Coupon Base")
      };

      var couponsResource = new ResourceSection("Coupons", default(Description?), new UriTemplate("/coupons{?limit}"), new[] { listAllCouponsAction, createCouponAction })
      {
        AttributesSection = new AttributesSection("array[Coupon]")
      };

      var resourceGroup = new ResourceGroupSection("Coupons", default(Description?), new[] { couponResource, couponsResource });

      var couponBaseAttributes = new AttributeSection[] {
          new("percent_off", "number", "A positive integer between 1 and 100 that represents the discount the coupon will apply.", false, 25),
          new("redeem_by", "number", "Date after which the coupon can no longer be redeemed", false)
        };
      var couponBaseAttributesSection = new AttributesSection("Coupon Base", attributes: couponBaseAttributes);
      var dataStructures = new[] { new DataStructuresSection(new[] { couponBaseAttributesSection }) };

      var apiNameAndOverviewDescription = @"Following [Advanced Attributes](09.%20Advanced%20Attributes.md), this example
demonstrates defining arbitrary data structure to be reused by various
attribute descriptions.

Since a portion of the `Coupon` data structure is shared between the `Coupon`
definition itself and the `Create a Coupon` action, it was separated into a
`Coupon Base` data structure in the `Data Structures` API Blueprint Section.
Doing so enables us to reuse it as a base-type of other attribute definitions.

## API Blueprint

+ [Previous: Advanced Attributes](09.%20Advanced%20Attributes.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/10.%20Data%20Structures.md)

+ [Next: Resource Model](11.%20Resource%20Model.md)";
      var apib = new Apib
      {
        MetadataSection = new MetadataSection(("FORMAT", "1A")),
        ResourceGroupSections = new[] { resourceGroup },
        DataStructuresSections = dataStructures,
        ApiNameAndOverviewSection = new ApiNameAndOverviewSection("Data Structures API", apiNameAndOverviewDescription)
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/10. Data Structures - 02.json");
    }
  }
}
