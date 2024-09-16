namespace NJ.Common.EqComparers
{
  public static class EqualityComparerHelper
  {
    public static bool TryGetComparisonResultByNulls(object? x, object? y, out bool? comparison)
    {
      if (x is null && y is null)
      {
        comparison = true;
        return true;
      }
      if (x is null ^ y is null)
      {
        comparison = false;
        return true;
      }

      comparison = null;
      return false;
    }
  }
}
