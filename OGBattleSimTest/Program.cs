using OGBattleSim;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGBattleSimTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings();
            settings.DebrisFactor = 0.5;
            settings.SimulationRound = 10;

            EntityInfo entityInfo = new EntityInfo();
            //entityInfo.EntityQuanties.Add(EntityType.LightFighter, 400000);
            entityInfo.EntityQuanties.Add(EntityType.LightFighter, 500);
            entityInfo.EntityQuanties.Add(EntityType.Cruiser, 150);
            
            Tech tech = new Tech();
            tech.Weapon = 20;
            tech.Shield = 20;
            tech.Armor = 20;


            EntityInfo entityInfo1 = new EntityInfo();
            //entityInfo1.EntityQuanties.Add(EntityType.LightFighter, 3000000);
            entityInfo1.EntityQuanties.Add(EntityType.LightFighter, 2000);

            var group1 = GroupGenerator.CreateGroup("Attacker1", entityInfo, tech);
            var group2 = GroupGenerator.CreateGroup("Defender1", entityInfo1, tech);

            List<Group> attackers = new List<Group>();
            List<Group> defenders = new List<Group>();

            attackers.Add(group1);
            defenders.Add(group2);


            var battleResult = Simulator.RunBattle(attackers, defenders, settings);

            Console.ReadLine();
        }
    }
}
