using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Systems.Helpers;
using Systems.Transforms;
using Systems.Transforms.Extensions;
using UnityEngine;

namespace Systems.Inputs.Extensions
{
    public static class TwinInputAxisExtension
    {
        public static void Update(this TwinInputAxis inputAxis)
        {
            var horizontalValue = Input.GetAxis(inputAxis.horizontal.name);
            var verticalValue = Input.GetAxis(inputAxis.vertical.name);
            
            if (inputAxis.stateless)
            {
                inputAxis.horizontal.value = horizontalValue;
                inputAxis.vertical.value = verticalValue;
                return;
            }

            if (inputAxis.IsInDeadZone())
            {
                return;
            }

            inputAxis.horizontal.value = horizontalValue;
            inputAxis.vertical.value = verticalValue;
        }

        public static bool IsInDeadZone(this TwinInputAxis inputAxis)
        {
            var horizontalValue = Input.GetAxis(inputAxis.horizontal.name);
            var verticalValue = Input.GetAxis(inputAxis.vertical.name);
            var isHorizontalAxisInDeadZone = inputAxis.IsInDeadZone(horizontalValue);
            var isVerticalAxisInDeadZone = inputAxis.IsInDeadZone(verticalValue);
            var isInDeadZone = isHorizontalAxisInDeadZone && isVerticalAxisInDeadZone;

            if (!isInDeadZone)
            {
                inputAxis.lastActivation = Time.time;
                return false;
            }

            if (inputAxis.inertia <= 0f)
            {
                return true;
            }

            var delta = Time.time - inputAxis.lastActivation;

            if (delta < inputAxis.inertia)
            {
                var time = Mathf.Clamp01(delta / inputAxis.inertia) * Time.deltaTime;
                inputAxis.horizontal.value = Mathf.Lerp(
                    inputAxis.horizontal.value, 0f, time      
                );
                inputAxis.vertical.value = Mathf.Lerp(
                    inputAxis.vertical.value, 0f, time      
                );
                return true;
            }

            return false;
        }
        
        private static bool IsInDeadZone(this TwinInputAxis inputAxis, float value)
        {
            return -inputAxis.deadZone <= value && value <= inputAxis.deadZone;
        }
        
        public static Vector3 Direction(this TwinInputAxis inputAxis)
        {
            var direction = Vector3.zero;
            direction = inputAxis.horizontal.axisMap.Map(direction, inputAxis.horizontal.value);
            direction = inputAxis.vertical.axisMap.Map(direction, inputAxis.vertical.value);
            return direction;
        }

        public static IEnumerable<Axis> Axes(this TwinInputAxis inputAxis)
        {
            return new[]
            {
                inputAxis.horizontal.axisMap.axis,
                inputAxis.vertical.axisMap.axis
            };
        }
    }
}