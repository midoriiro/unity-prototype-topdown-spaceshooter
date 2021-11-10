using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Extensions
{
    public static class Vector3Extension
    {
        public static Vector3 RemoveDepthAxis(this Vector3 vector)
        {
            vector.z = 0;
            return vector;
        }

        public static Vector2 ToVector2(this Vector3 vector)
        {
            return vector;
        }

        public static FastNoiseSIMD.VectorSet ToVectorSet(this Vector3 vector)
        {
            return new FastNoiseSIMD.VectorSet(new []{vector});
        }
        
        public static FastNoiseSIMD.VectorSet ToVectorSet(this IEnumerable<Vector3> vectors)
        {
            return new FastNoiseSIMD.VectorSet(vectors.ToArray());
        }

        public static Vector3 PerpendicularClockwise(this Vector3 vector, Vector3 target, Vector3 axis)
        {
            Vector3 direction = target - vector;
            return Vector3.Cross(direction, axis).normalized;
        }

        public static Vector3 PerpendicularCounterClockwise(this Vector3 vector, Vector3 target, Vector3 axis)
        {
            return -vector.PerpendicularClockwise(target, axis);
        }
    }
}