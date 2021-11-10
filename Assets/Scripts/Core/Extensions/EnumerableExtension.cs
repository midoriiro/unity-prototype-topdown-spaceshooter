using System;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class EnumerableExtension
    {
        public static TSource MinBy<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, float> selector)
        {
            TSource reference = default;
            float minimum = float.PositiveInfinity;

            foreach (var item in enumerable)
            {
                var value = selector(item);

                if (!(value < minimum))
                {
                    continue;
                }

                reference = item;
                minimum = value;
            }

            return reference;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}