using System;
using UnityEngine;

namespace Systems.Inputs
{
    [Serializable]
    public class TwinInputAxis
    {
        public bool stateless = true;
        public float deadZone;
        public float inertia;
        [HideInInspector] public float lastActivation;
        public InputAxis horizontal;
        public InputAxis vertical;
    }
}