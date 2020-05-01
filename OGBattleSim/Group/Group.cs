using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OGBattleSim
{
    [Serializable]
    public class Group
    {
        public Group()
        {
            Entities = new List<Entity>();
            Tech = new Tech();
        }

        public string Name { get; set; }
        public bool Attacker { get; set; }
        public Tech Tech { get; set; }
        public List<Entity> Entities { get; set; }
    }
}