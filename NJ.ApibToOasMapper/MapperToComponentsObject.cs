﻿using NJ.ApibModel;
using NJ.ApibToOasMapper.Model;
using NJ.OasModel;

namespace NJ.ApibToOasMapper
{
  public static class MapperToComponentsObject
  {
    public static ComponentsObject Map(Apib apib, IReadOnlyCollection<ApiType> apiNamedTypes)
    {
      var typeNamesWithSchemas = new Dictionary<string, SchemaObject>();

      var resources = apib.GetAllResourceSections().ToList();
      foreach (var resource in resources)
      {
        var typeName = resource.Identifier;
        if (resource.AttributesSection is null)
          continue;
        var schemaObject = MapTypeNameToSchema(typeName, apiNamedTypes);
        if (schemaObject is not null)
          typeNamesWithSchemas.Add(typeName, schemaObject);
      }

      if (apib.DataStructuresSections is not null)
        foreach (var dataStructureSection in apib.DataStructuresSections)
        {
          var typeName = dataStructureSection.Identifier;
          var schemaObject = MapTypeNameToSchema(typeName, apiNamedTypes);
          if (schemaObject is not null)
            typeNamesWithSchemas.Add(typeName, schemaObject);
        }

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
