using System.Linq;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBlaster.Effects
{
    public class LaserBlasterEffectCollider : MonoBehaviour
    {
        public new CapsuleCollider collider;
        public LaserBlasterEffect effect;
        
        private void Start()
        {
            var root = this.transform.root.gameObject;
            
            root.FindChildByTag("ForceField")
                .IgnoreCollisionFromGameObject(this.collider);
            
            root.FindChildrenByTag("Projectile")
                .Where(x => x.activeSelf)
                .ForEach(x => UnityEngine.Physics.IgnoreCollision(x.GetComponentInChildren<Collider>(), collider));
            
            this.collider.transform.localEulerAngles = -this.effect.transform.localRotation.eulerAngles;   
            this.collider.center = new Vector3(0, this.effect.size / 2f, 0);
            this.collider.height = this.effect.size;
        }

        private void Update()
        {
            this.collider.transform.position = this.effect.rigidbody.position;
        }
    }
}