using System;
using System.Collections.Generic;
using System.Text;

namespace OGBattleSim
{
    [Serializable]
    public class Tech
    {
        public int Weapon { get; set; }
        public int Shield { get; set; }
        public int Armor { get; set; }
    }
}
