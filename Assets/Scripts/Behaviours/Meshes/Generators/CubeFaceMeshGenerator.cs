using System.Linq;
using UnityEngine;

namespace Behaviours.Meshes.Generators
{
    public class CubeFaceMeshGenerator
    {
        public static MeshData Generate(int resolution, Vector3 direction)
        {
            var axisA = new Vector3(direction.y, direction.z, direction.x);
            var axisB = Vector3.Cross(direction, axisA);
            
            var vertices = new Vector3[resolution * resolution];
            var triangles = new int[(resolution - 1) * (resolution - 1) * 6];
            var triangleIndex = 0;

            for (var y = 0; y < resolution; y++)
            {
                for (var x = 0; x < resolution; x++)
                {
                    var index = x + y * resolution;
                    var percent = new Vector2(x, y) / (resolution - 1);
                    var pointOnUnitCube = direction + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                    vertices[index] = pointOnUnitCube;

                    if (x == resolution - 1 || y == resolution - 1)
                    {
                        continue;
                    }

                    triangles[triangleIndex] = index;
                    triangles[triangleIndex + 1] = index + resolution + 1;
                    triangles[triangleIndex + 2] = index + resolution;

                    triangles[triangleIndex + 3] = index;
                    triangles[triangleIndex + 4] = index + 1;
                    triangles[triangleIndex + 5] = index + resolution + 1;
                    
                    triangleIndex += 6;
                }
            }

            return new MeshData
            {
                vertices = vertices,
                normals = vertices.Select(x => x.normalized).ToArray(),
                triangles = triangles
            };
        }
    }
}