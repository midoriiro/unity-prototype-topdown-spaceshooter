using System;
using Systems.Transforms;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings;
using UnityEngine;
using RigidbodyConstraints = Behaviours.Physics.Rigidbody.RigidbodyConstraints;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public abstract class BaseEngine : MonoBehaviour
    {
        public bool IsPropelling { get; protected set; }
        public float NormalizedMagnitude { get; protected set; }
        public Vector3 NormalizedVelocity { get; protected set; }
        
        protected Vector3 direction;

        protected readonly Action<BaseEngine> onFixedUpdate = engine =>
        {
            if (engine.IsPropelling)
            {
                engine.Thrust();
            }
            else
            {
                engine.Stabilise();
            }

            engine.Clamp();
        };

        protected abstract void Start();
        protected abstract void FixedUpdate();
        public abstract void SetVelocity(Vector3 velocity);
        protected abstract void Thrust();
        protected abstract void Stabilise();
        protected abstract void Clamp();
        protected abstract void NormalizeMagnitude();
    }
}