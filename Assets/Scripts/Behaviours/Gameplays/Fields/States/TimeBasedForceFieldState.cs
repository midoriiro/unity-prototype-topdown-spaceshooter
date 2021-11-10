using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public abstract class TimeBasedForceFieldState : ForceFieldState
    {
        public float duration;

        private float _timeSinceStart;

        private float GetDeltaTime()
        {
            var now = Time.time;
            return now - this._timeSinceStart;
        }

        protected float GetTime()
        {
            return this.GetDeltaTime() / this.duration;
        }
        
        public override void OnStart()
        {
            if (!this.IsRunning)
            {
                this._timeSinceStart = Time.time;
            }
            
            base.OnStart();
        }

        public override bool IsReadyToNextState()
        {
            return this.GetDeltaTime().CompareTo(this.duration) == 1;
        }
    }
}