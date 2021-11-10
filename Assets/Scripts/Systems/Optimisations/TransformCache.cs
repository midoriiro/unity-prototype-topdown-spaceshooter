using System.Collections.Generic;
using Core.Extensions;
using JetBrains.Annotations;
using UnityEngine;

namespace Systems.Optimisations
{
    public static class TransformCache
    {     
        private static readonly Dictionary<Transform, object> Cache = new Dictionary<Transform, object>();

        [CanBeNull]
        public static T Get<T>(Transform key) where T : class
        {
            lock (Cache)
            {
                Cache.TryGetValue(key, out var value);

                if (value != null)
                {
                    return (T) value;
                }

                Cache.Remove(key);
                return null;

            }
        }

        public static void Add(Transform key, object value)
        {
            lock (Cache)
            {
                Cache.Add(key, value);
            }
        }
    }
}