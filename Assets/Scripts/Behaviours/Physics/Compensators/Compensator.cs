using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Physics.Compensators
{
    public abstract class Compensator : MonoBehaviour, ICompensable
    {
        public abstract void Compensate(float amount);
    }
}