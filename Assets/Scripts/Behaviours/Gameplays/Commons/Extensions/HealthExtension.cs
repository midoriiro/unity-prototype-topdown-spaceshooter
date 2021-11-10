using System.Runtime.CompilerServices;
using Systems.Helpers;

namespace Behaviours.Gameplays.Commons.Extensions
{
    public static class HealthExtension
    {
        public static float Ratio(this Health health)
        {
            return MathHelper.Ratio(health.threshold, health.Quantity);
        }

        public static float InverseOfRatio(this Health health)
        {
            return MathHelper.InverseOfRatio(health.threshold, health.Quantity);
        }
    }
}