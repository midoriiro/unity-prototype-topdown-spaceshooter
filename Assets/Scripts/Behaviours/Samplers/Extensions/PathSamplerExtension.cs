using System.Linq;
using Systems.Math;
using Systems.Math.Extensions;
using UnityEngine;

namespace Behaviours.Samplers.Extensions
{
    public static class PathSamplerExtension
    {
        public static Vector3 SampleAtTime(this PathSampler sampler, float time)
        {
            var bezier = new Bezier {Points = sampler.Positions.ToList()};
            var position = bezier.PositionAtTime(time);
            return position;
        }
    }
}