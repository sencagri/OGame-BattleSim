using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OGBattleSim
{
    public static class GroupGenerator
    {
        public static Group CreateGroup(string name, EntityInfo entityInfo, Tech tech)
        {
            Group result = null;


            if (entityInfo != null)
            {
                result = new Group();

                result.Name = name;
                result.Tech.Weapon = tech.Weapon;
                result.Tech.Shield = tech.Shield;
                result.Tech.Armor = tech.Armor;

                foreach (var entityinfo in entityInfo.EntityQuanties)
                {
                    for (int i = 0; i < entityinfo.Value; i++)
                    {
                        result.Entities.Add(new Entity(entityinfo.Key, tech));
                    }
                }

            }

            return result;
        }
    }
}
