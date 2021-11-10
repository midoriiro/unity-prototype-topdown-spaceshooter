using Systems.Transforms.Extensions;
using UnityEngine;

namespace Systems.Inputs.Extensions
{
    public static class InputAxisExtension
    {
        public static void Update(this InputAxis input)
        {
            input.value = Input.GetAxis(input.name);
        }

        public static Vector3 Direction(this InputAxis input)
        {
            return input.axisMap.Map(Vector3.zero, input.value);
        }
    }
}