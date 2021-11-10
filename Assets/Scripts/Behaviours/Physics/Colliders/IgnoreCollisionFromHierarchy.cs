using Core.Extensions;
using UnityEngine;

namespace Behaviours.Physics.Colliders
{
    public class IgnoreCollisionFromHierarchy : MonoBehaviour
    {
        private void Start()
        {
            this.gameObject.IgnoreCollisionFromHierarchy();
        }
    }
}