using System;
using System.Collections.Generic;
using System.Linq;
using Systems.Helpers;
using Systems.Math;
using Systems.Transforms;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Effects;
using UnityEditor.Rendering;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public class PropulsionEffect : MonoBehaviour
    {
        public float speed;
        public AxisMap axisMap;
        public FacingTwinAxisMap facingTwinAxisMap;
        public FloatRange range;
        public PropulsionEngine propulsionEngine;
        public AngularPropulsionEngine angularPropulsionEngine;
        public new LineRenderer renderer;

        private void Start()
        {
            this.facingTwinAxisMap.x.axis = Axis.X;
            this.facingTwinAxisMap.y.axis = Axis.Y;
        }

        private void Update()
        {
            var isPropelling = this.propulsionEngine.IsPropelling;
            var direction = this.angularPropulsionEngine.NormalizedVelocity;
            var velocity = this.propulsionEngine.NormalizedVelocity;
            var magnitude = this.propulsionEngine.NormalizedMagnitude;
            magnitude = this.ProcessingFacing(magnitude, velocity, direction);
            var isInverted = this.IsInverted(magnitude);
            magnitude = this.ClampMagnitude(magnitude, isInverted);
            magnitude = this.NormalizeMagnitude(magnitude, isInverted);

            // if (isPropelling)
            // {
            //     magnitude = Mathf.Lerp(
            //         magnitude, 1f, this.speed * Time.deltaTime     
            //     );
            // }
            // else
            // {
            //     magnitude = Mathf.Lerp(
            //         0f, magnitude, this.speed * Time.deltaTime     
            //     );
            // }
            

            if (isInverted)
            {
                this.WhenInverted(magnitude);
            }
            else
            {
                this.WhenNormal(magnitude);
            }
        }

        private void WhenNormal(float magnitude)
        {
            var minimum = 0f;
            var maximum = this.range.maximum;
            
            var length = MathHelper.LinearInterpolation( magnitude, minimum, maximum);
            var tail = this.renderer.GetPosition(1);
            
            this.renderer.SetPosition(0, Vector3.zero);
            this.renderer.SetPosition(1, this.axisMap.Map(tail, length));
        }
        
        private void WhenInverted(float magnitude)
        {
            var minimum = this.range.minimum;
            var maximum = 0f;
            
            var length = MathHelper.LinearInterpolation( magnitude, maximum, minimum);
            var tail = this.renderer.GetPosition(1);
            
            this.renderer.SetPosition(0, Vector3.zero);
            this.renderer.SetPosition(1, this.axisMap.Map(tail, length));
        }

        private float ProcessingFacing(float magnitude, Vector3 velocity, Vector3 direction)
        {
            var velocityX = Vector3.Dot(this.facingTwinAxisMap.x.NormalizedMap(), velocity);
            var velocityY = Vector3.Dot(this.facingTwinAxisMap.y.NormalizedMap(), velocity);
            var directionX = Vector3.Dot(this.facingTwinAxisMap.x.NormalizedMap(), direction);
            var directionY = Vector3.Dot(this.facingTwinAxisMap.y.NormalizedMap(), direction);
            
            if (this.axisMap.axis == Axis.Y && directionX <= -0.25f && velocityX <= -0.25f)
            {
                magnitude = -1f;
            }
            else if (this.axisMap.axis == Axis.Y && directionX <= -0.25f && 0.25f <= velocityX)
            {
                magnitude = 1f;
            }
            else if (this.axisMap.axis == Axis.Y && 0.25f <= directionX && 0.25f <= velocityX)
            {
                magnitude = -1f;
            }
            else if (this.axisMap.axis == Axis.Y && 0.25f <= directionX && velocityX <= -0.25f)
            {
                magnitude = 1f;
            }
            else
            {
                magnitude = 0f;
            }
            
            if (this.axisMap.axis == Axis.X && directionX <= -0.25f && velocityY <= -0.25f)
            {
                magnitude = -1f;
            }
            else if (this.axisMap.axis == Axis.X && directionX <= -0.25f && 0.25f <= velocityY)
            {
                magnitude = 1f;
            }
            else if (this.axisMap.axis == Axis.X && 0.25f <= directionY && 0.25f <= velocityX)
            {
                magnitude = 1f;
            }
            else if (this.axisMap.axis == Axis.X && 0.25f <= directionY && velocityX <= -0.25f)
            {
                magnitude = -1f;
            }
            else
            {
                magnitude = 0f;
            }

            return magnitude;
        }

        private bool IsInverted(float magnitude)
        {
            return magnitude < 0f;
        }

        private float ClampMagnitude(float magnitude, bool isInverted)
        {
            if (isInverted)
            {
                return Mathf.Clamp(magnitude, -1, 0);
            }

            return Mathf.Clamp(magnitude, 0, 1);
        }

        private float NormalizeMagnitude(float magnitude, bool isInverted)
        {
            if (!isInverted)
            {
                return magnitude;
            }
            
            return Mathf.Abs(magnitude);
        }
    }
}