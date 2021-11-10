using Behaviours.Gameplays.Inputs;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings;
using UnityEngine;
using RigidbodyConstraints = Behaviours.Physics.Rigidbody.RigidbodyConstraints;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public abstract class Engine : MonoBehaviour
    {
        public new Rigidbody rigidbody;
        public PropulsionAxisMap axisMap;
        public RigidbodyConstraints velocityConstraints;
        public RigidbodyConstraints angularVelocityConstraints;
        public GamePadInputController controller;
        
        // TODO decorelate engine and controller
    }
}