using System;
using UnityEngine;

namespace Behaviours.Meshes.Settings
{
    [Serializable]
    public class PlanetMeshGeneratorSettings : SphereMeshGeneratorSettings
    {
        [Range(0, 1)] public float seaBaseline;
    }
}