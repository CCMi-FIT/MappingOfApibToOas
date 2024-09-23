using System;
using System.Collections.Generic;
using System.Linq;

namespace NJ.Common.EqComparers
{
  public class SuperTypeEqualityComparer<T> : IEqualityComparer<T>
  {
    private readonly IReadOnlyCollection<IDerivedTypeEqualityComparer<T>> _derivedTypeComparers;

    public SuperTypeEqualityComparer(params IDerivedTypeEqualityComparer<T>[] derivedTypeComparers)
    {
      this._derivedTypeComparers = derivedTypeComparers.ToList();
    }

    public bool Equals(T? x, T? y)
    {
      if (x is null && y is null)
        return true;
      if (x is null || y is null)
        return false;

      var canCastDerivedTypeComparers = _derivedTypeComparers.Where(c => c.CanCastToDerivedType(x) && c.CanCastToDerivedType(y)).ToList();
      if (canCastDerivedTypeComparers.Count != 1)
        throw new NotSupportedException();

      var comparer = canCastDerivedTypeComparers.Single();
      var result = comparer.CastToDerivedTypeAndCheckEquality(x, y);
      return result;
    }

    public int GetHashCode(T obj)
    {
      if (obj is null)
        return 0;

      var canCompareComparers = _derivedTypeComparers.Where(c => c.CanCastToDerivedType(obj)).ToList();
      if (canCompareComparers.Count != 1)
        throw new NotSupportedException();

      var comparer = canCompareComparers.Single();
      var result = comparer.CastToDerivedTypeAndGetHashCode(obj);
      return result;
    }
  }
}
