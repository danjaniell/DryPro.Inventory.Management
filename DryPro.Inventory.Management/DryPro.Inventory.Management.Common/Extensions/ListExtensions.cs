using System.Collections.Generic;

namespace DryPro.Inventory.Management.Common.Extensions
{
    public static class ListExtensions
    {
        public static bool AddIfUnique<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
                return true;
            }
            return false;
        }
    }
}
