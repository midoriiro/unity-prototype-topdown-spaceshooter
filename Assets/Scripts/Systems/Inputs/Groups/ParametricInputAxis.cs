using System;
using UnityEngine;

namespace Systems.Inputs.Groups
{
    [Serializable]
    public class ParametricInputAxis : InputAxis
    {
        public bool stateless = true;
        public float deadZone;
        public float inertia;
        [HideInInspector] public float lastActivation;
    }
}