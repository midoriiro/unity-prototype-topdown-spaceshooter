using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using UnityMathf = UnityEngine.Mathf;

namespace Systems.Helpers
{
    public static class MathHelper
    {
        public static readonly float SpaceConstant = 5f;
        
        private static readonly List<List<int>> BinomialLookupTable = new List<List<int>>();
        
        public static float Ratio(float a, float b)
        {
            var min = UnityMathf.Min(a, b);
            var max = UnityMathf.Max(a, b);

            return min / max;
        }

        public static float InverseOfRatio(float a, float b)
        {
            return OneMinus(MathHelper.Ratio(a, b));
        }

        public static float OneMinus(float percentage)
        {
            return 1f - percentage;
        }

        public static float Map(float value, float a1, float a2, float b1, float b2)
        {
            return b1 + (value - a1) * (b2 - b1) / (a2 - a1);
        }

        public static float LinearInterpolation(float t, float a, float b)
        {
            return a + (b - a) * t;
        }
        
        public static float OrderedLinearInterpolation(float t, float a, float b)
        {
            var min = Mathf.Min(a, b);
            var max = Mathf.Max(a, b);
            return LinearInterpolation(t, min, max);
        }
        
        public static Vector3 LinearInterpolation(float t, Vector3 a, Vector3 b)
        {
            return (1f - t) * a + (t * b);
        }

        public static Vector3 QuadraticInterpolation(float t, Vector3 a, Vector3 b, Vector3 c)
        {
            var u = 1f - t;
            var uu = Mathf.Pow(u, 2);
            var uuu = 2 * u * t;
            var tt = Mathf.Pow(t, 2);
            return uu * a + uuu * b + tt * c;
        }
        
        public static int Binomial(int n, int k)
        {
            while (n > BinomialLookupTable.Count)
            {
                var count = BinomialLookupTable.Count;
                var nextRow = new List<int>();
                nextRow.Add(1);
                
                for (int i = 1, previous = count- 1; i < count; i++)
                {
                    nextRow[i] = BinomialLookupTable[previous][i - 1] + BinomialLookupTable[previous][i];
                }
                
                nextRow.Add(1);
                BinomialLookupTable.Add(nextRow);
            }
            
            return BinomialLookupTable[n][k];
        }

        public static Vector3 Bezier(int n, float t, ImmutableList<Vector3> weights)
        {
            Vector3 sum = Vector3.zero;

            for (var k = 0; k <= n; k++)
            {
                sum += weights[k] * Binomial(n, k) * Mathf.Pow(1f - t, n - k) * Mathf.Pow(t, k);
            }

            return sum;
        }
        
        public static Vector3 CasteljausAlgorithm(ImmutableList<Vector3> points, float t)
        {
            var pointList = new List<Vector3>(points);

            while (true)
            {
                if (pointList.Count == 1)
                {
                    break;
                }
                
                var newPoints = new List<Vector3>(pointList.Count - 1);
                    
                for (var i = 0; i < newPoints.Capacity; i++)
                {
                    var p0 = pointList[i];
                    var p1 = pointList[i + 1];
                    newPoints.Add(MathHelper.LinearInterpolation(t, p0, p1));
                }

                pointList = newPoints;
            }

            return pointList.Single();
        }
    }
}
