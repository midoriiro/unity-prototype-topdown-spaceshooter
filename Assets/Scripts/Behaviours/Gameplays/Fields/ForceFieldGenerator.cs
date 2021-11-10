using System;
using Systems.Helpers;
using Systems.Math;
using Behaviours.Effects.Shaders;
using Behaviours.Effects.Shaders.Extensions;
using Behaviours.Gameplays.Commons;
using Behaviours.Gameplays.Commons.Extensions;
using Behaviours.Gameplays.Fields.Extensions;
using Behaviours.Gameplays.Fields.States;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Fields
{
    public class ForceFieldGenerator : MonoBehaviour
    {
        public float capacity;
        public float velocity;
        public FloatRange radii;
        public Health health;
        public ForceFieldShaderController controller;
        public ForceFieldStateResolver resolver;
        public GameObject impact;

        private void Start()
        {
            this.controller.Collided += this.AbsorbImpact;
            this.resolver.OnStart();
        }

        private void Update()
        {
            this.health.AddHealing(this.capacity);
            this.resolver.Resolve();
        }
    }
}