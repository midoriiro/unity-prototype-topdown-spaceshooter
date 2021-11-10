using Systems.Transforms.Extensions;
using Behaviours.Samplers.Extensions;
using UnityEngine;

namespace Behaviours.Physics.Compensators
{
    public class YawRotationCompensator : RotationCompensator
    {
        public override void Compensate(float amount)
        {
            var transform = this.transform;
            var position = this.sampler.SampleAtTime(amount);
            var direction = (transform.position - position).normalized;
            direction = Vector3.Lerp(this.direction.TransformMap(transform), direction, this.speed * Time.deltaTime);
            this.direction.TransformMap(transform, direction);
        }
    }
}