namespace NJ.ApibModel.AdditionalDomainObjects
{
  public class ActionTransaction
  {
    public IReadOnlyCollection<RequestSection> Requests { get; }
    public IReadOnlyCollection<ResponseSection> Responses { get; }

    // TODO: Request and Response nested section within one transaction example should have different identifiers
    public ActionTransaction(IEnumerable<RequestSection>? requests, IEnumerable<ResponseSection> responses)
    {
      Requests = requests?.ToList() ?? new List<RequestSection>();
      var responsesList = responses.ToList();
      if (responsesList.Count == 0)
        throw new ArgumentException($"{nameof(responses)} must not be empty");
      Responses = responsesList;
    }
  }
}
