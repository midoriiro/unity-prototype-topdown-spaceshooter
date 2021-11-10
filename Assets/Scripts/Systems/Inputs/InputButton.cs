using System;
using UnityEngine;

namespace Systems.Inputs
{
    [Serializable]
    public class InputButton
    {
        [HideInInspector] public bool value;
        public string name;
        public Color color;
    }
}