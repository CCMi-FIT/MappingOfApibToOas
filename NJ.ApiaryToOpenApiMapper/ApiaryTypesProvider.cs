using NJ.ApiaryAPIModel;
using NJ.ApiaryToOpenApiMapper.Model;
using System.Text.RegularExpressions;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class ApiaryTypesProvider
  {
    private const string ArrayTypePattern = @"^array\[(.*)\]$";

    #region Public Methods

    public static IReadOnlyCollection<ApiType> GetNamedTypes(ApiaryApiBlueprint apb)
    {
      var resultInternal = GetNamedTypesInternal(apb);
      var result = ConvertNamedTypesInternal(resultInternal);
      return result;
    }

    public static ApiType GetApiaryType(string typeName, IReadOnlyCollection<ApiType> namedTypes)
    {
      var coercedTypeName = typeName ?? "object";
      var arrayMatch = Regex.Match(coercedTypeName, ArrayTypePattern);
      ApiType result;
      if (arrayMatch.Success)
      {
        var itemTypeName = arrayMatch.Groups[1].ToString();
        var itemType = GetApiaryType(itemTypeName, namedTypes);
        result = new ApiArrayType(coercedTypeName, itemType);
      }
      else
        result = namedTypes.First(t => t.Name == coercedTypeName);
      return result;
    }

    public static ApiType GetApiaryType(AttributesSection attributesSection, IReadOnlyCollection<ApiType> namedTypes)
    {
      var apiaryNamedType = GetApiaryType(attributesSection.TypeDefinition, namedTypes);
      if (apiaryNamedType is ApiArrayType)
        return apiaryNamedType;

      if (apiaryNamedType is ApiPropertyType)
      {
        var attributes = attributesSection.Attributes;
        if (attributes is null || attributes.Count == 0)
          return apiaryNamedType;
        var properties = attributes.Select(a => new ApiTypeProperty(a.TypeName, a.Name, a.Required, a.SampleValue, a.Default, a.Description));
        var result = new ApiPropertyType(null, properties, apiaryNamedType);
        return result;
      }

      throw new NotSupportedException();
    }

    #endregion Public Methods

    #region Other Methods

    private static IReadOnlyCollection<ApiTypeInternal> GetNamedTypesInternal(ApiaryApiBlueprint apb)
    {
      var resources = apb.GetAllResourceSections();
      // TODO: Is this syntax for LINQ more appropriate?
      var resourceTypes = from r in resources let typeName = r.Identifier let attributesSection = r.AttributesSection where typeName is not null && attributesSection is not null select GetApiaryTypeInternal(attributesSection, typeName);

      var dataStructures = apb.DataStructuresSections;
      IEnumerable<ApiTypeInternal> dataStructureTypes;
      if (dataStructures is null)
        dataStructureTypes = Array.Empty<ApiTypeInternal>();
      else
        dataStructureTypes = dataStructures.Select(GetApiaryTypeInternal);

      var objectType = new ApiTypeInternal("object");
      IEnumerable<ApiTypeInternal> otherTypes = new[] { objectType };

      //var resourceModels = resources.Select(r => r.ResourceModelSection).Where(r => r is not null);
      //foreach (var resourceModel in resourceModels)
      //{
      //  var typeName = resourceModel.Identifier;
      //  var bodySection = resourceModel.BodySection;
      //  if (bodySection is null)
      //    continue;
      //  var content = bodySection.Content;
      //  if (content is null)
      //    continue;
      //  // TODO: What about Attributes?
      //  // TODO: Convert content over schema to ApiaryType ?
      //  //var typeInternal = new ApiaryTypeInternal(typeName, )
      //}

      IReadOnlyCollection<ApiTypeInternal> result = resourceTypes.Concat(dataStructureTypes).Concat(otherTypes).ToList();
      return result;
    }

    private static ApiTypeInternal GetApiaryTypeInternal(DataStructureSection dataStructure)
    {
      var properties = GetProperties(dataStructure.Attributes);
      var result = new ApiTypeInternal(dataStructure.Identifier, properties, dataStructure.TypeDefinition);
      return result;
    }

    private static ApiTypeInternal GetApiaryTypeInternal(AttributesSection attributesSection, string typeName)
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
            // TODO Internally mutable Properties?
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
