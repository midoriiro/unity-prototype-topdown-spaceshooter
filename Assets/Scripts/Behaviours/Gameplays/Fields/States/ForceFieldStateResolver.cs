using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Behaviours.Gameplays.Fields.States
{
    public class ForceFieldStateResolver : MonoBehaviour
    {
        public List<ForceFieldState> states;
        
        private int _current;
        private bool _isRunning;

        private void RollIndex()
        {
            var index = this._current + 1;
            
            Debug.Log(index);

            if (index >= this.states.Count)
            {
                this.OnReset();
                return;
            }

            this._current = index;
        }

        public void OnStart()
        {
            this._isRunning = true;
        }

        public void OnStop()
        {
            this._isRunning = false;
        }

        public void OnReset()
        {
            this._current = 0;
        }

        public void Resolve()
        {
            if (!this._isRunning)
            {
                return;
            }
            
            var state = this.states[this._current];
            
            state.OnStart();
            state.OnUpdate();

            if (!state.IsReadyToNextState())
            {
                return;
            }

            state.OnStop();
            state.OnReset();
            this.RollIndex();
        }
    }
}