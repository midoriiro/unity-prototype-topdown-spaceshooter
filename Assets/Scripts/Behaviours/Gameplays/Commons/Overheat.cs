using System;
using Core.Attributes;
using Core.Interfaces;
using UnityEngine;

namespace Behaviours.Gameplays.Commons
{
    public class Overheat : MonoBehaviour, ICoolable, IOverheatable
    {
        public float rate;
        public float safetyThreshold;
        public float criticalThreshold;
        
        public Action Cooldown { get; set; }
        public bool IsInOverheat { get; private set; }
        
        [ReadOnly, SerializeField] private float level;
        
        public delegate void OverheatEventHandler(Overheat sender, float currentLevel);

        public event OverheatEventHandler Overheated;
        public event OverheatEventHandler Overheating;
        public event OverheatEventHandler Cooled;
        public event OverheatEventHandler Cooling;

        private void Update()
        {
            this.Cooldown();

            if (this.IsInOverheat && this.level <= this.safetyThreshold)
            {
                this.level = this.safetyThreshold;
                this.IsInOverheat = false;
                this.Cooled?.Invoke(this, this.level);
            }
            
            if (!this.IsInOverheat && this.level >= this.criticalThreshold)
            {
                this.level = this.criticalThreshold;
                this.IsInOverheat = true;
                this.Overheated?.Invoke(this, this.level);
            }
        }

        public void AddOverheating()
        {
            if (this.level >= this.criticalThreshold)
            {
                this.level = this.criticalThreshold;
                return;
            }
            
            this.level += this.rate;
            this.Overheating?.Invoke(this, this.level);
        }

        public void AddCooling(float amount)
        {
            if (this.level <= 0)
            {
                this.level = 0f;
                return;
            }
            
            this.level -= amount;
            this.Cooling?.Invoke(this, this.level);
        }
    }
}