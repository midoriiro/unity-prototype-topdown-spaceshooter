using Systems.Helpers;
using Systems.Inputs.Extensions;
using Behaviours.Gameplays.Inputs;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public class EngineController : MonoBehaviour
    {
        public float speed;
        public float rate;
        public GamePadInputController controller;
        public Engine fightEngine;
        public Engine cruiseEngine;

        private Engine _currentEngine;
        private float _lastTimeNormalTransition;
        private float _lastTimeHoldTransition;
        private float _lastTimePressButton;
        private Vector3 _lastVelocityNormalTransition;
        private Vector3 _lastVelocityHoldTransition;
        private Vector3 _lastAngularVelocityNormalTransition;
        private Vector3 _lastAngularVelocityHoldTransition;
        
        private void Start()
        {
            this.cruiseEngine.gameObject.SetActive(false);
            this.fightEngine.gameObject.SetActive(true);
            this._currentEngine = this.fightEngine;
            this.cruiseEngine.velocityConstraints.clamped = false;
            this.fightEngine.velocityConstraints.clamped = false;
            this._lastTimeNormalTransition = Time.time;
            this._lastTimeHoldTransition = Time.time;
            this._lastTimePressButton = Time.time;
        }

        private void FixedUpdate()
        {
            if (this.controller.faceButtons.B.IsDown())
            {
                this._lastTimePressButton = Time.time;
            }
            
            if (this.controller.faceButtons.B.IsHold() && Time.time - this._lastTimePressButton >= this.rate)
            {               
                this.fightEngine.gameObject.SetActive(false);
                this.cruiseEngine.gameObject.SetActive(true);
                this._currentEngine = this.cruiseEngine;

                this._currentEngine.ClampVelocityBy(() => Mathf.Lerp(
                    this._lastVelocityHoldTransition.magnitude,
                    this.cruiseEngine.velocityConstraints.range.maximum,
                    this.GetTime(this._lastTimeHoldTransition)
                ));
                
                this._currentEngine.ClampAngularVelocityBy(() => Mathf.Lerp(
                    this._lastAngularVelocityHoldTransition.magnitude,
                    this.cruiseEngine.velocityConstraints.range.maximum,
                    this.GetTime(this._lastTimeHoldTransition)
                ));
                
                this._lastTimeNormalTransition = Time.time;
                this._lastVelocityNormalTransition = this._currentEngine.rigidbody.velocity;
                this._lastAngularVelocityNormalTransition = this._currentEngine.rigidbody.angularVelocity;
            }
            else
            {
                this.cruiseEngine.gameObject.SetActive(false);
                this.fightEngine.gameObject.SetActive(true);
                this._currentEngine = this.fightEngine;

                this._currentEngine.ClampVelocityBy(() => Mathf.Lerp(
                    this._lastVelocityNormalTransition.magnitude,
                    this.fightEngine.velocityConstraints.range.maximum,
                    this.GetTime(this._lastTimeNormalTransition)
                ));
                
                this._currentEngine.ClampAngularVelocityBy(() => Mathf.Lerp(
                    this._lastAngularVelocityNormalTransition.magnitude,
                    this.fightEngine.velocityConstraints.range.maximum,
                    this.GetTime(this._lastTimeNormalTransition)
                ));
                
                this._lastTimeHoldTransition = Time.time;
                this._lastVelocityHoldTransition = this._currentEngine.rigidbody.velocity;
                this._lastAngularVelocityHoldTransition = this._currentEngine.rigidbody.angularVelocity;
            }
        }

        private float GetTime(float lastTimeTransition)
        {
            return MathHelper.Map(
                Mathf.Clamp(
                    Time.time - lastTimeTransition,
                    0,
                    this.speed
                ),
                0,
                this.speed,
                0,
                1
            );
        }
    }
}