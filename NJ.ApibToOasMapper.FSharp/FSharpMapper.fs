namespace NJ.ApibToOasMapper.FSharp

open System.Collections.Generic
open NJ.ApibModel
open NJ.ApibToOasMapper
open NJ.OasModel
open NJ.ApibToOasMapper.Model

module Mapper =
    let MapToServerObjects(apib : Apib) =
        let metadataDictionary = apib.MetadataSection.KeysWithValues;
        if metadataDictionary = null then
        []
        else
        let found, value = metadataDictionary.TryGetValue("HOST")
        if not found then
            [];
        else
            [ServerObject(value)];

    let MapApibToOpenApiObject (apib : Apib) =
        let apiNamedTypes = ApiTypesProvider.GetNamedApiTypes(apib);
        new OpenApiObject(
            OpenApi = "3.0.3",
            Servers = ResizeArray<ServerObject>(MapToServerObjects(apib)),
            Info = ApiNameAndOverviewSectionMapper.Map(apib.ApiNameAndOverviewSection),
            Paths = ResourceMapper.MapResources(apib, apiNamedTypes),
            Tags = ResourceGroupSectionToTagsMapper.MapToTagObjects(apib.ResourceGroupSections),
            Components = MapperToComponentsObject.Map(apib, apiNamedTypes)
        )

    let MapResources (apib : Apib, apiNamedTypes : IReadOnlyCollection<ApiType>) =
        new PathsObject(PathItems = ResourceMapper.MapResourcesToDictionary(apib, apiNamedTypes))

    //let MapResourcesToDictionary(apib : Apib, apiNamedTypes : IReadOnlyCollection<ApiType>) =
    //    let query = query {
    //        for g in apib.ResourceGroupSections do
    //    }
    //    let resourcesWithExistingParentSections = apib.ResourceGroupSections.