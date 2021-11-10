using UnityEngine;

namespace Systems.Inputs.Extensions
{
    public static class InputButtonExtension
    {
        public static bool IsUp(this InputButton input)
        {
            input.value = Input.GetButtonUp(input.name);
            return input.value;
        }
        
        public static bool IsDown(this InputButton input)
        {
            input.value = Input.GetButtonDown(input.name);
            return input.value;
        }

        public static bool IsHold(this InputButton input)
        {
            input.value = Input.GetButton(input.name);
            return input.value;
        }
    }
}