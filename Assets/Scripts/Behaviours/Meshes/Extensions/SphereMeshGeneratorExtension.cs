using System.Collections.Generic;
using System.Linq;
using Behaviours.Meshes.Generators;
using UnityEngine;

namespace Behaviours.Meshes.Extensions
{
    public static class SphereMeshGeneratorExtension
    {
        private static readonly Vector3[] Directions =
        {
            Vector3.up, 
            Vector3.down, 
            Vector3.left, 
            Vector3.right, 
            Vector3.forward, 
            Vector3.back
        };
        
        public static MeshData GenerateRawFace(this SphereMeshGenerator generator, Vector3 direction)
        {
            var data = CubeFaceMeshGenerator.Generate(generator.settings.resolution, direction);
            return data;
        }
        
        public static MeshData GenerateFace(this SphereMeshGenerator generator, Vector3 direction)
        {
            var data = generator.GenerateRawFace(direction);
            data.vertices = data.vertices.Select(x => x.normalized * generator.settings.radius).ToArray();
            data.normals = data.vertices.Select(x => x.normalized).ToArray();
            return data;
        }

        public static List<MeshData> GenerateRaw(this SphereMeshGenerator generator)
        {
            return Directions.Select(direction => generator.GenerateRawFace(direction)).ToList();
        }
        
        public static List<MeshData> Generate(this SphereMeshGenerator generator)
        {
            return Directions.Select(direction => generator.GenerateFace(direction)).ToList();
        }
    }
}