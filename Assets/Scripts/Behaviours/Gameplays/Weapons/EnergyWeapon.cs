using Systems.Optimisations;
using Behaviours.Gameplays.Commons;
using Behaviours.Gameplays.Weapons.Commons;
using Behaviours.Gameplays.Weapons.Commons.Extensions;
using Core.Extensions;
using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons
{
    public abstract class EnergyWeapon : MonoBehaviour
    {
        public Damage damage;
        public Transform origin;
        public Transform direction;
        public Overheat overheat;
        public Cooldown cooldown;

        public abstract void Fire();

        public abstract void CeaseFire();
    }

}