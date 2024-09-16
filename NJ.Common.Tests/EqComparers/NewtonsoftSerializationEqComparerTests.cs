using System.Collections.Generic;
using NJ.Common.EqComparers;
using Xunit;

namespace NJ.Common.Tests.EqComparers
{
  public class NewtonsoftSerializationEqComparerTests
  {
    private readonly IEqualityComparer<object> eqComparer = new NewtonsoftSerializationEqComparer();

    [Fact]
    public void NewtonsoftSerializationEqComparerTest()
    {
      ReferencesEqualTest();
      CrossReferenceEqualTest();
      CrossReferenceNotEqualTest();
    }

    private void ReferencesEqualTest()
    {
      var testObject = new object();
      var testConfigs = new List<(object? X, object? Y, bool ExpectedResult)>
      {
        (null, null, true),
        (testObject, testObject, true),
        ("x", null, false),
        (null, "x", false)
      };

      foreach (var tc in testConfigs)
      {
        var result = eqComparer.Equals(tc.X, tc.Y);
        var equal = tc.ExpectedResult == result;
        Assert.True(equal);
      }
    }

    private void CrossReferenceEqualTest()
    {
      var x = new CrossReferenceClass();
      var y = new CrossReferenceClass();

      x.Next = y;
      x.Previous = y;
      y.Next = x;
      y.Previous = x;

      var equal = eqComparer.Equals(x, y);
      Assert.True(equal);
    }

    private void CrossReferenceNotEqualTest()
    {
      var x = new CrossReferenceClass { Value = "x" };
      var y = new CrossReferenceClass();

      x.Next = y;
      x.Previous = y;
      y.Next = x;
      y.Previous = x;

      var equal = eqComparer.Equals(x, y);
      Assert.False(equal);
    }

    private class CrossReferenceClass
    {
      public CrossReferenceClass? Next { get; set; }
      public CrossReferenceClass? Previous { get; set; }
      public object? Value { get; set; }
    }
  }
}
