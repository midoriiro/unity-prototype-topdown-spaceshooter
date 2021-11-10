using Behaviours.Gameplays.Fields.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public class ForceFieldShutdownState : TimeBasedForceFieldState
    {
        public override void OnStop() {}

        public override void OnUpdate()
        {
            this.generator.Dissolve(this.alpha, this.GetTime());
        }
    }
}