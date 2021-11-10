using UnityEngine;

namespace Behaviours.Gameplays.Weapons.LaserBeam.Effects
{
    public abstract class LaserBeamEffect : MonoBehaviour
    {
        public float speed;
        public LaserBeam laserBeam;

        public abstract void Initialisation();
        public abstract void OnFiring(float deltaTIme);
        public abstract void OnFireCeased(float deltaTime);
    }
}