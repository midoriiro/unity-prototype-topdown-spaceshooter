using System;

namespace Systems.Inputs.Groups
{
    [Serializable]
    public class TriggerInputGroup : InputGroup
    {
        public ParametricInputAxis left;
        public ParametricInputAxis leftShared;
        public ParametricInputAxis right;
        public ParametricInputAxis rightShared;
    }
}