using System;
using Systems.Transforms;
using UnityEngine;
using RigidbodyConstraints = Behaviours.Physics.Rigidbody.RigidbodyConstraints;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings
{
    [Serializable]
    public class AngularPropulsionEngineSettings
    {
        public AxisMap axisMap;
        public AxisMap angularAxisMap;
        public Rigidbody rigidbody;
        public RigidbodyConstraints constraints;
    }
}