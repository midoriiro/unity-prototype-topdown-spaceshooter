using Systems.Inputs.Extensions;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using Vector3 = UnityEngine.Vector3;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public class FightEngine : Engine
    {
        private void FixedUpdate()
        {
            if (!this.controller.sticks.left.IsInDeadZone())
            {
                this.ThrustPropulsionEngine(this.controller.sticks.left.Direction());
                this.Acceleration();
            }
            else
            {
                this.VelocityStabilisation(() => this.Deceleration());
            }

            if (!this.controller.sticks.right.IsInDeadZone())
            {
                var direction = this.controller.sticks.right.Direction();
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
                this.AngularStabilisation(() => this.AngularDeceleration());
            }
            
            this.ClampVelocity()
                .ClampAngularVelocity();
        }
    }
}