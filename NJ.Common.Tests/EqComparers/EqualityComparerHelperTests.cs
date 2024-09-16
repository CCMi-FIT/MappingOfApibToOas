using System.Collections.Generic;
using NJ.Common.EqComparers;
using Xunit;

namespace NJ.Common.Tests.EqComparers
{
  public class EqualityComparerHelperTests
  {
    [Fact]
    public void EqualityComparerHelperTryGetComparisonResultByNullsTest()
    {
      var testConfigs = new List<(object? X, object? Y, bool ExpectedResult, bool? ExpectedComparison)>
      {
        (null, null, true, true),
        (null, new object(), true, false),
        (new object(), null, true, false),
        (new object(), new object(), false, null)
      };
      foreach (var tc in testConfigs)
      {
        var result = EqualityComparerHelper.TryGetComparisonResultByNulls(tc.X, tc.Y, out var expectedComparison);
        var resultsEqual = tc.ExpectedResult == result;
        Assert.True(resultsEqual);
        var comparisonResultsEqual = tc.ExpectedComparison == expectedComparison;
        Assert.True(comparisonResultsEqual);
      }
    }
  }
}
