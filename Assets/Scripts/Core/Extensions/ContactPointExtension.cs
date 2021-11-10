using UnityEngine;

namespace Core.Extensions
{
    public static class ContactPointExtension
    {
        public static Vector3 ToLocalPoint(this ContactPoint contactPoint, Transform transform)
        {
            return transform.InverseTransformPoint(contactPoint.point);
        }
    }
}