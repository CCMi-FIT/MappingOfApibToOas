using System.Collections.Generic;
using System.Linq;

namespace NJ.Common.EqComparers
{
  public class CollectionOrderIndependentEqualityComparer<T> : IEqualityComparer<ICollection<T>?>
  {
    private readonly IEqualityComparer<T> _itemEqComparer;

    public CollectionOrderIndependentEqualityComparer(IEqualityComparer<T>? itemEqualityComparer = null)
    {
      _itemEqComparer = itemEqualityComparer ?? EqualityComparer<T>.Default;
    }

    public bool Equals(ICollection<T>? x, ICollection<T>? y)
    {
      if (x is null && y is null)
        return true;
      if (x is null || y is null)
        return false;

      if (x.Count != y.Count)
        return false;

      foreach (var xItem in x)
        if (NumberOfOccurrencesIsDifferent(xItem, x, y))
          return false;
      return true;
    }

    public int GetHashCode(ICollection<T>? collection)
    {
      var result = 0;
      if (collection is null)
        return result;

      foreach (var item in collection)
      {
        var itemHashCode = item is null ? 0 : _itemEqComparer.GetHashCode(item);
        result ^= itemHashCode;
      }

      return result;
    }

    private bool NumberOfOccurrencesIsDifferent(T item, ICollection<T> x, ICollection<T> y)
    {
      var occurrencesInX = x.Count(xItem => _itemEqComparer.Equals(xItem, item));
      var occurrencesInY = y.Count(yItem => _itemEqComparer.Equals(yItem, item));
      var result = occurrencesInX != occurrencesInY;
      return result;
    }
  }
}
