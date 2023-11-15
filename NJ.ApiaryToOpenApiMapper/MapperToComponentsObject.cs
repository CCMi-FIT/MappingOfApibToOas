using NJ.ApiaryAPIModel;
using NJ.ApiaryToOpenApiMapper.Model;
using NJ.OpenAPIModel;

namespace NJ.ApiaryToOpenApiMapper
{
  public static class MapperToComponentsObject
  {
    public static ComponentsObject Map(ApiaryApiBlueprint apb, IReadOnlyCollection<ApiType> apiaryNamedTypes)
    {
      var typeNamesWithSchemas = new Dictionary<string, SchemaObject>();

      var resources = apb.GetAllResourceSections().ToList();
      foreach (var resource in resources)
      {
        var typeName = resource.Identifier;
        // TODO: A bit non-transparent
        if (resource.AttributesSection is null)
          continue;
        var schemaObject = MapTypeNameToSchema(typeName, apiaryNamedTypes);
        if (schemaObject is not null)
          typeNamesWithSchemas.Add(typeName, schemaObject);
      }

      if (apb.DataStructuresSections is not null)
        foreach (var dataStructureSection in apb.DataStructuresSections)
        {
          var typeName = dataStructureSection.Identifier;
          var schemaObject = MapTypeNameToSchema(typeName, apiaryNamedTypes);
          if (schemaObject is not null)
            typeNamesWithSchemas.Add(typeName, schemaObject);
        }

      // TODO: Is this correct?
      //var resourceModels = resources.Select(r => r.ResourceModelSection).Where(r => r is not null);
      //foreach (var resourceModel in resourceModels)
      //{
      //  var typeName = resourceModel.Identifier;
      //  var schemaObject = MapTypeNameToSchema(typeName, apiaryNamedTypes);
      //  if (schemaObject is not null)
      //    typeNamesWithSchemas.Add(typeName, schemaObject);
      //}

      var result = new ComponentsObject { Schemas = typeNamesWithSchemas };
      return result;
    }

    private static SchemaObject MapTypeNameToSchema(string typeName, IReadOnlyCollection<ApiType> apiaryNamedTypes)
    {
      if (typeName is null)
        return null;
      var apiaryType = ApiaryTypesProvider.GetApiaryType(typeName, apiaryNamedTypes);
      var result = MapperToSchemaObject.MapFromApiaryType(apiaryType, "application/json", true);
      return result;
    }
  }
}
