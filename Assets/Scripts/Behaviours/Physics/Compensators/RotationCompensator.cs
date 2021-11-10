using Systems.Transforms;
using Behaviours.Samplers;

namespace Behaviours.Physics.Compensators
{
    public abstract class RotationCompensator : Compensator
    {
        public float speed;
        public AxisMap direction;
        public PathSampler sampler;
    }
}