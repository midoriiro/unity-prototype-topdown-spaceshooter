using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBeam.Effects
{
    public class LaserBeamWidthEffect : LaserBeamEffect
    {
        private float _startWidth;
        private float _endWidth;

        public override void Initialisation()
        {
            this._startWidth = this.laserBeam.lineRenderer.startWidth;
            this._endWidth = this.laserBeam.lineRenderer.endWidth;
            this.laserBeam.lineRenderer.startWidth = 0f;
            this.laserBeam.lineRenderer.endWidth = 0f;
        }

        public override void OnFiring(float deltaTime)
        {
            this.laserBeam.lineRenderer.startWidth = Mathf.Lerp(this.laserBeam.lineRenderer.startWidth, this._startWidth, deltaTime);
            this.laserBeam.lineRenderer.endWidth = Mathf.Lerp(this.laserBeam.lineRenderer.endWidth, this._endWidth, deltaTime);
        }
           
        public override void OnFireCeased(float deltaTime)
        {
            this.laserBeam.lineRenderer.startWidth = Mathf.Lerp(this.laserBeam.lineRenderer.startWidth, 0f, deltaTime);
            this.laserBeam.lineRenderer.endWidth = Mathf.Lerp(this.laserBeam.lineRenderer.endWidth, 0f, deltaTime);
        }
    }
}