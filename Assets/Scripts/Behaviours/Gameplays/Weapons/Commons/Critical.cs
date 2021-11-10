using System;

namespace Behaviours.Gameplays.Weapons.Commons
{
    [Serializable]
    public class Critical
    {
        public float multiplier;
        public float chance;
        
        public static readonly Critical Default = new Critical
        {
            multiplier = 1,
            chance = 0
        };
    }
    
}