using System;
using Behaviours.Gameplays.Commons.Extensions;
using Behaviours.Gameplays.Fields.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public class ForceFieldRechargingState : ForceFieldState
    {
        public override void OnStop() {}
        
        public override void OnUpdate()
        {
            this.generator.Resolve(this.alpha, this.generator.health.Ratio());
        }

        public override bool IsReadyToNextState()
        {
            return this.generator.health.Quantity >= this.generator.health.threshold;
        }
    }
}