using System;

namespace NJ.Common.Extensions
{
    public static class NullableExtensions
    {
        public static T GetValueThrowable<T>(this T? nullable) where T : struct
        {
            if (!nullable.HasValue)
                throw new ArgumentNullException(nameof(nullable));
            return nullable.Value;
        }
    }
}
