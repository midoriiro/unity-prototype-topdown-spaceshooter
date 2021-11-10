using Behaviours.Gameplays.Fields.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public class ForceFieldReadyState : TimeBasedForceFieldState
    {
        private Collider _collider;

        private void Start()
        {
            this._collider = this.generator.GetComponent<Collider>();
        }

        public override void OnStop()
        {
            this._collider.enabled = true;
        }

        public override void OnUpdate()
        {
            this.generator.ReadyToAbsorb(this.alpha, this.GetTime());
        }
    }
}