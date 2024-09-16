using NJ.Common.EqComparers;
using NJ.TestCommon;
using NJ.TestCommon.EqComparers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NJ.Common.Tests.EqComparers
{
  public class SuperTypeEqualityComparerTests
  {
    [Fact]
    public void NoDerivedComparerTest()
    {
      var testConfigs = new List<TestConfiguration<object?>>
      {
        new TestConfiguration<object?>(null, null, true),
        new TestConfiguration<object?>(null, "", typeof(NotSupportedException)),
        new TestConfiguration<object?>(null, new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(null, 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>("", "", typeof(NotSupportedException))
      };

      var eqComparer = new SuperTypeEqualityComparer<object?>();
      var testRunner = new TestRunner<object?>(eqComparer);
      testRunner.RunTestsAndAssert(testConfigs);
    }

    [Fact]
    public void OneDerivedComparerTest()
    {
      var testConfigs = new List<TestConfiguration<object?>>
      {
        new TestConfiguration<object?>(null, null, true),
        new TestConfiguration<object?>(null, "", false),
        new TestConfiguration<object?>(null, new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(null, 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>("", null, false),
        new TestConfiguration<object?>("", "", true),
        new TestConfiguration<object?>("", new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>("", 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), null, typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), "", typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, null, typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, "", typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, 0, typeof(NotSupportedException))
      };

      var stringComparer = new MockedDerivedStringEqualityComparer();
      var eqComparer = new SuperTypeEqualityComparer<object?>(stringComparer);
      var testRunner = new TestRunner<object?>(eqComparer);
      testRunner.RunTestsAndAssert(testConfigs);
    }

    [Fact]
    public void TwoSameDerivedComparersTest()
    {
      var testConfigs = new List<TestConfiguration<object?>>
      {
        new TestConfiguration<object?>(null, null, true),
        new TestConfiguration<object?>(null, "", typeof(NotSupportedException)),
        new TestConfiguration<object?>(null, new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(null, 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>("", "", typeof(NotSupportedException))
      };

      var stringComparer = new MockedDerivedStringEqualityComparer();
      var eqComparer = new SuperTypeEqualityComparer<object?>(stringComparer, stringComparer);
      var testRunner = new TestRunner<object?>(eqComparer);
      testRunner.RunTestsAndAssert(testConfigs);
    }

    [Fact]
    public void TwoDifferentComparersTest()
    {
      var testConfigs = new List<TestConfiguration<object?>>
      {
        new TestConfiguration<object?>(null, null, true),
        new TestConfiguration<object?>(null, "", false),
        new TestConfiguration<object?>(null, new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(null, 0, false),
        new TestConfiguration<object?>("", null, false),
        new TestConfiguration<object?>("", "", true),
        new TestConfiguration<object?>("", new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>("", 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), null, typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), "", typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(new object(), 0, typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, null, false),
        new TestConfiguration<object?>(0, "", typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, new object(), typeof(NotSupportedException)),
        new TestConfiguration<object?>(0, 0, true)
      };

      var stringComparer = new MockedDerivedStringEqualityComparer();
      var intComparer = new MockedDerivedIntEqualityComparer();
      var eqComparer = new SuperTypeEqualityComparer<object?>(stringComparer, intComparer);
      var testRunner = new TestRunner<object?>(eqComparer);
      testRunner.RunTestsAndAssert(testConfigs);
    }

    private class MockedDerivedStringEqualityComparer : IDerivedTypeEqualityComparer<object?>
    {
      public bool CanCastToDerivedType(params object?[] parameters) => parameters.All(p => p is string || p is null);
      public bool CastToDerivedTypeAndCheckEquality(object? x, object? y) => (string?)x == (string?)y;
      public int CastToDerivedTypeAndGetHashCode(object? obj) => ((string?)obj)?.GetHashCode() ?? 123;
    }

    private class MockedDerivedIntEqualityComparer : IDerivedTypeEqualityComparer<object?>
    {
      public bool CanCastToDerivedType(params object?[] parameters) => parameters.All(p => p is int);
      public bool CastToDerivedTypeAndCheckEquality(object? x, object? y) => ((int)x!) == ((int)y!);
      public int CastToDerivedTypeAndGetHashCode(object? obj) => (int)obj!;
    }
  }
}
