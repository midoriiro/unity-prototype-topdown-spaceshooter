using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public abstract class ForceFieldState : MonoBehaviour
    {
        public ForceFieldGenerator generator;
        public float alpha;

        protected bool IsRunning { get; private set; }

        public virtual void OnStart()
        {
            if (this.IsRunning)
            {
                return;
            }

            this.IsRunning = true;
        }
        
        public abstract void OnStop();

        public void OnReset()
        {
            this.IsRunning = false;
        }

        public abstract void OnUpdate();

        public abstract bool IsReadyToNextState();
    }
}