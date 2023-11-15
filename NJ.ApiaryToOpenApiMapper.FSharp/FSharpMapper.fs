namespace NJ.ApiaryToOpenApiMapper.FSharp

open System.Collections.Generic
open NJ.ApiaryAPIModel
open NJ.ApiaryToOpenApiMapper
open NJ.OpenAPIModel
open NJ.ApiaryToOpenApiMapper.Model

module Mapper =
    let MapToServerObjects(apiaryApiBlueprint : ApiaryApiBlueprint) =
        let metadataDictionary = apiaryApiBlueprint.MetadataSection.KeysWithValues;
        if metadataDictionary = null then
        []
        else
        let found, value = metadataDictionary.TryGetValue("HOST")
        if not found then
            [];
        else
            [ServerObject(value)];

    let MapApiaryApiBlueprintToOpenApiObject (apb : ApiaryApiBlueprint) =
        let apiaryNamedTypes = ApiaryTypesProvider.GetNamedTypes(apb);
        new NJ.OpenAPIModel.OpenApiObject(
            OpenApi = "3.0.3",
            Servers = ResizeArray<ServerObject>(MapToServerObjects(apb)),
            Info = ApiNameAndOverviewSectionMapper.Map(apb.ApiNameAndOverviewSection),
            Paths = ResourceMapper.MapResources(apb, apiaryNamedTypes),
            Tags = ResourceGroupSectionToTagsMapper.MapToTagObjects(apb.ResourceGroupSections),
            Components = MapperToComponentsObject.Map(apb, apiaryNamedTypes)
        )

    let MapResources (apb : ApiaryApiBlueprint, apiaryNamedTypes : IReadOnlyCollection<ApiType>) =
        new NJ.OpenAPIModel.PathsObject(PathItems = ResourceMapper.MapResourcesToDictionary(apb, apiaryNamedTypes))

    //let MapResourcesToDictionary(apb : ApiaryApiBlueprint, apiaryNamedTypes : IReadOnlyCollection<ApiaryType>) =
    //    let query = query {
    //        for g in apb.ResourceGroupSections do
    //    }
    //    let resourcesWithExistingParentSections = apb.ResourceGroupSections.