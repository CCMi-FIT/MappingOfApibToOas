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

  public ActionSection(HttpRequestMethod httpRequestMethod, IEnumerable<ActionTransaction> transactions)
  {
    HttpRequestMethod = httpRequestMethod;
    var transactionsList = transactions.ToList();
    if (transactionsList.Count == 0)
      throw new ArgumentException($"{nameof(transactionsList)} must not be empty");
    Transactions = transactionsList;
  }
}