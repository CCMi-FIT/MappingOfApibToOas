using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NJ.Common.Tests.BasicExtensions
{
  public class ICollectionExtensionsTests
  {
    [Fact]
    public void AddRangeTest()
    {
      var collection = new List<string> { "A", "B" };
      var range = new List<string> { "C", "D" };
      var expectedResult = new List<string> { "A", "B", "C", "D" };
      collection.AddRange(range);
      var equal = expectedResult.SequenceEqual(collection);
      Assert.True(equal);
    }
  }
}
