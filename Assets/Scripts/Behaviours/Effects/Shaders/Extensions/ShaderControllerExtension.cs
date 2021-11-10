using System;
using UnityEngine;

namespace Behaviours.Effects.Shaders.Extensions
{
    public static class ShaderControllerExtension
    {
        public static float GetValue(this ShaderController controller, string name)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            return controller.propertyBlock.GetFloat(name);
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<float, float> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetFloat(name);
            value = functor(value);
            controller.propertyBlock.SetFloat(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<int, int> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetInt(name);
            value = functor(value);
            controller.propertyBlock.SetInt(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<Color, Color> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetColor(name);
            value = functor(value);
            controller.propertyBlock.SetColor(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<Matrix4x4, Matrix4x4> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetMatrix(name);
            value = functor(value);
            controller.propertyBlock.SetMatrix(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<Vector4, Vector4> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetVector(name);
            value = functor(value);
            controller.propertyBlock.SetVector(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<Texture, Texture> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetTexture(name);
            value = functor(value);
            controller.propertyBlock.SetTexture(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<float[], float[]> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetFloatArray(name);
            value = functor(value);
            controller.propertyBlock.SetFloatArray(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<Matrix4x4[], Matrix4x4[]> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetMatrixArray(name);
            value = functor(value);
            controller.propertyBlock.SetMatrixArray(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
        
        public static ShaderController SetValue(this ShaderController controller, string name, Func<Vector4[], Vector4[]> functor)
        {
            controller.renderer.GetPropertyBlock(controller.propertyBlock);
            var value = controller.propertyBlock.GetVectorArray(name);
            value = functor(value);
            controller.propertyBlock.SetVectorArray(name, value);
            controller.renderer.SetPropertyBlock(controller.propertyBlock);
            return controller;
        }
    }
}