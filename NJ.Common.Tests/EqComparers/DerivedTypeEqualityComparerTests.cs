using System;
using System.Collections.Generic;
using NJ.Common.EqComparers;
using Xunit;

namespace NJ.Common.Tests.EqComparers
{
  public class DerivedTypeEqualityComparerTests
  {
    private readonly IEqualityComparer<string?> nullStringEqComparer;
    private readonly IDerivedTypeEqualityComparer<object?> derivedTypeEqComparer;

    public DerivedTypeEqualityComparerTests()
    {
      nullStringEqComparer = new NullStringEqualityComparer();
      derivedTypeEqComparer = new DerivedTypeEqualityComparer<string?, object?>(nullStringEqComparer);
    }

    [Fact]
    public void CastToDerivedTypeAndCheckEqualityTest()
    {
      var testConfigs = new List<(object? X, object? Y, Type? ExpectedException, bool? ExpectedResult)>
      {
        (null, null, null, true),
        (null, "", null, false),
        (null, 0, typeof(InvalidCastException), null),
        ("", null, null, false),
        ("", "", null, true),
        ("", 0, typeof(InvalidCastException), null),
        (0, null, typeof(InvalidCastException), null),
        (0, "", typeof(InvalidCastException),  null),
        (0, 0, typeof(InvalidCastException), null)
      };

      foreach (var tc in testConfigs)
      {
        var x = tc.X;
        var y = tc.Y;
        var expectedException = tc.ExpectedException;
        var expectedResult = tc.ExpectedResult;

        try
        {
          var result = derivedTypeEqComparer.CastToDerivedTypeAndCheckEquality(x, y);
          Assert.Null(expectedException);
          var equal = expectedResult == result;
          Assert.True(equal);
        }
        catch (Exception ex) when (ex.GetType() == expectedException)
        {
          Assert.Null(expectedResult);
        }
      }
    }

    [Fact]
    public void CastToDerivedTypeAndGetHashCodeTest()
    {
      var testConfigs = new List<(object? Object, Type? ExpectedException, int? ExpectedResult)>
      {
        (null, null, 0),
        (new object(), typeof(InvalidCastException), null),
        (0, typeof(InvalidCastException), null),
        ("", null, 123),
      };

      foreach (var tc in testConfigs)
      {
        try
        {
          var result = derivedTypeEqComparer.CastToDerivedTypeAndGetHashCode(tc.Object);
          Assert.Null(tc.ExpectedException);
          var equal = tc.ExpectedResult == result;
          Assert.True(equal);
        }
        catch (Exception ex) when (ex.GetType() == tc.ExpectedException)
        {
          Assert.Null(tc.ExpectedResult);
        }
      }
    }

    [Fact]
    public void CanCastToDerivedTypeTest()
    {
      var testConfigs = new List<(object?[] Objects, bool ExpectedResult)>
      {
        (new object?[0], true),
        (new object?[] { null }, true),
        (new object?[] { "" }, true),
        (new object?[] { 0 }, false),
        (new object?[] { null, null }, true),
        (new object?[] { null, "" }, true),
        (new object?[] { null, 0 }, false),
        (new object?[] { "", null }, true),
        (new object?[] { "", "" }, true),
        (new object?[] { "", 0 }, false),
        (new object?[] { 0, null }, false),
        (new object?[] { 0, "" }, false),
        (new object?[] { 0, 0 }, false)
      };

      foreach (var tc in testConfigs)
      {
        var result = derivedTypeEqComparer.CanCastToDerivedType(tc.Objects);
        var equal = tc.ExpectedResult == result;
        Assert.True(equal);
      }
    }

    private class NullStringEqualityComparer : IEqualityComparer<string?>
    {
      public bool Equals(string? x, string? y)
      {
        return x is null == y is null;
      }

      public int GetHashCode(string? obj)
      {
        return 123;
      }
    }
  }
}
