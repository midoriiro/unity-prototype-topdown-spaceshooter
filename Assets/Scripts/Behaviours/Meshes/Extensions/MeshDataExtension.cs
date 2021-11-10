using System.Collections.Generic;
using System.Linq;
using Behaviours.Meshes.Generators;
using UnityEngine;
using UnityEngine.Rendering;

namespace Behaviours.Meshes.Extensions
{
    public static class MeshDataExtension
    {      
        public static Mesh ToMesh(this MeshData meshData, IndexFormat indexFormat)
        {
            var mesh = new Mesh
            {
                indexFormat = indexFormat,
                vertices = meshData.vertices,
                triangles = meshData.triangles,
                normals = meshData.normals
            };
            
            return mesh;
        }
        
        public static Mesh Combine(this List<MeshData> meshDatas, Matrix4x4 transform, IndexFormat indexFormat)
        {
            var length = meshDatas.ToList().Count;
            CombineInstance[] combine = new CombineInstance[length];
            
            for (var i = 0; i < length; i++)
            {
                var data = meshDatas.ElementAt(i);
                combine[i].mesh = data.ToMesh(indexFormat);
                combine[i].transform = transform;
            }
            
            var mesh = new Mesh();
            mesh.CombineMeshes(combine);
            return mesh;
        }
    }
}