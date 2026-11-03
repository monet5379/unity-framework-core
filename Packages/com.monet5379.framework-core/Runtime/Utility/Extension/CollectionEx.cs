using System.Collections.Generic;

namespace Monet5379
{
    public static class CollectionEx
    {
        public static bool IsValid<T>(this T[] array)
        {
            return array != null && array.Length > 0;
        }

        public static bool IsValid<T>(this T[] array, int index)
        {
            return array != null && index >= 0 && array.Length > index;
        }

        public static bool IsValid<TValue>(this List<TValue> list)
        {
            return list != null && list.Count > 0;
        }

        public static bool IsValid<TValue>(this List<TValue> list, int index)
        {
            return list != null && index >= 0 && list.Count > index;
        }
    }
}
