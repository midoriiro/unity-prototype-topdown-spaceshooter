using System;
using System.Collections.Generic;

namespace Behaviours.Gameplays.Weapons.Commons
{
    [Serializable]
    public class Damage
    {
        public float minimum;
        public float maximum;
        public List<Critical> criticals;
    }
}