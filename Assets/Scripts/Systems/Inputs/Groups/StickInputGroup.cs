using System;

namespace Systems.Inputs.Groups
{
    [Serializable]
    public class StickInputGroup : InputGroup
    {
        public TwinInputAxis left;
        public InputButton leftButton;
        public TwinInputAxis right;
        public InputButton rightButton;
    }
}