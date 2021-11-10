using Systems.Helpers;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBeam.Effects
{
    public class LaserBeamAlphaEffect : LaserBeamEffect
    {
        private Material _material;

        public override void Initialisation()
        {
            this._material = this.laserBeam.lineRenderer.material;
            this._material.SetFloat("_time", 0);
        }

        public override void OnFiring(float deltaTime)
        {
            this._material.SetFloat("_time", deltaTime);
        }

        public override void OnFireCeased(float deltaTime)
        {
            this._material.SetFloat(
                "_time", 
                MathHelper.OneMinus(deltaTime)
            ); 
        }
    }
}