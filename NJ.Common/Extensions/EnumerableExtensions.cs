using System.Collections.Generic;
using System.Linq;

namespace NJ.Common.Extensions
{
  public static class EnumerableExtensions
  {
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
    {
      if (enumerable is null)
        return true;
      if (!enumerable.Any())
        return true;
      return false;
    }

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? enumerable) => !IsNullOrEmpty(enumerable);
  }
}
