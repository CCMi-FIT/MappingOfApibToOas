using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class ApiTypesProvider
  {
    private const string ArrayTypePattern = @"^array\[(.*)\]$";

    #region Public Methods

    public static IReadOnlyCollection<ApiType> GetNamedApiTypes(Apib apib)
    {
      var resultInternal = GetNamedTypesInternal(apib);
      var result = ConvertNamedTypesInternal(resultInternal);
      return result;
    }

    public static ApiType GetApiType(string typeName, IReadOnlyCollection<ApiType> namedTypes)
    {
      var coercedTypeName = typeName ?? "object";
      var arrayMatch = Regex.Match(coercedTypeName, ArrayTypePattern);
      ApiType result;
      if (arrayMatch.Success)
      {
        var itemTypeName = arrayMatch.Groups[1].ToString();
        var itemType = GetApiType(itemTypeName, namedTypes);
        result = new ApiArrayType(coercedTypeName, itemType);
      }
      else
        result = namedTypes.First(t => t.Name == coercedTypeName);
      return result;
    }

    public static ApiType GetApiType(AttributesSection attributesSection, IReadOnlyCollection<ApiType> namedTypes)
    {
      var apiNamedType = GetApiType(attributesSection.TypeDefinition, namedTypes);
      if (apiNamedType is ApiArrayType)
        return apiNamedType;

      if (apiNamedType is ApiPropertyType)
      {
        var attributes = attributesSection.Attributes;
        if (attributes is null || attributes.Count == 0)
          return apiNamedType;
        var properties = attributes.Select(a => new ApiTypeProperty(a.TypeName, a.Name, a.Required, a.SampleValue, a.Default, a.Description));
        var result = new ApiPropertyType(null, properties, apiNamedType);
        return result;
      }

      throw new NotSupportedException();
    }

    public static ApiType GetApiType(AttributesSection attributesSection, SchemaSection schemaSection, IReadOnlyCollection<ApiType> namedTypes)
    {
      var apiNamedType = GetApiType(attributesSection.TypeDefinition, namedTypes);
      if (apiNamedType is ApiArrayType)
        return apiNamedType;

      if (apiNamedType is ApiPropertyType)
      {
        var attributes = attributesSection.Attributes;
        if (attributes is null || attributes.Count == 0)
          return apiNamedType;
        var properties = attributes.Select(a => new ApiTypeProperty(GetTypeName(a, schemaSection), a.Name, a.Required, a.SampleValue, a.Default, a.Description));
        var result = new ApiPropertyType(null, properties, apiNamedType);
        return result;
      }

      throw new NotSupportedException();
    }

    #endregion Public Methods

    #region Other Methods

    private static string GetTypeName(AttributeSection attributeSection, SchemaSection schemaSection)
    {
      var result = attributeSection.TypeName;
      if (result is not null)
        return result;
      var schemaJObject = JObject.Parse(schemaSection.Schema);
      result = schemaJObject["properties"][attributeSection.Name]["type"].ToString();
      return result;
    }

    private static IReadOnlyCollection<ApiTypeInternal> GetNamedTypesInternal(Apib apib)
    {
      var resources = apib.GetAllResourceSections();
      var resourceTypes = from r in resources let typeName = r.Identifier let attributesSection = r.AttributesSection where typeName is not null && attributesSection is not null select GetApiTypeInternal(attributesSection, typeName);

      var dataStructures = apib.DataStructuresSections;
      IEnumerable<ApiTypeInternal> dataStructureTypes;
      if (dataStructures is null)
        dataStructureTypes = Array.Empty<ApiTypeInternal>();
      else
        dataStructureTypes = dataStructures.Select(GetApiTypeInternal);

      var objectType = new ApiTypeInternal("object");
      IEnumerable<ApiTypeInternal> otherTypes = new[] { objectType };

      IReadOnlyCollection<ApiTypeInternal> result = resourceTypes.Concat(dataStructureTypes).Concat(otherTypes).ToList();
      return result;
    }

    private static ApiTypeInternal GetApiTypeInternal(DataStructureSection dataStructure)
    {
      var properties = GetProperties(dataStructure.Attributes);
      var result = new ApiTypeInternal(dataStructure.Identifier, properties, dataStructure.TypeDefinition);
      return result;
    }

    private static ApiTypeInternal GetApiTypeInternal(AttributesSection attributesSection, string typeName)
    {
      var properties = GetProperties(attributesSection.Attributes);
      var result = new ApiTypeInternal(typeName, properties, attributesSection.TypeDefinition);
      return result;
    }

    private static IReadOnlyCollection<ApiTypeProperty> GetProperties(ICollection<AttributeSection> attributeSections)
    {
      IReadOnlyCollection<ApiTypeProperty> result;
      if (attributeSections is null)
        result = Array.Empty<ApiTypeProperty>();
      else
        result = attributeSections.Select(a => new ApiTypeProperty(a.TypeName, a.Name, a.Required, a.SampleValue, a.Default, a.Description)).ToList();
      return result;
    }

    private static IReadOnlyCollection<ApiType> ConvertNamedTypesInternal(IReadOnlyCollection<ApiTypeInternal> namedTypesInternal)
    {
      var result = new List<ApiType>();
      foreach (var namedTypeInternal in namedTypesInternal.ToList())
        GetOrUpdateNamedType(namedTypeInternal.Name, namedTypesInternal, result);

      return result;
    }
    private static ApiType GetOrUpdateNamedType(string typeName, IReadOnlyCollection<ApiTypeInternal> typesInternal, ICollection<ApiType> namedTypes)
    {
      var result = namedTypes.FirstOrDefault(t => t.Name == typeName);
      if (result is not null)
        return result;

      var arrayMatch = Regex.Match(typeName, ArrayTypePattern);
      if (arrayMatch.Success)
      {
        var itemTypeName = arrayMatch.Groups[1].Value;
        var itemType = GetOrUpdateNamedType(itemTypeName, typesInternal, namedTypes);
        result = new ApiArrayType(typeName, itemType);
      }
      else
      {
        var typeInternal = typesInternal.First(t => t.Name == typeName);
        var parentTypeName = typeInternal.ParentName;

        if (parentTypeName is not null && parentTypeName != "object")
        {
          var parentType = GetOrUpdateNamedType(parentTypeName, typesInternal, namedTypes);
          if (parentType is ApiArrayType parentArrayType)
            result = new ApiArrayType(typeName, parentArrayType.ItemType);
          else if (parentType is ApiPropertyType)
            result = new ApiPropertyType(typeName, typeInternal.Properties, parentType);
          else
            throw new NotSupportedException();
        }
        else
          result = new ApiPropertyType(typeName, typeInternal.Properties);
      }

      namedTypes.Add(result);
      return result;
    }

    #endregion Other Methods
  }
}
