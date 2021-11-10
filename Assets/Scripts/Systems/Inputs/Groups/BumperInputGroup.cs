using System;

namespace Systems.Inputs.Groups
{
    [Serializable]
    public class BumperInputGroup : InputGroup
    {
        public InputButton left;
        public InputButton right;
    }
}