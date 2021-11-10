using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Gameplays.Commons
{
    public class Cooldown : MonoBehaviour
    {
        public float rate;

        public void Cooling(ICoolable coolable)
        {
            coolable.AddCooling(this.rate);
        }
    }
}