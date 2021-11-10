using System.Collections.Generic;
using UnityEngine;

namespace Systems.Math
{
    public class Bezier
    {
        public int Order => this.Points.Count - 1;
        public List<Vector3> Points { get; set; } = new List<Vector3>();
    }
}