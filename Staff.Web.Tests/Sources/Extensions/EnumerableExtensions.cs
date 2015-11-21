using System.Collections.Generic;

namespace Staff.Web.Tests.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinAsStrings<T>(this IEnumerable<T> strings, string delimiter)
        {
            return string.Join(delimiter, strings);
        }
    }
}