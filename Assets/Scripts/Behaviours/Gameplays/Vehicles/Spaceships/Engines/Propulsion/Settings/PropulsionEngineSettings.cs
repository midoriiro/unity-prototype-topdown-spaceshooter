using System;
using Systems.Transforms;
using UnityEngine;
using RigidbodyConstraints = Behaviours.Physics.Rigidbody.RigidbodyConstraints;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings
{
    [Serializable]
    public class PropulsionEngineSettings
    {
        public AxisMap axisMap;
        public Rigidbody rigidbody;
        public RigidbodyConstraints constraints;
    }
}