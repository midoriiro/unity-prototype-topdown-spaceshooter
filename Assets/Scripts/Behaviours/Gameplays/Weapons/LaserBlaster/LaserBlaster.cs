using Behaviours.Gameplays.Weapons.LaserBlaster.Effects;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBlaster
{
    public class LaserBlaster : MonoBehaviour
    {
        // TODO required 
        public GameObject ammo;
        public EnergyBlastWeapon weapon;

        private void Start()
        {
            this.weapon.Firing += sender =>
            {
                var effect = Instantiate(this.ammo).GetComponent<LaserBlasterEffect>();
                effect.LaserBlaster = this;
                effect.transform.position = this.weapon.origin.position;
                effect.transform.SetParent(this.weapon.transform);
                effect.gameObject.SetActive(true);
            };
        }
    }
}