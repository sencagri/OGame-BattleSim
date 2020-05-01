using OGBattleSim;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OGBattleSimTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Task> givenTasks = new List<Task>();

            for (int i = 0; i < 50; i++)
            {
                givenTasks.Add(StartSimAsync(i));
            }
            
            Task.WaitAll(givenTasks.ToArray());

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms");

            Console.ReadLine();
        }

        public static async Task<BattleResult> StartSimAsync(int i)
        {
            var simResult = Task.Run(() =>
            {
                Settings settings = new Settings();
                settings.DebrisFactor = 0.5;
                settings.SimulationRound = 10;

                EntityInfo entityInfo = new EntityInfo();
                //entityInfo.EntityQuanties.Add(EntityType.LightFighter, 400000);
                //entityInfo.EntityQuanties.Add(EntityType.LightFighter, 50000);
                entityInfo.EntityQuanties.Add(EntityType.Reaper, 50000);

                Tech tech = new Tech();
                tech.Weapon = 20;
                tech.Shield = 20;
                tech.Armor = 20;


                EntityInfo entityInfo1 = new EntityInfo();
                entityInfo1.EntityQuanties.Add(EntityType.Cruiser, 100000);
                
                //entityInfo1.EntityQuanties.TryAdd(EntityType.Battlecruiser, 100000);
                //entityInfo1.EntityQuanties.Add(EntityType.Battleship, 200000);

                var group1 = GroupGenerator.CreateGroup("Attacker" + i, entityInfo, tech);
                group1.Attacker = true;

                var group2 = GroupGenerator.CreateGroup("Defender" + i, entityInfo1, tech);
                group2.Attacker = false;

                List<Group> groups = new List<Group>();
                groups.Add(group1);
                groups.Add(group2);

                //Console.WriteLine("Starting sim");

                var battleResult = new Simulator().RunBattle(groups, settings);
                var def = battleResult.DefenderLoss;
                var att = battleResult.AttackerLoss;


                Console.WriteLine("Attacker {0}: {1}" ,i,  att.ToString("n0"));
                Console.WriteLine("Defender {0}: {1}" ,i,  def.ToString("n0"));
                //.WriteLine("**************");

                return battleResult;
            });

            return await simResult;
        }
    }
}
