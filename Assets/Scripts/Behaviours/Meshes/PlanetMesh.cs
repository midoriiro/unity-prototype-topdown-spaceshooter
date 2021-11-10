using System.Collections.Generic;
using System.Linq;
using Behaviours.Meshes.Extensions;
using Behaviours.Meshes.Generators;
using Behaviours.Meshes.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace Behaviours.Meshes
{
    [ExecuteInEditMode]
    public class PlanetMesh : MonoBehaviour
    {
        public PlanetMeshGeneratorSettings settings;
        public SphereMesh sea;
        public NoiseFilter noise;
        public MeshFilter filter;
        
        public SphereMeshGenerator Generator { get; private set; }

        private void OnValidate()
        {
            if (this.Generator is null)
            {
                this.Generator = new SphereMeshGenerator
                {
                    settings = this.settings
                };
            
                this.noise.SettingsChanged += this.OnValidate;
            }
            
            if (this.settings.indexFormat == IndexFormat.UInt16 && this.settings.resolution > 100)
            {
                this.settings.resolution = 100;
            }
            
            var meshDatas = this.Generate();
            this.filter.sharedMesh = meshDatas.Combine(this.transform.localToWorldMatrix, this.settings.indexFormat);
            this.filter.sharedMesh.name = "Planet";
        }

        private List<MeshData> Generate()
        {
            var meshDatas = this.Generator.GenerateRaw();

            var values = new List<float>();

            foreach (var meshData in meshDatas)
            {
                for (var index = 0; index < meshData.vertices.Length; index++)
                {
                    var vertex = meshData.vertices[index];
                    var value = this.noise.Evaluate(vertex);
                    meshData.vertices[index] = vertex.normalized * (value+1) * this.settings.radius;
                    
                    values.Add(value);
                }

//                meshData.normals = meshData.vertices.Select(x => x.normalized).ToArray();
            }

            var min = values.Min();
            var max = values.Max();
            
            this.GenerateSea(Mathf.Lerp(min, max, this.settings.seaBaseline));

            return meshDatas;
        }

        private void GenerateSea(float baseline)
        {
            this.sea.settings.resolution = this.settings.resolution;
            this.sea.settings.radius = this.settings.radius;
            
            var meshDatas = this.sea.Generator.GenerateRaw();
            
            foreach (var meshData in meshDatas)
            {
                for (var index = 0; index < meshData.vertices.Length; index++)
                {
                    var vertex = meshData.vertices[index];
                    var value = this.noise.Evaluate(vertex);
                    meshData.vertices[index] = vertex.normalized * (baseline+1) * this.settings.radius;
                }

//                meshData.normals = meshData.vertices.Select(x => x.normalized).ToArray();
            }
            
            this.sea.AssignMeshData(meshDatas);
        }
    }
}