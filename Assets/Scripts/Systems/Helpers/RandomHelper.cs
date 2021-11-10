using UnityEngine;

namespace Systems.Helpers
{
    public static class RandomHelper
    {
        public static Vector2 InsideTwoUnitCircles(float innerRadius, float outerRadius)
        {
            var position = Random.insideUnitCircle;
            return position.normalized * (innerRadius + outerRadius * Random.value);
        }
        
        public static Vector3 InsideTwoUnitSpheres(float innerRadius, float outerRadius)
        {
            var position = Random.insideUnitSphere;
            return position.normalized * (innerRadius + outerRadius * Random.value);
        }
    }
}
