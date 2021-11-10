using System;
using Systems.Optimisations;
using Behaviours.Gameplays.Weapons.Commons;
using Behaviours.Gameplays.Weapons.Commons.Extensions;
using Core.Extensions;
using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons
{
    public class EnergyRayCastWeapon : EnergyWeapon
    {
        public float range;

        public delegate void WeaponEventHandler(EnergyRayCastWeapon sender);
        public delegate void WeaponRayCastEventHandler(EnergyRayCastWeapon sender, RaycastHit? hit);
        
        public event WeaponRayCastEventHandler Firing;
        public event WeaponEventHandler FireCeased;

        private void Start()
        {
            this.direction.position.Normalize();
            this.overheat.Cooldown = () => this.cooldown.Cooling(this.overheat);
            this.Firing += (sender, hit) => this.overheat.AddOverheating();
        }

        public override void Fire()
        {
            if (this.overheat.IsInOverheat)
            {
                this.CeaseFire();
                return;
            }
            
            var origin = this.origin.position;
            var direction = this.direction.position - origin;
            var hits = UnityEngine.Physics.RaycastAll(origin, direction, this.range);

            if (hits.Length == 0)
            {
                this.Firing?.Invoke(this, null);
                return;
            }
                
            var hit = hits
                .ExcludeHierarchy(this.transform.root.gameObject)
                .MinBy(x => x.distance);
                
            if (hit.transform != null)
            {
                var target = TransformCache.Get<IDamageable>(hit.transform);
                target?.AddDamaging(this.damage.RandomWithCritical(Critical.Default));
                this.Firing?.Invoke(this, hit);    
            }
            else
            {
                this.Firing?.Invoke(this, null); 
            }
        }

        public override void CeaseFire()
        {
            this.FireCeased?.Invoke(this);
        }
    }
}