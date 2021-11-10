using Systems.Optimisations;
using Behaviours.Gameplays.Weapons.Commons;
using Behaviours.Gameplays.Weapons.Commons.Extensions;
using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons
{
    public class EnergyBlastWeapon : EnergyWeapon
    {
        public float range;
        public float rate;
        
        private float _nextTimeToFire;
        
        public delegate void WeaponEventHandler(EnergyBlastWeapon sender);
        public delegate void WeaponRayCastEventHandler(EnergyBlastWeapon sender);
        
        public event WeaponRayCastEventHandler Firing;
        public event WeaponEventHandler FireCeased;
        
        private void Start()
        {
            this.direction.position.Normalize();
            this.overheat.Cooldown = () => this.cooldown.Cooling(this.overheat);
            this.Firing += sender => this.overheat.AddOverheating();
        }
        
        public override void Fire()
        {
            if (this.overheat.IsInOverheat)
            {
                this.CeaseFire();
                return;
            }
            
            var now = Time.time;

            if (now < this._nextTimeToFire)
            {
                return;
            }

            this._nextTimeToFire = now + 1f / this.rate;
            
            this.Firing?.Invoke(this);
        }

        public override void CeaseFire()
        {
            this.FireCeased?.Invoke(this);
        }

        public void Damage(Transform transform)
        {
            var target = TransformCache.Get<IDamageable>(transform);
            target?.AddDamaging(this.damage.RandomWithCritical(Critical.Default));
        }
    }
}