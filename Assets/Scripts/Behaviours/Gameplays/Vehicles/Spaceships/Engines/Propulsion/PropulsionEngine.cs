using System;
using Systems.Transforms;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings;
using UnityEngine;
using RigidbodyConstraints = Behaviours.Physics.Rigidbody.RigidbodyConstraints;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public class PropulsionEngine : BaseEngine
    {
        public PropulsionEngineSettings settings;

        protected override void Start()
        {
            this.direction = this.settings.axisMap.NormalizedMap();
            this.settings.axisMap.inverted = false;
        }

        protected override void FixedUpdate()
        {
            this.onFixedUpdate(this);
        }

        public override void SetVelocity(Vector3 velocity)
        {
            var magnitude = this.settings.axisMap.Value(velocity);
            this.IsPropelling = magnitude < 0f || 0f < magnitude;
            this.direction = this.settings.axisMap.Map(this.direction, magnitude);
            this.NormalizedMagnitude = magnitude;
            this.NormalizedVelocity = velocity;
        }

        protected override void Thrust()
        {
            this.Thrust(this.direction);
            this.Acceleration();
        }

        protected override void Stabilise()
        {
            this.Stabilisation(this.Deceleration);
        }

        protected override void Clamp()
        {
            this.ClampVelocity();
        }

        protected override void NormalizeMagnitude()
        {
            this.NormalizedMagnitude = this.settings.axisMap.Value(this.direction);
        }
    }
}