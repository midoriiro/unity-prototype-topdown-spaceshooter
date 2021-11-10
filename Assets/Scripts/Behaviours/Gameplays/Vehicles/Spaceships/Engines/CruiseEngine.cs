using Systems.Inputs.Extensions;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using Behaviours.Physics.Compensators;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public class CruiseEngine : Engine
    {
        private void FixedUpdate()
        {       
            if (!this.controller.sticks.left.IsInDeadZone())
            {
                this.ThrustPropulsionEngine(
                    this.axisMap.velocity.NormalizedMap() * this.controller.sticks.left.Direction().magnitude
                );
                this.Acceleration();
                
                var direction = this.controller.sticks.left.Direction();
                var angle = Vector3.SignedAngle(
                    this.axisMap.velocity.NormalizedMap(),
                    direction,
                    this.axisMap.yaw.NormalizedMap()
                );
                
                this.ThrustAngularPropulsionEngine(this.axisMap.yaw.Map(this.rigidbody.rotation.eulerAngles, angle));
                this.AngularAcceleration();
            }
            else
            {
                this.VelocityStabilisation(() => this.Deceleration());
                this.AngularStabilisation(() => this.AngularDeceleration());
            }
            
            this.Compensation(velocityConstraints.compensation)
                .AngularCompensation<YawRotationCompensator>(this.angularVelocityConstraints.compensation)
                .ClampVelocity()
                .ClampAngularVelocity();
        }
    }
}
