using UnityEngine;

namespace Behaviours.Physics.Colliders
{
    public class ColliderEventPropagator : MonoBehaviour
    {
        public delegate void CollisionEventHander(Collision other);
        public delegate void ColliderEventHander(Collider other);

        public event CollisionEventHander CollisionEntered;
        public event CollisionEventHander CollisionExited;
        public event CollisionEventHander CollisionStayed;
        public event ColliderEventHander TriggerEntered;
        public event ColliderEventHander TriggerExited;
        public event ColliderEventHander TriggerStayed;
        
        private void OnCollisionEnter(Collision other)
        {
            this.CollisionEntered?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            this.CollisionExited?.Invoke(other);
        }

        private void OnCollisionStay(Collision other)
        {
            this.CollisionStayed?.Invoke(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            this.TriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.TriggerExited?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            this.TriggerStayed?.Invoke(other);
        }
    }
}