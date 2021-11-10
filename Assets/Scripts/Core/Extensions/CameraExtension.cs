using UnityEngine;

namespace Core.Extensions
{
    public static class CameraExtension
    {
        public static Vector3 Center(this Camera camera)
        {
            return camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.farClipPlane));
        }
        
        public static float Radius(this Camera camera, bool withoutDepthAxis = false)
        {
            var corner = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
            var center = camera.Center();

            if (withoutDepthAxis)
            {
                corner = corner.RemoveDepthAxis();
                center = center.RemoveDepthAxis();
            }
            
            var radius = Vector3.Distance(corner, center);

            return radius;
        }

        public static float RadiusWithMarge(this Camera camera, bool withoutDepthAxis = false)
        {
            var radius = camera.Radius(withoutDepthAxis);
            var top = camera.ViewportToWorldPoint(new Vector3(0.5f, 0f, camera.farClipPlane));
            var left = camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, camera.farClipPlane));
            var center = camera.Center();
            
            if (withoutDepthAxis)
            {
                top = top.RemoveDepthAxis();
                left = left.RemoveDepthAxis();
                center = center.RemoveDepthAxis();
            }
            
            var distanceTopToCenter = Vector3.Distance(top, center);
            var distanceLeftToCenter = Vector3.Distance(left, center);
            var maxDistance = Mathf.Max(distanceTopToCenter, distanceLeftToCenter);

            return radius + (radius - maxDistance);
        }
    }
}