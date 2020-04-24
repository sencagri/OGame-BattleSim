using System.Collections.Generic;

namespace OGBattleSim
{
    public class EntityInfo
    {
        public EntityInfo()
        {
            EntityQuanties = new Dictionary<EntityType, int>();
        }

        public Dictionary<EntityType, int> EntityQuanties { get; set; }

    }
}