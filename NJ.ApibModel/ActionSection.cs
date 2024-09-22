using NJ.ApibModel.AdditionalDomainObjects;
using NJ.SharedModel;

namespace NJ.ApibModel;

public class ActionSection : NamedSection
{
  public override string Keyword { get; } = "";
  public HttpRequestMethod HttpRequestMethod { get; }

  public UriTemplate? UriTemplate { get; init; }
  public RelationSection? RelationSection { get; init; }
  public UriParametersSection? ParametersSection { get; init; }
  public AttributesSection? AttributesSection { get; init; }

  public IEnumerable<RequestSection> RequestSections => Transactions.SelectMany(t => t.Requests);
  public IEnumerable<ResponseSection> ResponseSections => Transactions.SelectMany(t => t.Responses);
  public IReadOnlyCollection<ActionTransaction> Transactions { get; }

  public ActionSection(HttpRequestMethod httpRequestMethod, IEnumerable<ActionTransaction> transactions) : this(default, default(Description), httpRequestMethod, transactions)
  {
  }

  public ActionSection(string? identifier, HttpRequestMethod httpRequestMethod, IEnumerable<ActionTransaction> transactions) : this(identifier, default(Description), httpRequestMethod, transactions)
  {
  }

  public ActionSection(string? identifier, string? description, HttpRequestMethod httpRequestMethod, IEnumerable<ActionTransaction> transactions) : this(identifier, description is not null ? new PlainTextDescription(description) : default, httpRequestMethod, transactions)
  {
  }

  public ActionSection(string? identifier, string? description, HttpRequestMethod httpRequestMethod, IEnumerable<ResponseSection> responseSections) : this(identifier, description is not null ? new PlainTextDescription(description) : default, httpRequestMethod, new[] { new ActionTransaction(default, responseSections) })
  {
  }

  public ActionSection(string? identifier, string? description, HttpRequestMethod httpRequestMethod, IEnumerable<RequestSection> requestSections, IEnumerable<ResponseSection> responseSections) : this(identifier, description is not null ? new PlainTextDescription(description) : default, httpRequestMethod, new[] { new ActionTransaction(requestSections, responseSections) })
  {
  }

  public ActionSection(string? identifier, Description? description, HttpRequestMethod httpRequestMethod, IEnumerable<ActionTransaction> transactions)
  {
    Identifier = identifier;
    Description = description;
    HttpRequestMethod = httpRequestMethod;
    var transactionsList = transactions.ToList();
    if (transactionsList.Count == 0)
      throw new ArgumentException($"{nameof(transactionsList)} must not be empty");
    Transactions = transactionsList;
  }
}