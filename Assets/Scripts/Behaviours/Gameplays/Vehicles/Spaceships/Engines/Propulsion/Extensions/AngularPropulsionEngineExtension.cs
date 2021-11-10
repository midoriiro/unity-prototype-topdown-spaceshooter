using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Systems.Helpers;
using Systems.Transforms;
using Behaviours.Physics.Compensators;
using Core.Enums;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions
{
    public static class AngularPropulsionEngineExtension
    {
        #region Constraints
        public static void ClampVelocity(this AngularPropulsionEngine engine)
        {
            if (!engine.settings.constraints.clamped)
            {
                return;
            }

            engine.ClampVelocityBy(() => engine.settings.constraints.range.maximum);
        }
        
        public static void ClampVelocityBy(this AngularPropulsionEngine engine, Func<float> selector)
        {
            if (!engine.settings.constraints.clamped)
            {
                return;
            }
            
            engine.settings.rigidbody.angularVelocity = Vector3.ClampMagnitude(
                engine.settings.rigidbody.angularVelocity, 
                selector()
            );
        }
        
        public static void Stabilisation(this AngularPropulsionEngine engine, Action action)
        {
            if (!engine.settings.constraints.stabilised)
            {
                return;
            }

            action();
        }

        public static void Acceleration(this AngularPropulsionEngine engine)
        {
            engine.settings.rigidbody.angularDrag = Mathf.Lerp(
                engine.settings.rigidbody.angularDrag, 
                engine.settings.constraints.range.minimum, 
                engine.settings.constraints.acceleration * Time.deltaTime
            );
        }
        
        public static void Deceleration(this AngularPropulsionEngine engine)
        {
            engine.settings.rigidbody.angularDrag = Mathf.Lerp(
                engine.settings.rigidbody.angularDrag, 
                engine.settings.constraints.range.maximum, 
                engine.settings.constraints.deceleration * Time.deltaTime
            );
        }
        
        public static void Compensation(this AngularPropulsionEngine engine, float amount)
        {
            engine.settings.constraints.compensators.ForEach(x => x.Compensate(amount));
        }
        
        public static void Compensation<T>(this AngularPropulsionEngine engine, float amount) where T : Compensator
        {
            engine.settings.constraints.compensators
                .OfType<T>()
                .ToList()
                .ForEach(x => x.Compensate(amount));
        }
        #endregion

        #region Information
        public static float NormalizeVelocityMagnitude(this AngularPropulsionEngine engine)
        {
            return MathHelper.Ratio(engine.settings.rigidbody.angularVelocity.magnitude, engine.settings.constraints.range.maximum);
        }
        #endregion

        #region Thrust
        public static void Thrust(this AngularPropulsionEngine engine, Vector3 direction)
        {
            var force = direction * engine.settings.constraints.speed;
            var mode = engine.settings.constraints.forceMode;
            var deltaTime = engine.settings.constraints.acceleration;

            switch (engine.settings.constraints.forceType)
            {
                case ForceType.Absolute:
                    engine.settings.rigidbody.AddTorque(force, mode);
                    break;
                case ForceType.Relative:
                    engine.settings.rigidbody.AddRelativeTorque(force, mode);
                    break;
                case ForceType.Manual:
                    engine.settings.rigidbody.MoveRotation(Quaternion.Lerp(
                        engine.settings.rigidbody.rotation, 
                        Quaternion.Euler(direction), 
                        deltaTime
                    ));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Collection
        public static IEnumerable<AngularPropulsionEngine> GetByAxis(this IEnumerable<AngularPropulsionEngine> engines, Axis axis)
        {
            return engines.Where(x => x.settings.axisMap.axis == axis);
        }
        
        public static IEnumerable<AngularPropulsionEngine> GetByAxes(this IEnumerable<AngularPropulsionEngine> engines, IEnumerable<Axis> axes)
        {
            return engines.Where(x => axes.Contains(x.settings.axisMap.axis));
        }

        public static IEnumerable<AngularPropulsionEngine> GetByInvertedAxis(this IEnumerable<AngularPropulsionEngine> engines, bool inverted)
        {
            return engines.Where(x => x.settings.axisMap.inverted == inverted);
        }
        #endregion
    }
}