using UnityEngine;

namespace Behaviours.Physics.Colliders
{
    public class IgnoreLayerCollision : MonoBehaviour
    {
        public new string name;

        private void Start()
        {
            UnityEngine.Physics.IgnoreLayerCollision(LayerMask.NameToLayer(this.name), LayerMask.NameToLayer(this.name));
        }
    }
}