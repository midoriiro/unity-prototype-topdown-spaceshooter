using Behaviours.Physics.Colliders;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBlaster.Effects
{
    public class LaserBlasterEffect : MonoBehaviour
    {
        public float speed;
        public float size;
        public ColliderEventPropagator colliderEventPropagator;
        public new Rigidbody rigidbody;
        public Rigidbody reference;
        public LineRenderer lineRenderer;

        public LaserBlaster LaserBlaster { get; set; }
        
        private float _timeSinceFired;

        private void Start()
        {
            var transform = this.transform;
            var origin = this.LaserBlaster.weapon.origin.position;
            var direction = (this.LaserBlaster.weapon.direction.position - origin).normalized;
            
            transform.position = origin;

            this.lineRenderer.positionCount = 2;
            this.lineRenderer.SetPositions(new []
            {
                Vector3.zero, 
                Vector3.zero + direction * this.size
            });
            
            this.rigidbody.velocity = this.reference.velocity;
            this.rigidbody.angularVelocity = this.reference.angularVelocity;
            this.rigidbody.AddForce(this.speed * direction, ForceMode.Impulse);
            this.colliderEventPropagator.TriggerEntered += other => this.DamageAndDestroy(other.transform);
            this.colliderEventPropagator.CollisionEntered += other => this.DamageAndDestroy(other.collider.transform);
            this._timeSinceFired = Time.time;
        }

        private void Update()
        {
            var now = Time.time;
            var delta = now - this._timeSinceFired;
            
            if (delta > this.LaserBlaster.weapon.range)
            {
                this.Destroy();
            }
        }

        private void DamageAndDestroy(Transform transform)
        {
            this.LaserBlaster.weapon.Damage(transform);
            this.Destroy();
        }

        private void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}