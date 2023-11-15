using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class MapperToComponentsObject
  {
    public static ComponentsObject Map(Apib apb, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var typeNamesWithSchemas = new Dictionary<string, SchemaObject>();

      var resources = apb.GetAllResourceSections().ToList();
      foreach (var resource in resources)
      {
        var typeName = resource.Identifier;
        // TODO: A bit non-transparent
        if (resource.AttributesSection is null)
          continue;
        var schemaObject = MapTypeNameToSchema(typeName, apiNamedTypes);
        if (schemaObject is not null)
          typeNamesWithSchemas.Add(typeName, schemaObject);
      }

      if (apb.DataStructuresSections is not null)
        foreach (var dataStructureSection in apb.DataStructuresSections)
        {
          var typeName = dataStructureSection.Identifier;
          var schemaObject = MapTypeNameToSchema(typeName, apiNamedTypes);
          if (schemaObject is not null)
            typeNamesWithSchemas.Add(typeName, schemaObject);
        }

      // TODO: Is this correct?
      //var resourceModels = resources.Select(r => r.ResourceModelSection).Where(r => r is not null);
      //foreach (var resourceModel in resourceModels)
      //{
      //  var typeName = resourceModel.Identifier;
      //  var schemaObject = MapTypeNameToSchema(typeName, apiNamedTypes);
      //  if (schemaObject is not null)
      //    typeNamesWithSchemas.Add(typeName, schemaObject);
      //}

      var result = new ComponentsObject { Schemas = typeNamesWithSchemas };
      return result;
    }

    private static SchemaObject MapTypeNameToSchema(string typeName, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      if (typeName is null)
        return null;
      var type = ApiTypesProvider.GetApiType(typeName, apiNamedTypes);
      var result = MapperToSchemaObject.MapFromApiType(type, "application/json", true);
      return result;
    }
  }
}
