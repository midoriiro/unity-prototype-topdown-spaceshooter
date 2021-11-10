using System.Collections.Generic;
using Behaviours.Gameplays.Weapons.LaserBeam.Effects;
using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBeam
{
    public class LaserBeam : MonoBehaviour
    {
        public float speed;
        public EnergyRayCastWeapon weapon;
        public LineRenderer lineRenderer;
        public List<LaserBeamEffect> effects;

        public float Range { get; private set; }
        
        private float _timeElapsedSinceFired;
        private float _timeElapsedSinceFireCeased;
        private float _timeSinceFired;
        private float _timeSinceFireCeased;

        private void Start()
        {
            this.effects.ForEach(x =>
            {
                x.laserBeam = this;
                x.Initialisation();
            });
            
            this.Range = this.weapon.range;
            this._timeSinceFired = Time.time;

            if (this.lineRenderer.positionCount != 2)
            {
                this.lineRenderer.positionCount = 2;
                this.SetPositions();
            }
            
            this.weapon.Firing += (sender, hit) =>
            {
                this._timeElapsedSinceFired = Time.time - this._timeSinceFired;
                this._timeSinceFireCeased = Time.time;

                var range = hit?.transform != null ? hit.Value.distance : this.weapon.range;

                this.Range = Mathf.Lerp(range, this.Range, this.speed * Time.deltaTime);
                
                this.SetPositions();
                this.effects.ForEach(x => x.OnFiring(this.DeltaTimeSinceFired(x.speed)));
            };
            
            this.weapon.FireCeased += (sender) =>
            {               
                this._timeElapsedSinceFireCeased = Time.time - this._timeSinceFireCeased;
                this._timeSinceFired = Time.time;
                this.SetPositions();
                this.effects.ForEach(x => x.OnFireCeased(this.DeltaTimeSinceFireCeased(x.speed)));
            };
        }

        private void SetPositions()
        {
            var origin = this.weapon.origin.position;
            var direction = this.weapon.direction.position - origin;
            this.SetPositions(origin, origin + direction.normalized * this.Range);
        }

        public void SetPositions(Vector3 startPosition, Vector3 endPosition)
        {
            this.lineRenderer.SetPositions(new[]
            {
                startPosition,
                endPosition
            });
        }

        private float DeltaTimeSinceFired(float range)
        {
            return Mathf.Clamp(this._timeElapsedSinceFired / range, 0, 1);
        }

        private float DeltaTimeSinceFireCeased(float range)
        {
            return Mathf.Clamp(this._timeElapsedSinceFireCeased / range, 0, 1);
        }
    }
}