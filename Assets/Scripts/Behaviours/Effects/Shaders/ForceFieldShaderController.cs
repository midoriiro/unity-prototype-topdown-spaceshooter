using System;
using Behaviours.Effects.Shaders.Extensions;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Effects.Shaders
{
    public class ForceFieldShaderController : ShaderController
    {
        public delegate void CollisionEventHandler(Collision collision);

        public event CollisionEventHandler Collided;

        private void Start()
        {
            this.propertyBlock = new MaterialPropertyBlock();
            this.SetValue("_alpha", x => 0f);
            this.SetValue("_sphereCenter", x => Vector4.zero);
        }

        private void OnCollisionEnter(Collision other)
        {
            this.Collided?.Invoke(other);
        }

        private void OnCollisionStay(Collision other)
        {
            this.Collided?.Invoke(other);
        }

        public void SetAlpha(Func<float, float> alpha)
        {
            this.SetValue("_alpha", alpha);
        }

        public void SetSphereCenter(Func<Vector4, Vector4> center)
        {
            this.SetValue("_sphereCenter", center);
        }

        public void SetSphereRadius(Func<float, float> radius)
        {
            this.SetValue("_sphereRadius", radius);
        }
        
        public void SetDissolve(Func<float, float> dissolve)
        {
            this.SetValue("_dissolve", dissolve);
        }
    }
}