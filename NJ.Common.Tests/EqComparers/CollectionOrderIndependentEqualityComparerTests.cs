using NJ.Common.EqComparers;
using NJ.TestCommon;
using NJ.TestCommon.EqComparers;
using System.Collections.Generic;
using Xunit;

namespace NJ.Common.Tests.EqComparers
{
  public class CollectionOrderIndependentEqualityComparerTests
  {
    [Fact]
    public void CollectionOrderIndependentEqualityComparerTest()
    {
      var itemEqualityComparer = new MockedItemComparer();
      IEqualityComparer<ICollection<object?>?> collectionComparer = new CollectionOrderIndependentEqualityComparer<object?>(itemEqualityComparer);

      var testConfigs = new List<TestConfiguration<ICollection<object?>?>>
      {
        CreateTestConfig(null, null, true),
        CreateTestConfig(null, new List<object?>(), false),
        CreateTestConfig(new List<object?>(), null, false),
        CreateTestConfig(new List<object?>(), new List<object?>(), true),
        CreateTestConfig(new List<object?> { null }, new List<object?> { null }, true),
        CreateTestConfig(new List<object?> { null }, new List<object?> { new object() }, false),
        CreateTestConfig
        (
          new List<object?> { null, null, new object(), new object(), new object() },
          new List<object?> { new object(), null, new object(), null, new object() },
          true
        )
      };

      var testRunner = new TestRunner<ICollection<object?>?>(collectionComparer);
      testRunner.RunTestsAndAssert(testConfigs);
    }

    private TestConfiguration<ICollection<object?>?> CreateTestConfig(ICollection<object?>? x, ICollection<object?>? y, bool expectedResult)
    {
      var result = new TestConfiguration<ICollection<object?>?>(x, y, expectedResult);
      return result;
    }

    public class MockedItemComparer : IEqualityComparer<object?>
    {
      public new bool Equals(object? x, object? y) => x is null == y is null;
      public int GetHashCode(object? obj) => obj is null ? 0 : 1;
    }
  }
}
