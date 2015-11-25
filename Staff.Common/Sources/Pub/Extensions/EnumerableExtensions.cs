using System.Collections.Generic;

namespace RB.Staff.Common.Pub.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinAsStrings<T>(this IEnumerable<T> objects, string delimiter)
        {
            return string.Join(delimiter, objects);
        }
    }
}