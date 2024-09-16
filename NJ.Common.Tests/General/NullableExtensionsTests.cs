using NJ.Common.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace NJ.Common.Tests.General
{
    public class NullableExtensionsTests
  {
    [Fact]
    public void NullableExtensionsGetValueThrowableTest()
    {
      var testConfigs = new List<(bool? Nullable, bool? ExpectedResult, Type? ExpectedExceptionType)>
      {
        (null, null, typeof(ArgumentException)),
        (false, false, null),
        (true, true, null)
      };

      foreach (var tc in testConfigs)
      {
        var nullable = tc.Nullable;
        var expectedResult = tc.ExpectedResult;
        var expectedExceptionType = tc.ExpectedExceptionType;

        bool result;
        try
        {
          result = nullable.GetValueThrowable();
        }
        catch (Exception ex)
        {
          if (expectedExceptionType is null)
            throw;
          if (expectedExceptionType.IsAssignableFrom(ex.GetType()))
            continue;
          throw;
        }

        var equal = expectedResult == result;
        Assert.True(equal);
        Assert.Null(expectedExceptionType);
      }
    }
  }
}
