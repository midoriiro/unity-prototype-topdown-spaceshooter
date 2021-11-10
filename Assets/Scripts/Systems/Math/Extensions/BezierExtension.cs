using System.Collections.Generic;
using System.Collections.Immutable;
using Systems.Helpers;
using UnityEngine;

namespace Systems.Math.Extensions
{
    public static class BezierExtension
    {
        public static void AddPoint(this Bezier bezier, Vector3 point)
        {
            bezier.Points.Add(point);
        }

        public static void RemovePoint(this Bezier bezier, Vector3 point)
        {
            bezier.Points.Remove(point);
        }
        
        public static ImmutableList<Vector3> Flatten(this Bezier bezier, int resolution)
        {
            var steps = 1f / resolution;
            var points = bezier.Points;
            var flattenPoints = new List<Vector3>
            {
                MathHelper.Bezier(bezier.Order, 0, points.ToImmutableList())
            };

            for (var i = 1; i <= resolution; i++)
            {
                var t = i * steps;
                flattenPoints.Add(MathHelper.Bezier(bezier.Order, t, points.ToImmutableList()));
            }

            return flattenPoints.ToImmutableList();
        }

        public static Vector3 PositionAtTime(this Bezier bezier, float time)
        {
            return MathHelper.CasteljausAlgorithm(bezier.Points.ToImmutableList(), time);
        }
    }
}