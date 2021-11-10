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
    public static class PropulsionEngineExtension
    {
        #region Constraints
        public static void ClampVelocity(this PropulsionEngine engine)
        {
            if (!engine.settings.constraints.clamped)
            {
                return;
            }

            engine.ClampVelocityBy(() => engine.settings.constraints.range.maximum);
        }
        
        public static void ClampVelocityBy(this PropulsionEngine engine, Func<float> selector)
        {           
            engine.settings.rigidbody.velocity = Vector3.ClampMagnitude(
                engine.settings.rigidbody.velocity, 
                selector()
            );
        }

        public static void Stabilisation(this PropulsionEngine engine, Action action)
        {
            if (!engine.settings.constraints.stabilised)
            {
                return;
            }

            action();
        }

        public static void Acceleration(this PropulsionEngine engine)
        {
            engine.settings.rigidbody.drag = Mathf.Lerp(
                engine.settings.rigidbody.drag, 
                engine.settings.constraints.range.minimum, 
                engine.settings.constraints.acceleration * Time.deltaTime
            );
        }
        
        public static void Deceleration(this PropulsionEngine engine)
        {
            engine.settings.rigidbody.drag = Mathf.Lerp(
                engine.settings.rigidbody.drag, 
                engine.settings.constraints.range.maximum, 
                engine.settings.constraints.deceleration * Time.deltaTime
            );
        }

        public static void Compensation(this PropulsionEngine engine, float amount)
        {
            engine.settings.constraints.compensators.ForEach(x => x.Compensate(amount));
        }
        
        public static void Compensation<T>(this PropulsionEngine engine, float amount) where T : Compensator
        {
            engine.settings.constraints.compensators
                .OfType<T>()
                .ToList()
                .ForEach(x => x.Compensate(amount));
        }
        #endregion

        #region Information
        public static float NormalizeVelocityMagnitude(this PropulsionEngine engine)
        {
            return MathHelper.Ratio(engine.settings.rigidbody.velocity.magnitude, engine.settings.constraints.range.maximum);
        }
        #endregion

        #region Thrust       
        public static void Thrust(this PropulsionEngine engine, Vector3 direction)
        {
            var force = direction * engine.settings.constraints.speed;
            var mode = engine.settings.constraints.forceMode;

            switch (engine.settings.constraints.forceType)
            {
                case ForceType.Absolute:
                    engine.settings.rigidbody.AddForce(force, mode);
                    break;
                case ForceType.Relative:
                    engine.settings.rigidbody.AddRelativeForce(force, mode);
                    break;
                case ForceType.Manual:
                    engine.settings.rigidbody.MovePosition(force);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Collection
        public static IEnumerable<PropulsionEngine> GetByAxis(this IEnumerable<PropulsionEngine> engines, Axis axis)
        {
            return engines.Where(x => x.settings.axisMap.axis == axis);
        }
        
        public static IEnumerable<PropulsionEngine> GetByAxes(this IEnumerable<PropulsionEngine> engines, IEnumerable<Axis> axes)
        {
            return engines.Where(x => axes.Contains(x.settings.axisMap.axis));
        }

        public static IEnumerable<PropulsionEngine> GetByInvertedAxis(this IEnumerable<PropulsionEngine> engines, bool inverted)
        {
            return engines.Where(x => x.settings.axisMap.inverted == inverted);
        }
        #endregion
    }
}