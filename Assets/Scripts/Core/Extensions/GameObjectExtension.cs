using System.Collections.Generic;
using System.Linq;
using Systems.Math;
using UnityEngine;

namespace Core.Extensions
{
    public static class GameObjectExtension
    {       
        #region Hierarchy
        public static GameObject CreateChild(this GameObject gameObject, string name)
        {
            var child = new GameObject(name);
            child.transform.SetParent(gameObject.transform, false);
            return child;
        }

        public static GameObject GetOrCreateChild(this GameObject gameObject, string name)
        {
            var hasChild = gameObject.gameObject.transform.Find(name);
            return hasChild ? hasChild.gameObject : CreateChild(gameObject, name);
        }

        public static LineRenderer CreateLineRenderer(this GameObject gameObject, string name)
        {
            var child = GetOrCreateChild(gameObject, name);
            return child.AddComponent<LineRenderer>();
        }
        
        public static LineRenderer GetOrCreateLineRenderer(this GameObject gameObject, string name)
        {
            var child = GetOrCreateChild(gameObject, name);
            var lineRenderer = child.GetComponent<LineRenderer>();

            return lineRenderer ? lineRenderer : CreateLineRenderer(gameObject, name);
        }
        
        public static TrailRenderer CreateTrailRenderer(this GameObject gameObject, string name)
        {
            var child = GetOrCreateChild(gameObject, name);
            return child.AddComponent<TrailRenderer>();
        }
        
        public static TrailRenderer GetOrCreateTrailRenderer(this GameObject gameObject, string name)
        {
            var child = GetOrCreateChild(gameObject, name);
            var trailRenderer = child.GetComponent<TrailRenderer>();

            return trailRenderer ? trailRenderer : CreateTrailRenderer(gameObject, name);
        }
        
        public static IEnumerable<GameObject> FindChildrenGenerator(this GameObject gameObject)
        {
            var stack = new List<KeyValuePair<int, Transform>>();
            var result = new List<GameObject>();
            var depth = 0;
            
            stack.Add(new KeyValuePair<int, Transform>(depth, gameObject.transform));

            while (true)
            {
                var references = stack
                    .Where(x => x.Key == depth)
                    .Select(x => x.Value)
                    .ToList();

                if (references.Count == 0)
                {
                    break;
                }
                
                depth++;

                foreach (var reference in references)
                {
                    for (var i = 0; i < reference.childCount; i++)
                    {
                        var children = reference.GetChild(i).gameObject;

                        yield return children;
                        
                        stack.Add(new KeyValuePair<int, Transform>(depth, children.transform));
                    }
                }
            }
        }

        public static GameObject FindChildByTag(this GameObject gameObject, string tag)
        {
            return FindChildrenGenerator(gameObject)
                .SingleOrDefault(x => x.CompareTag(tag));
        }
        
        public static List<GameObject> FindChildrenByTag(this GameObject gameObject, string tag)
        {
            return FindChildrenGenerator(gameObject)
                .Where(x => x.CompareTag(tag))
                .ToList();
        }
        
        public static List<GameObject> Children(this GameObject gameObject)
        {
            return FindChildrenGenerator(gameObject).ToList();
        }

        public static List<GameObject> Hierarchy(this GameObject gameObject)
        {
            return FindChildrenGenerator(gameObject)
                .Append(gameObject)
                .ToList();
        }

        public static List<T> GetComponentInHierarchy<T>(this GameObject gameObject)
        {
            return gameObject.Hierarchy()
                .Select(x => x.GetComponent<T>())
                .ToList();
        }
        #endregion

        #region Collision
        public static void IgnoreCollisionFromHierarchy(this GameObject gameObject)
        {
            var hierarchy = gameObject.GetComponentInHierarchy<Collider>();

            hierarchy.ForEach(x =>
            {
                hierarchy.ForEach(y =>
                {
                    Physics.IgnoreCollision(x, y);
                });
            });
        }

        public static void IgnoreCollisionFromGameObject(this GameObject gameObject, Collider collider)
        {
            gameObject
                .GetComponentInHierarchy<Collider>()
                .ForEach(x => Physics.IgnoreCollision(x, collider));
        }
        #endregion

        #region Draw Shapes Gizmos
        public static void DrawLine(this GameObject gameObject, Vector3 from, Vector3 to, float width)
        {
            var count = Mathf.CeilToInt(width);
            
            if(count <= 1)
            {
                Gizmos.DrawLine(from, to);
            }
            else
            {
                var camera = Camera.current;
                
                if (camera == null)
                {
                    Debug.LogError("Camera.current is null");
                    return;
                }
                
                var direction = (to - from).normalized;
                var directionToCamera = (camera.transform.position - from).normalized;
                var normal = Vector3.Cross(direction,directionToCamera);
                
                for(var i = 0; i < count; i++)
                {
                    var o = normal * width * ((float)i/(count-1) - 0.5f);
                    Gizmos.DrawLine(from + o, to + o);
                }
            }
        }
        
        public static void DrawWireArc(this GameObject gameObject, Vector3 position, Vector3 direction, Vector3 axis, float startAngle, float endAngle, float width = 1f, int resolution = 360)
        {
            var length = endAngle - startAngle;
            var pointCount = resolution + 1;
            var innerPoints = new Vector3[pointCount];
            var outerPoints = new Vector3[pointCount];
            
            for (var i = 0; i < pointCount; i++)
            {
                var angle = i * length / resolution;
                var rotation = Quaternion.AngleAxis(angle + startAngle, axis); 
                innerPoints[i] = rotation * direction + position;
                outerPoints[i] = rotation * (direction + (direction.normalized * width)) + position;

                if (i == 0)
                {
                    continue;
                }
                
                Gizmos.DrawLine(innerPoints[i - 1], innerPoints[i]);
                Gizmos.DrawLine(outerPoints[i - 1], outerPoints[i]);
            }

            Gizmos.DrawLine(innerPoints[0], outerPoints[0]);
            Gizmos.DrawLine(innerPoints[pointCount - 1], outerPoints[pointCount - 1]);
        }
        
        public static void DrawArc(this GameObject gameObject, Vector3 position, Vector3 direction, Vector3 axis, float startAngle, float endAngle, float width = 1f, int resolution = 360, bool reverse = false)
        {
            var step = 1f / resolution;
            var length = endAngle - startAngle;
            var pointCount = resolution + 1;
            var innerPoints = new Vector3[pointCount];
            var outerPoints = new Vector3[pointCount];
            
            for (var i = 0; i < pointCount; i++)
            {
                var angle = i * length / resolution;
                var rotation = Quaternion.AngleAxis(angle + startAngle, axis); 
                innerPoints[i] = rotation * direction + position;
                outerPoints[i] = rotation * (direction + (direction.normalized * width)) + position;

                if (i == 0)
                {
                    continue;
                }

                var previousInnerPoint = innerPoints[i - 1];
                var currentInnerPoint = innerPoints[i];
                var previousOuterPoint = outerPoints[i - 1];
                var currentOuterPoint = outerPoints[i];

                for (var t = 0f; t < 1; t = t + step)
                {
                    var innerPoint = Vector3.Lerp(previousInnerPoint, currentInnerPoint, t);
                    var outerPoint = Vector3.Lerp(previousOuterPoint, currentOuterPoint, t);
                    Gizmos.DrawLine(innerPoint, outerPoint);
                }
            }
            
            
        }
        #endregion

        #region Draw shapes        
        public static void DrawCircle(this GameObject gameObject, string name, Vector3 position, Vector3 direction, Vector3 axis, float width = 1f, int resolution = 360)
        {
            var lineRenderer = gameObject.GetOrCreateLineRenderer($"gizmo_circle_{gameObject.GetInstanceID()}_{name}");
            lineRenderer.useWorldSpace = true;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.positionCount = resolution + 1;

            var pointCount = resolution + 1;
            var points = new Vector3[pointCount];
            
            for (var i = 0; i < pointCount; i++)
            {
                var angle = i * 360f / resolution;
                points[i] = Quaternion.AngleAxis(angle, axis) * direction + position;
            }

            lineRenderer.SetPositions(points);
        }

        public static void DrawLine(this GameObject gameObject, string name, Vector3[] points, float width = 1f, bool useWorldSpace = true)
        {
            var lineRenderer = gameObject.GetOrCreateLineRenderer($"gizmo_line_{gameObject.GetInstanceID()}_{name}");
            lineRenderer.useWorldSpace = useWorldSpace;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }

        public static void DrawBezier(this GameObject gameObject, string name, Bezier bezier, float t)
        {
            // TODO this follow
//        function drawCurve(points[], t):
//            if(points.length==1):
//        draw(points[0])
//            else:
//            newpoints=array(points.size-1)
//            for(i=0; i<newpoints.length; i++):
//              newpoints[i] = (1-t) * points[i] + t * points[i+1]
//            drawCurve(newpoints, t)
        }
        #endregion
    }
}
