using System.Collections.Generic;

namespace RB.Staff.Web.Tests.Sources.Pvt.Extensions
{
    internal static class EnumerableExtensions
    {
        public static string JoinAsStrings<T>(this IEnumerable<T> strings, string delimiter)
        {
            return string.Join(delimiter, strings);
        }
    }
}