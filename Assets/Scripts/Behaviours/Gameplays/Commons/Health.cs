using Systems.Helpers;
using Systems.Optimisations;
using Core.Attributes;
using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Gameplays.Commons
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        [ReadOnly, SerializeField] private float quantity;
        public float threshold;

        public float Quantity => quantity;

        public delegate void HealthEventHandler(float currentHealth);

        public event HealthEventHandler HealthDownToZero;
        public event HealthEventHandler HealthUpToMax;
        public event HealthEventHandler Damaged;
        public event HealthEventHandler Healed;

        private void Awake()
        {
            TransformCache.Add(this.transform, this);
        }

        private void Start()
        {
            this.quantity = this.threshold; 
        }

        public void AddDamaging(float quantity)
        {
            this.quantity -= quantity;

            if (this.quantity <= 0f)
            {
                this.quantity = 0f;
                this.HealthDownToZero?.Invoke(this.quantity);
            }
        
            this.Damaged?.Invoke(this.quantity);
        }

        public void AddHealing(float quantity)
        {
            this.quantity += quantity;

            if (this.quantity >= this.threshold)
            {
                this.quantity = this.threshold;
                this.HealthUpToMax?.Invoke(this.quantity);
            }
        
            this.Healed?.Invoke(this.quantity);
        }
    }
}
