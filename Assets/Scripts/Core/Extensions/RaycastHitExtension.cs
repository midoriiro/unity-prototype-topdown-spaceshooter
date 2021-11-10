using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Extensions
{
    public static class RaycastHitExtension
    {
        public static IEnumerable<RaycastHit> ExcludeHierarchy(this IEnumerable<RaycastHit> hits, GameObject gameObject)
        {
            var hierarchy = gameObject.Children()
                .Select(x => x.transform.GetInstanceID())
                .Append(gameObject.transform.GetInstanceID());

            return hits.Where(x => !hierarchy.Contains(x.transform.GetInstanceID()));
        }
    }
}