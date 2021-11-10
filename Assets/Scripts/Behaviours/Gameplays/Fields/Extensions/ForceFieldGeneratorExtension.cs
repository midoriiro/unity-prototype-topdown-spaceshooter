using System.Runtime.CompilerServices;
using Systems.Helpers;
using Behaviours.Gameplays.Commons.Extensions;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Gameplays.Fields.Extensions
{
    public static class ForceFieldGeneratorExtension
    {
        public static void AbsorbImpact(this ForceFieldGenerator generator, Collision collision)
        {
            var previousLocalPosition = generator.impact.transform.localPosition;
            var localPosition = collision.GetContact(0).ToLocalPoint(generator.transform);
            generator.impact.transform.localPosition = Vector3.Lerp(previousLocalPosition, localPosition, generator.velocity * Time.deltaTime);
        }
        
        public static void Absorb(this ForceFieldGenerator generator, Vector2 collision, float alpha, float time)
        {
            generator.controller.SetAlpha(x => Mathf.Lerp(x, alpha, generator.velocity * Time.deltaTime));
            generator.controller.SetSphereCenter(x => collision);
            generator.controller.SetSphereRadius(x => Mathf.Lerp(generator.radii.minimum, generator.radii.maximum, time));
            generator.controller.SetDissolve(x => 0f);
        }

        public static void ReadyToAbsorb(this ForceFieldGenerator generator, float alpha, float time)
        {
            generator.controller.SetAlpha(x => Mathf.Lerp(alpha, 0f, time));
            generator.controller.SetSphereCenter(x => generator.transform.position);
            generator.controller.SetSphereRadius(x => Mathf.Lerp(generator.radii.maximum, generator.radii.minimum, time));
            generator.controller.SetDissolve(x => 0f);
        }

        public static void Dissolve(this ForceFieldGenerator generator, float alpha, float time)
        {
            generator.controller.SetAlpha(x => alpha);
            generator.controller.SetSphereCenter(x => generator.transform.position);
            generator.controller.SetSphereRadius(x => generator.radii.maximum);
            generator.controller.SetDissolve(x => Mathf.Lerp(0f, 1f, time));
        }
        
        public static void Resolve(this ForceFieldGenerator generator, float alpha, float time)
        {
            generator.controller.SetAlpha(x => alpha);
            generator.controller.SetSphereCenter(x => generator.transform.position);
            generator.controller.SetSphereRadius(x => generator.radii.maximum);
            generator.controller.SetDissolve(x => Mathf.Lerp(1f, 0f, time));
        }
    }
}