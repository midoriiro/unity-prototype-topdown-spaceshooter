using System.Collections.Generic;

namespace Core.Extensions
{
    public static class DictionaryExtension
    {
        public static V Get<K, V>(this Dictionary<K, V> dictionary, K key)
        {
            dictionary.TryGetValue(key, out var value);
            return value;
        }
    }
}