using System;
using Systems.Transforms;
using Core.Attributes;
using UnityEngine;

namespace Systems.Inputs
{
    [Serializable]
    public class InputAxis
    {
        [HideInInspector] public float value;
        public string name;
        public Color color;
        public AxisMap axisMap;
    }
}