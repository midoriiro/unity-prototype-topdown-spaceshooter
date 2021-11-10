using System.Collections.Generic;
using Behaviours.Effects.Particles.Extensions;
using Behaviours.Gameplays.Commons;
using Core.Extensions;
using UnityEngine;
using Particle = UnityEngine.ParticleSystem.Particle;
using Random = UnityEngine.Random;

namespace Behaviours.Effects.Particles
{
    public class StarDustField : MonoBehaviour
    {
        public int capacity;
        public float innerRadius;
        public float outerRadius;
        public GameObject reference;
        public bool drawGizmos;

        [HideInInspector] public List<Particle> particles;
        [HideInInspector] public List<LineRenderer> lineRenderers;
        [HideInInspector] public Follower follower;
        [HideInInspector] public Queue<Vector3> velocityTrailer;
        
        private ParticleSystem _particleSystem;
        private Rigidbody _referenceRigidbody;

        private void Start()
        {
            this.particles = new List<Particle>();
            this.lineRenderers = new List<LineRenderer>();
            this.follower = this.GetComponent<Follower>();
            this.velocityTrailer = new Queue<Vector3>();
            this._particleSystem = this.gameObject.GetComponent<ParticleSystem>();
            this._referenceRigidbody = this.reference.GetComponent<Rigidbody>();

            this.AddStardusts(this.capacity, () => new Particle
            {
                position = this.RandomPosition(),
                startSize = Random.Range(10f, 10f),
                startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f)
            });//.AttatchToLineRenderers((lineRenderer, particle) =>
//            {
//                lineRenderer.useWorldSpace = true;
//                lineRenderer.startWidth = particle.startSize / 4f;
//                lineRenderer.endWidth = 0f;
//            });
        }

        private void Update()
        {
            var referenceVelocity = this._referenceRigidbody.velocity;

            if (this.velocityTrailer.Count > 10)
            {
                this.velocityTrailer.Dequeue();
            }

            this.velocityTrailer.Enqueue(referenceVelocity);

            this.UpdateStardusts((ref Particle particle) =>
            {              
//                if (particle.startColor.a < 1f)
//                {
//                    var currentColor = particle.startColor;
//                    particle.startColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.1f);
//                }  
//                else if(particle.startColor.a > 1f)
//                {
//                    particle.startColor = new Color(1, 1, 1, 1);
//                }
//                if (particle.startLifetime < 1f)
//                {
//                    particle.startLifetime += 0.01f;
//                }
            });
            
//            this.UpdateLineRenderers((ref LineRenderer lineRenderer) =>
//            {
                //                lineRenderer.endWidth = referenceVelocity.normalized.magnitude * lineRenderer.startWidth;
//                lineRenderer.startColor = particle.startColor;
//                lineRenderer.endColor = particle.startColor;
//            });
            
            this._particleSystem.SetParticles(this.particles.ToArray());

            if (this.drawGizmos)
            {
                var position = this.transform.position;
                
                this.gameObject.DrawCircle(
                    "inner_bound",
                    position, 
                    Vector3.up * this.innerRadius, 
                    Vector3.forward
                );
                
                this.gameObject.DrawCircle(
                    "outer_bound",
                    position, 
                    Vector3.up * this.outerRadius, 
                    Vector3.forward
                );
            }
        }
    }
}