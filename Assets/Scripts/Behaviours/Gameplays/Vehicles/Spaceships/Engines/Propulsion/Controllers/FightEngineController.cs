using System;
using System.Linq;
using Systems.Inputs;
using Systems.Inputs.Extensions;
using Systems.Transforms;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines.Fight
{
    public class FightEngineController : PropulsionController
    {
        private TwinInputAxis _leftStick;
        private TwinInputAxis _rightStick;
        private Vector3 _normalizedVelocity;
        private Vector3 _normalizedYaw;

        private void Start()
        {
            this._leftStick = this.controller.sticks.left;
            this._rightStick = this.controller.sticks.right;
            this._normalizedVelocity = this.axisMap.velocity.NormalizedMap();
            this._normalizedYaw = this.axisMap.yaw.NormalizedMap();
            
            var yaw = this.axisMap.yaw.Value(this.transform.rotation.eulerAngles);
            var direction = (Quaternion.AngleAxis(yaw, this._normalizedYaw) * this._normalizedVelocity).normalized;
            this._rightStick.horizontal.value = this._rightStick.horizontal.axisMap.Value(direction);
            this._rightStick.vertical.value = this._rightStick.vertical.axisMap.Value(direction);
        }

        private void Update()
        {
            this.ProcessRegularPropulsion(this._leftStick);
            this.ProcessAngularPropulsion(this._rightStick);
        }

        private void ProcessRegularPropulsion(TwinInputAxis input)
        {
            var propulsors = this.propulsors
                .OfType<PropulsionEngine>()
                .GetByAxes(input.Axes());

            foreach (var propulsor in propulsors)
            {
                propulsor.SetVelocity(input.Direction());
            }
        }
        
        private void ProcessAngularPropulsion(TwinInputAxis input)
        {
            var velocity = this.ProcessYaw(input);
            var propulsors = this.propulsors
                .OfType<AngularPropulsionEngine>()
                .GetByAxes(input.Axes());

            foreach (var propulsor in propulsors)
            {
                propulsor.SetVelocity(velocity);
            }
        }

        private Vector3 ProcessYaw(TwinInputAxis input)
        {
            var direction = input.Direction();
            var angle = Vector3.SignedAngle(
                this._normalizedVelocity,
                direction,
                this._normalizedYaw
            );
            return this.axisMap.yaw.Map(direction, angle);
        }
    }
}