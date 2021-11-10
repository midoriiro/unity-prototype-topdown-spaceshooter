using System;
using System.Data.Common;
using UnityEngine;

namespace Systems.Transforms.Extensions
{
    public static class AxisMapExtension
    {
        public static float Value(this AxisMap axisMap, Vector3 vector)
        {
            switch (axisMap.axis)
            {
                case Axis.X:
                    return vector.x;
                case Axis.Y:
                    return vector.y;
                case Axis.Z:
                    return vector.z;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static bool IsFacing(this AxisMap axisMap, Vector3 direction, Func<float, bool> predicate)
        {
            var dot = Vector3.Dot(axisMap.NormalizedMap(), direction.normalized);
            return predicate(dot);
        }
        
        public static Vector3 Map(this AxisMap axisMap, Vector3 vector, float value)
        {
            switch (axisMap.axis)
            {
                case Axis.X:
                    vector.x = axisMap.MapValue(vector.x, value);
                    break;
                case Axis.Y:
                    vector.y = axisMap.MapValue(vector.y, value);
                    break;
                case Axis.Z:
                    vector.z = axisMap.MapValue(vector.z, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return vector;
        }
        
        public static Vector3 NormalizedMap(this AxisMap axisMap, Vector3 vector, float value)
        {
            return Map(axisMap, vector, value).normalized;
        }
        
        public static Vector3 NormalizedMap(this AxisMap axisMap)
        {
            return axisMap.Map(Vector3.zero, 1f);
        }

        public static Vector3 TransformMap(this AxisMap axisMap, Transform transform)
        {
            var vector = Vector3.zero;
            
            switch (axisMap.axis)
            {
                case Axis.X:
                    vector = -transform.right; // substitution to missing transform.left 
                    break;
                case Axis.Y:
                    vector = transform.up;
                    break;
                case Axis.Z:
                    vector = transform.forward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return axisMap.inverted ? -vector : vector;
        }
        
        public static void TransformMap(this AxisMap axisMap, Transform transform, Vector3 direction)
        {
            direction = axisMap.inverted ? -direction : direction;
            
            switch (axisMap.axis)
            {
                case Axis.X:
                    transform.right = -direction; // substitution to missing transform.left 
                    break;
                case Axis.Y:
                    transform.up = direction;
                    break;
                case Axis.Z:
                    transform.forward = direction;
                    break;
            }
        }

        private static float MapValue(this AxisMap axisMap, float vectorValue, float value)
        {
            switch (axisMap.mode)
            {
                case AxisMapMode.Assignment:
                    vectorValue = value;
                    break;
                case AxisMapMode.Sum:
                    vectorValue += value;
                    break;
            }

            return axisMap.inverted ? -vectorValue : vectorValue;
        }
    }
}