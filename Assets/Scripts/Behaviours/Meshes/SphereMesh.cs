using System.Collections.Generic;
using Behaviours.Meshes.Extensions;
using Behaviours.Meshes.Generators;
using Behaviours.Meshes.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace Behaviours.Meshes
{
    [ExecuteInEditMode]
    public class SphereMesh : MonoBehaviour
    {
        public SphereMeshGeneratorSettings settings;
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
            }
            
            if (this.settings.indexFormat == IndexFormat.UInt16 && this.settings.resolution > 100)
            {
                this.settings.resolution = 100;
            }
            
            var meshDatas = this.Generator.Generate();
            this.AssignMeshData(meshDatas);
        }

        public void AssignMeshData(List<MeshData> meshDatas)
        {
            this.filter.sharedMesh = meshDatas.Combine(this.transform.localToWorldMatrix, this.settings.indexFormat);
            this.filter.sharedMesh.name = "Sphere";
        }
    }
}