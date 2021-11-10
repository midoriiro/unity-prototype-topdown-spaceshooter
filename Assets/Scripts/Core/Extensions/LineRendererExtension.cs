using UnityEngine;

namespace Core.Extensions
{
    public static class LineRendererExtension
    {
        public static void Reset(this LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = 0;
        }
    }
}