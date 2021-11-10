using System;
using Behaviours.Gameplays.Commons.Extensions;
using Behaviours.Gameplays.Fields.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public class ForceFieldAbsorbState : ForceFieldState
    {
        private Collider _collider;

        private void Start()
        {
            this._collider = this.generator.GetComponent<Collider>();
        }

        public override void OnStop()
        {
            this._collider.enabled = false;
        }
        
        public override void OnUpdate()
        {
            var collision = this.generator.impact.transform.position;
            var time = this.generator.health.InverseOfRatio();
            this.generator.Absorb(collision, this.alpha, time);
        }

        public override bool IsReadyToNextState()
        {
            return Math.Abs(this.generator.health.Quantity) <= this.generator.capacity;
        }
    }
}