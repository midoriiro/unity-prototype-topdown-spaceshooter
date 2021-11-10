using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        private List<EnergyWeapon> _weapons;
        
        private void Start()
        {
            this._weapons = this.transform.GetComponentsInChildren<EnergyWeapon>().ToList();
        }

        private void Update()
        {
            // TODO Refactor this
            this._weapons.ForEach(x => x.Fire());
            
            // if (Input.GetAxis("Left Trigger") > 0f)
            // {
            //     this._weapons.ForEach(x => x.Fire());
            // }
            // else
            // {
            //     this._weapons.ForEach(x => x.CeaseFire());
            // }
        }
    }
}