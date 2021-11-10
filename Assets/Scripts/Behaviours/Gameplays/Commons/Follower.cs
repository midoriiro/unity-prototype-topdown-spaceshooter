using Systems.Transforms;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Commons
{
    public class Follower : MonoBehaviour
    {
        public GameObject target;
        public float speed;
        public FreezeAxis freezeAxis;
        public bool drawGizmos;

        private void FixedUpdate()
        {
            var step = speed * Time.deltaTime;
            var position = this.transform.position;
            var targetPosition = this.target.transform.position + this.target.transform.up;
            var interpolatedPosition = Vector3.Lerp(position, targetPosition, step);

            if (this.freezeAxis.x)
            {
                interpolatedPosition.x = position.x;
            }
        
            if (this.freezeAxis.y)
            {
                interpolatedPosition.y = position.y;
            }
        
            if (this.freezeAxis.z)
            {
                interpolatedPosition.z = position.z;
            }

            this.transform.position = interpolatedPosition;

            if (this.drawGizmos)
            {
                this.gameObject.DrawCircle(
                    "bound",
                    (Vector2)targetPosition, 
                    Vector3.up * 2, 
                    Vector3.forward, 2
                );   
            }        
        }
    }
}
