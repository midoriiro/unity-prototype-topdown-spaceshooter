using System;
using System.Linq;
using Systems.Transforms;
using Behaviours.Physics.Compensators;
using Core.Enums;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions
{
    public static class SpaceshipEngineExtension
    {
        #region Constraints
        public static Engine ClampVelocity(this Engine engine)
        {
            if (!engine.velocityConstraints.clamped)
            {
                return engine;
            }

            engine.ClampVelocityBy(() => engine.velocityConstraints.range.maximum);

            return engine;
        }
        
        public static Engine ClampVelocityBy(this Engine engine, Func<float> selector)
        {           
            engine.rigidbody.velocity = Vector3.ClampMagnitude(
                engine.rigidbody.velocity, 
                selector()
            );

            return engine;
        }
        
        public static Engine ClampAngularVelocity(this Engine engine)
        {
            if (!engine.angularVelocityConstraints.clamped)
            {
                return engine;
            }

            engine.ClampAngularVelocityBy(() => engine.angularVelocityConstraints.range.maximum);
            
            return engine;
        }
        
        public static Engine ClampAngularVelocityBy(this Engine engine, Func<float> selector)
        {
            if (!engine.angularVelocityConstraints.clamped)
            {
                return engine;
            }
            
            engine.rigidbody.angularVelocity = Vector3.ClampMagnitude(
                engine.rigidbody.angularVelocity, 
                selector()
            ); 
            
            return engine;
        }
        
        public static Engine VelocityStabilisation(this Engine engine, Action action)
        {
            if (!engine.velocityConstraints.stabilised)
            {
                return engine;
            }

            action();

            return engine;
        }
        
        public static Engine AngularStabilisation(this Engine engine, Action action)
        {
            if (!engine.angularVelocityConstraints.stabilised)
            {
                return engine;
            }

            action();

            return engine;
        }
        
        public static Engine Acceleration(this Engine engine)
        {
            engine.rigidbody.drag = Mathf.Lerp(
                engine.rigidbody.drag, 
                engine.velocityConstraints.range.minimum, 
                engine.velocityConstraints.acceleration * Time.deltaTime
            );
            
            return engine;
        }
        
        public static Engine Deceleration(this Engine engine)
        {
            engine.rigidbody.drag = Mathf.Lerp(
                engine.rigidbody.drag, 
                engine.velocityConstraints.range.maximum, 
                engine.velocityConstraints.deceleration * Time.deltaTime
            );
            
            return engine;
        }
    
        public static Engine AngularAcceleration(this Engine engine)
        {
            engine.rigidbody.angularDrag = Mathf.Lerp(
                engine.rigidbody.angularDrag, 
                engine.angularVelocityConstraints.range.minimum, 
                engine.angularVelocityConstraints.acceleration * Time.deltaTime
            );
            
            return engine;
        }
        
        public static Engine AngularDeceleration(this Engine engine)
        {
            engine.rigidbody.angularDrag = Mathf.Lerp(
                engine.rigidbody.angularDrag, 
                engine.angularVelocityConstraints.range.maximum, 
                engine.angularVelocityConstraints.deceleration * Time.deltaTime
            );
            
            return engine;
        }
        
        public static Engine Compensation(this Engine engine, float amount)
        {
            engine.velocityConstraints.compensators.ForEach(x => x.Compensate(amount));
            
            return engine;
        }
        
        public static Engine Compensation<T>(this Engine engine, float amount) where T : Compensator
        {
            engine.velocityConstraints.compensators
                .OfType<T>()
                .ToList()
                .ForEach(x => x.Compensate(amount));
            
            return engine;
        }

        public static Engine AngularCompensation(this Engine engine, float amount)
        {
            engine.angularVelocityConstraints.compensators.ForEach(x => x.Compensate(amount));
            
            return engine;
        }
        
        public static Engine AngularCompensation<T>(this Engine engine, float amount) where T : Compensator
        {
            engine.angularVelocityConstraints.compensators
                .OfType<T>()
                .ToList()
                .ForEach(x => x.Compensate(amount));
            
            return engine;
        }
        #endregion

        #region Thrust       
        public static Engine ThrustPropulsionEngine(this Engine engine, Vector3 direction)
        {
            var force = direction * engine.velocityConstraints.speed;
            var mode = engine.velocityConstraints.forceMode;

            switch (engine.velocityConstraints.forceType)
            {
                case ForceType.Absolute:
                    engine.rigidbody.AddForce(force, mode);
                    break;
                case ForceType.Relative:
                    engine.rigidbody.AddRelativeForce(force, mode);
                    break;
                case ForceType.Manual:
                    engine.rigidbody.MovePosition(force);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return engine;
        }
        
        public static Engine ThrustAngularPropulsionEngine(this Engine engine, Vector3 direction)
        {
            var force = direction * engine.angularVelocityConstraints.speed;
            var mode = engine.angularVelocityConstraints.forceMode;
            var deltaTime = engine.angularVelocityConstraints.acceleration;

            switch (engine.angularVelocityConstraints.forceType)
            {
                case ForceType.Absolute:
                    engine.rigidbody.AddTorque(force, mode);
                    break;
                case ForceType.Relative:
                    engine.rigidbody.AddRelativeTorque(force, mode);
                    break;
                case ForceType.Manual:
                    engine.rigidbody.MoveRotation(Quaternion.Lerp(
                        engine.rigidbody.rotation, 
                        Quaternion.Euler(direction), 
                        deltaTime
                    ));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return engine;
        }
        
        public static Engine ThrustToYaw(this Engine engine, Func<AxisMap, Vector3> functor)
        {
            return ThrustAngularPropulsionEngine(engine, functor(engine.axisMap.yaw));
        }
        
        public static Engine ThrustToInvertedYaw(this Engine engine, Func<AxisMap, Vector3> functor)
        {
            return ThrustAngularPropulsionEngine(engine, -functor(engine.axisMap.yaw));
        }
        
        public static Engine ThrustToPitch(this Engine engine, Func<AxisMap, Vector3> functor)
        {
            return ThrustAngularPropulsionEngine(engine, functor(engine.axisMap.pitch));
        }
        
        public static Engine ThrustToInvertedPitch(this Engine engine, Func<AxisMap, Vector3> functor)
        {
            return ThrustAngularPropulsionEngine(engine, -functor(engine.axisMap.pitch));
        }
        
        public static Engine ThrustToRoll(this Engine engine, Func<AxisMap, Vector3> functor)
        {
            return ThrustAngularPropulsionEngine(engine, functor(engine.axisMap.roll));
        }
        
        public static Engine ThrustToInvertedRoll(this Engine engine, Func<AxisMap, Vector3> functor)
        {
            return ThrustAngularPropulsionEngine(engine, -functor(engine.axisMap.roll));
        }
        #endregion
    }
}