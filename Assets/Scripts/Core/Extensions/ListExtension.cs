using System.Collections.Generic;
using Core.Delegates;

namespace Core.Extensions
{
    public static class ListExtension
    {
        public static void ForEach<T>(this List<T> list, ActionRef<T> action)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                action(ref item);
                list[i] = item;
            }
        }
    }
}