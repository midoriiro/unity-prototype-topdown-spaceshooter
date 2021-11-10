using System.Collections.Generic;
using System.Linq;
using Systems.Helpers;

namespace Behaviours.Gameplays.Weapons.Commons.Extensions
{
    public static class CriticalExtension
    {
        #region List
        public static Critical Single(this List<Critical> criticals, float chance, Critical defaultCritical)
        {
            var critical = criticals
                .Where(x => MathHelper.OneMinus(x.chance) <= chance)
                .OrderByDescending(x => x.chance)
                .FirstOrDefault();
            return critical ?? defaultCritical;
        }
        
        public static Critical Random(this List<Critical> criticals, Critical defaultCritical)
        {
            return criticals.Single(UnityEngine.Random.value, defaultCritical);
        }
        #endregion
    }
}