using System;
using System.Collections.Generic;
using Systems.Helpers;
using Core.Delegates;
using Core.Extensions;
using UnityEngine;
using Particle = UnityEngine.ParticleSystem.Particle;

namespace Behaviours.Effects.Particles.Extensions
{
    public static class StardustFieldExtension
    {
        public static StarDustField AddStardusts(this StarDustField field, int capacity, Func<Particle> generator)
        {
            var particles = new List<Particle>(capacity);

            for (var i = 0; i < particles.Capacity; i++)
            {
                particles.Add(generator());
            }
            
            field.particles.AddRange(particles);

            return field;
        }

        public static StarDustField AttatchToLineRenderers(this StarDustField field, Action<LineRenderer, Particle> configurator)
        {
            field.particles.ForEach(particle =>
            {
                var index = field.particles.IndexOf(particle);
                var lineRenderer = field.gameObject.GetOrCreateLineRenderer($"stardust_{index}");
                
                configurator(lineRenderer, particle);
                
                field.lineRenderers.Insert(index, lineRenderer);
            });

            return field;
        }

        public static bool IsOutOfBound(this StarDustField field, Vector3 position)
        {
            return Vector3.Distance(position, field.follower.transform.position) > field.outerRadius;
        }
        
        public static Vector3 RandomPosition(this StarDustField field)
        {           
            var center = field.follower.transform.position;         
            var position = RandomHelper.InsideTwoUnitSpheres(field.innerRadius, field.outerRadius);
//            var normal = position.PerpendicularClockwise(center, Vector3.forward);
//            position = Quaternion.AngleAxis(Random.Range(-90f, 90f), normal) * position;           
            return position + center;
        }

        public static void UpdateStardusts(this StarDustField field, ActionRef<Particle> configurator)
        {
            field.particles.ForEach((ref Particle particle) =>
            {
                var index = field.particles.IndexOf(particle);

                if (field.IsOutOfBound(particle.position))
                {
                    particle.position = field.RandomPosition();
                }

                configurator(ref particle);
            });
        }

        public static void UpdateLineRenderers(this StarDustField field, ActionRef<LineRenderer> configurator)
        {
            var referenceVelocity = field.reference.GetComponent<Rigidbody>().velocity;
            var trailerPosition = referenceVelocity * Time.deltaTime;

            field.particles.ForEach((ref Particle particle) =>
            {
                var index = field.particles.IndexOf(particle);
                var lineRenderer = field.lineRenderers[index];

                if (field.IsOutOfBound(particle.position))
                {
                    particle.position = field.RandomPosition();
                }

                configurator(ref lineRenderer);
                
//                lineRenderer.Reset();
//                lineRenderer.positionCount = 1;
//                lineRenderer.SetPosition(0, particle.position);
//                lineRenderer.SetPosition(1, particle.position + trailerPosition);


//                var velocityVectors = new List<Vector3>(field.velocityTrailer);
//                velocityVectors = velocityVectors
//                    .Select(x => x * Time.deltaTime)
//                    .Prepend(particle.position)
//                    .ToList();
//                
//                for(var i = 0 ; i < velocityVectors.Count ; i++)
//                {
//                    if (i == 0)
//                    {
//                        continue;
//                    }
//
//                    var first = velocityVectors[i - 1];
//                    var second = velocityVectors[i];
//
//                    velocityVectors[i - 1] = first;
//                    velocityVectors[i] = first + second;
//                }
//                
//                lineRenderer.positionCount = velocityVectors.Count - 1;
//                lineRenderer.SetPositions(velocityVectors.ToArray());

//                field.particles[index] = particle;
            });
        }
    }
}