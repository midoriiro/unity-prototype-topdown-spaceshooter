using System;

namespace Systems.Transforms
{
    [Serializable]
    public class AxisMap
    {
        public Axis axis;
        public bool inverted;
        public AxisMapMode mode;
    }
}