using UnityEngine;

namespace Systems.Transforms.Extensions
{
    public static class FreezeAxisExtension
    {
        public static Vector3 Freeze(this FreezeAxis axis, Vector3 vector)
        {
            if (axis.x)
            {
                vector.x = 0;
            }
        
            if (axis.y)
            {
                vector.y = 0;
            }
        
            if (axis.z)
            {
                vector.z = 0;
            }

            return vector;
        }
    }
}