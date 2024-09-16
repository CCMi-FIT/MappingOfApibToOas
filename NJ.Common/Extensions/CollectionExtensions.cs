using System.Collections.Generic;

namespace NJ.Common.Extensions
{
  public static class CollectionExtensions
  {
    public static void AddRangeParams<T>(this ICollection<T> collection, params T[] items)
    {
      collection.AddRangeExtension(items);
    }

    public static void AddRangeExtension<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
      foreach (var item in items)
        collection.Add(item);
    }

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
      foreach (var item in items)
        collection.Add(item);
    }

    public static void AddItems<T>(this ICollection<T> collection, params T[] items)
    {
      foreach (var item in items)
        collection.Add(item);
    }
  }
}
