namespace Behaviours.Gameplays.Weapons.LaserBeam.Effects
{
    public class LaserBeamLengthEffect : LaserBeamEffect
    {
        public override void Initialisation()
        {
            return;
        }

        public override void OnFiring(float deltaTime)
        {       
            // TODO
//            var origin = this.LaserBeam.weapon.origin.localPosition;
//            var direction = this.LaserBeam.weapon.direction.localPosition - origin;
//            var endPosition = Vector3.Lerp(
//                this.LaserBeam.lineRenderer.GetPosition(1),
//                origin + direction.normalized * this.LaserBeam.Range,
//                deltaTime
//            );
//            this.LaserBeam.SetPositions(origin, endPosition);
        }

        public override void OnFireCeased(float deltaTime)
        {    
            // TODO
//            var origin = this.LaserBeam.weapon.origin.localPosition;
//            var direction = this.LaserBeam.weapon.direction.localPosition - origin;
//            var endPosition = Vector3.Lerp(
//                this.LaserBeam.lineRenderer.GetPosition(1),
//                origin,
//                deltaTime
//            );
//            this.LaserBeam.SetPositions(origin, endPosition);
        }
    }
}