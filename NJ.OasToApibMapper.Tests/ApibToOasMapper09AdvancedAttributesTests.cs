using NJ.ApibModel;

namespace NJ.ApibToOasMapper.Tests
{
  public class ApibToOasMapper09AdvancedAttributesTests
  {
    [Fact]
    public void ApibToOasMapper09AttributesTest()
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
        ParametersSection = new UriParametersSection(new[] {new UriParameter("id", true, "string", "The ID of the desired coupon.") }),
        AttributesSection = new AttributesSection
        {
          TypeDefinition = "object",
          Attributes = new List<AttributeSection>
          {
            new("id", null, true, "string", "250FF"),
            new("created", "Time stamp", false, "number", 1415203908),
            new("percent_off", "A positive integer between 1 and 100 that represents the discount the coupon will apply.", false, "number", 25),
            new("redeem_by", "Date after which the coupon can no longer be redeemed", false, "number")
          }
        }
      };

      var listAllCouponsAction =
        new ActionSection("List all Coupons", "Returns a list of your coupons.", HttpRequestMethod.Get)
        {
          UriParametersSection = new UriParametersSection(new[] {new UriParameter("limit", false, "number", "A limit on the number of objects to be returned. Limit can range\nbetween 1 and 100 items.") { DefaultValue = 10} }),
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
          TypeDefinition = "object",
          Attributes = new List<AttributeSection>
          {
            new("percent_off", null, false, "number", 25),
            new("redeem_by", null, false, "number")
          }
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

      var apib = new Apib();
      apib.MetadataSection = new MetadataSection { { "FORMAT", "1A" } };
      apib.ResourceGroupSections = new[] { resourceGroup };
      apib.ApiNameAndOverviewSection = new ApiNameAndOverviewSection
      {
        Name = "Advanced Attributes API",
        Description = @"Improving the previous [Attributes](08.%20Attributes.md) description example,
this API example describes the `Coupon` resource attributes (data structure)
regardless of the serialization format. These attributes can be later
referenced using the resource name.

These attributes are then reused in the `Retrieve a Coupon` action. Since they
describe the complete message, no explicit JSON body example is needed.

Moving forward, the `Coupon` resource data structure is then reused when
defining the attributes of the coupons collection resource – `Coupons`.

The `Create a Coupon` action also demonstrate the description of request
attributes – once defined, these attributes are implied on every `Create a
Coupon` request unless the request specifies otherwise. Apparently, the
description of action attributes is somewhat duplicate to the definition of
`Coupon` resource attributes. We will address this in the next 
[Data Structures](10.%20Data%20Structures.md) example.

## API Blueprint

+ [Previous: Attributes](08.%20Attributes.md)

+ [This: Raw API Blueprint](https://raw.github.com/apiaryio/api-blueprint/master/examples/09.%20Advanced%20Attributes.md)

+ [Next: Data Structures](10.%20Data%20Structures.md)"
      };

      ApibToOasMapperTestRunner.RunTest(apib, "TestFiles/09. Advanced Attributes.json");
    }
  }
}
