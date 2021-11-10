using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Behaviours.Meshes.Settings
{
    [Serializable]
    public class MeshGeneratorSettings
    {
        public IndexFormat indexFormat;
        [Range(2, 256)] public int resolution;
    }
}