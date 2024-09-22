using NJ.OasModel.AdditionalDomainObjects;
using NJ.Common.Extensions;

namespace NJ.OasModel;

public class OpenApiObject
{
  public OpenApiVersion OpenApi { get; init; }
  public InfoObject Info { get; init; }
  public Uri? JsonSchemaDialect { get; init; }
  
  private readonly static ServerObject _defaultServerObject = new ServerObject("/");
  private readonly static IReadOnlyCollection<ServerObject> _defaultServerObjects = new List<ServerObject> { _defaultServerObject };

  private IReadOnlyCollection<ServerObject>? _servers;
  public IReadOnlyCollection<ServerObject>? Servers
  {
    get => _servers.IsNotNullOrEmpty() ? _servers : _defaultServerObjects;
    init => _servers = value;
  }
  public PathsObject? Paths { get; init; }
  public IReadOnlyDictionary<string, IPathItemOrReferenceObject>? WebHooks { get; init; }
  public ComponentsObject? Components { get; init; }

  // TODO: What should allow object look like ?
  private static readonly SecurityRequirementObject _defaultAllowSecurityRequirementObject = new SecurityRequirementObject { };
  public static readonly IReadOnlyCollection<SecurityRequirementObject> _defaultAllowSecurityRequirementObjects = new List<SecurityRequirementObject> { _defaultAllowSecurityRequirementObject };

  private IReadOnlyCollection<SecurityRequirementObject>? _security;
  public IReadOnlyCollection<SecurityRequirementObject>? Security
  {
    get => _security.IsNotNullOrEmpty() ? _security : _defaultAllowSecurityRequirementObjects;
    init => _security = value;
  }
  public IReadOnlyCollection<TagObject>? Tags { get; init; }
  public ExternalDocumentationObject? ExternalDocs { get; init; }
  public SpecificationExtensions? SpecificationExtensions { get; init; }
}