using Systems.Helpers;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBeam.Effects
{
    public class LaserBeamEffectCollider : MonoBehaviour
    {
        public new CapsuleCollider collider;
        public LaserBeam effect;

        private void Start()
        {
            this.transform.root.gameObject
                .FindChildByTag("ForceField") // TODO find another place 
                .IgnoreCollisionFromGameObject(this.collider);
        }

        private void Update()
        {
            var range = this.effect.Range * MathHelper.SpaceConstant; // TODO what is that ?
            var center = range / 2f + this.effect.weapon.origin.localPosition.magnitude;
            this.collider.center = new Vector3(0, center, 0);
            this.collider.height = range;
        }
    }
}