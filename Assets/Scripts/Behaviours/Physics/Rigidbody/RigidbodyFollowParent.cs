using UnityEngine;

namespace Behaviours.Physics.Rigidbody
{
    public class RigidbodyFollowParent : MonoBehaviour
    {
        public new UnityEngine.Rigidbody rigidbody;

        private void FixedUpdate()
        {
            var parent = this.transform.parent;
            this.rigidbody.MovePosition(parent.position);
            this.rigidbody.MoveRotation(parent.rotation);
        }
    }
}