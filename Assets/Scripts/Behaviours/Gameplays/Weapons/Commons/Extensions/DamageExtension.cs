using UnityEngine;

namespace Behaviours.Gameplays.Weapons.Commons.Extensions
{
    public static class DamageExtension
    {
        public static float Random(this Damage damage)
        {
            var minimum = Mathf.Min(damage.minimum, damage.maximum);
            var maximum = Mathf.Max(damage.minimum, damage.maximum);
            return UnityEngine.Random.Range(minimum, maximum);
        }
        
        public static float RandomWithCritical(this Damage damage, Critical defaultCritical)
        {
            var amount = Random(damage);
            var critical = damage.criticals.Random(defaultCritical);
            return amount * critical.multiplier;
        }
    }
}