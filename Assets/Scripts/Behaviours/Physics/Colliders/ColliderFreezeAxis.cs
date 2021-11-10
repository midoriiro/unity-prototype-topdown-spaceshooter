using Systems.Transforms;
using Systems.Transforms.Extensions;
using UnityEngine;

namespace Behaviours.Physics.Colliders
{
    public class ColliderFreezeAxis : MonoBehaviour
    {
        public FreezeAxis freezePosition;
        public FreezeAxis freezeRotation;
        public UnityEngine.Rigidbody reference;

        private void FixedUpdate()
        {
            var referenceTransform = this.reference.transform;
            var position = referenceTransform.position;
            var localPosition = referenceTransform.localPosition;
            position = this.freezePosition.Freeze(position);
            localPosition = this.freezePosition.Freeze(localPosition);
            referenceTransform.position = position;
            referenceTransform.localPosition = localPosition;

            var eulerAngles = referenceTransform.eulerAngles;
            var localEulerAngles = referenceTransform.localEulerAngles;
            eulerAngles = this.freezeRotation.Freeze(eulerAngles);
            localEulerAngles = this.freezeRotation.Freeze(localEulerAngles);
            referenceTransform.eulerAngles = eulerAngles;
            referenceTransform.localEulerAngles = localEulerAngles;
        }
    }
}