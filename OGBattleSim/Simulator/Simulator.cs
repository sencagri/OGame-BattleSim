using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OGBattleSim
{
    public class Simulator
    {

        public Random R { get; set; } = new Random();


        public BattleResult RunBattle(List<Group> groups, Settings settings)
        {
            BattleResult results = new BattleResult();

            List<Group> attackerGroup = groups.Where(r => r.Attacker).ToList();
            List<Group> defenderGroup = groups.Where(r => !r.Attacker).ToList();

            addBattleEntity(results, groups);

            StartRounds(attackerGroup, defenderGroup);

            addBattleEntity(results, groups, false);

            return results;
        }

        private void StartRounds(List<Group> attackerGroup, List<Group> defenderGroup)
        {
            // Rounds
            for (int i = 0; i < 6; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                // attack phase

                var t1 = Task.Run(() =>
                 {
                     groupFire(attackerGroup, defenderGroup);

                 });
                var t2 = Task.Run(() =>
                {
                    groupFire(defenderGroup, attackerGroup);
                });

                Task.WaitAll(t1, t2);
                sw.Stop();
                var t = sw.ElapsedMilliseconds;
                // round end, reset shields and remove exploded entities
                resetShields(attackerGroup);
                resetShields(defenderGroup);

                // remove exploded entities
                removeExplodedEntities(attackerGroup);
                removeExplodedEntities(defenderGroup);

                // check result
                if (attackerGroup.FirstOrDefault().Entities.Count == 0 || defenderGroup.FirstOrDefault().Entities.Count == 0)
                {
                    break;
                }
            }
        }

        public BattleResult addBattleEntity(BattleResult battleResult, List<Group> groups, bool before = true)
        {
            var attackerBeforeGroup = groups.FirstOrDefault(r => r.Attacker);
            var defenderBeforeGroup = groups.FirstOrDefault(r => !r.Attacker);

            //EntityType[] entityTypes = new EntityType();
            /*
            EntityType[] vals = (EntityType[])Enum.GetValues(typeof(EntityType)).Clone();
            
            Parallel.ForEach(vals, (entityType) => {
                var quantityAttacker = attackerBeforeGroup.Entities.Count(r => r.EntityType == entityType);
                var quantityDefender = defenderBeforeGroup.Entities.Count(r => r.EntityType == entityType);

                if (before)
                {
                    battleResult.AttackerResults.Add(new EntityResult() { EntityType = entityType, QuantityBefore = quantityAttacker });
                    battleResult.DefenderResults.Add(new EntityResult() { EntityType = entityType, QuantityBefore = quantityDefender });
                }
                else
                {
                    battleResult.AttackerResults.FirstOrDefault(r => r.EntityType == entityType).QuantityAfter = quantityAttacker;
                    battleResult.DefenderResults.FirstOrDefault(r => r.EntityType == entityType).QuantityAfter = quantityDefender;
                }
            });
            */

            foreach (EntityType entityType in Enum.GetValues(typeof(EntityType)))
            {
                var quantityAttacker = attackerBeforeGroup.Entities.Count(r => r.EntityType == entityType);
                var quantityDefender = defenderBeforeGroup.Entities.Count(r => r.EntityType == entityType);

                if (quantityAttacker > 0)
                {
                    if (before)
                    {
                        battleResult.AttackerResults.Add(new EntityResult() { EntityType = entityType, QuantityBefore = quantityAttacker });
                    }
                    else
                    {
                        battleResult.AttackerResults.FirstOrDefault(r => r.EntityType == entityType).QuantityAfter = quantityDefender;
                    }
                }

                if (quantityDefender > 0)
                {
                    if (before)
                    {
                        battleResult.DefenderResults.Add(new EntityResult() { EntityType = entityType, QuantityBefore = quantityAttacker });
                    }
                    else
                    {
                        battleResult.DefenderResults.FirstOrDefault(r => r.EntityType == entityType).QuantityAfter = quantityDefender;
                    }
                }
            }

            return battleResult;
        }

        private void removeExplodedEntities(List<Group> groupList)
        {
            foreach (var group in groupList)
            {
                group.Entities = group.Entities.Where(r => !r.ExplodeMe).ToList();
            }
        }

        private void resetShields(List<Group> groupList)
        {
            foreach (var group in groupList)
            {
                for (int i = 0; i < group.Entities.Count; i++)
                {
                    var entity = group.Entities[i];
                    entity.ShieldPower = entity.ShieldPowerInit;
                }
            }
        }

        private void groupFire(List<Group> attackerGroup, List<Group> defenderGroup)
        {

            var attacker = attackerGroup.FirstOrDefault().Entities.ToArray();
            var defender = defenderGroup.FirstOrDefault().Entities.ToArray();

            var attackerCount = attacker.Count();
            var defenderCount = defender.Count();

            for (int i = 0; i < attackerCount; i++)
            {
                var attackingEntity = attacker[i];
                bool rapidFire;

                do
                {
                    var rand = R.Next(defenderCount);
                    var defendingEntity = defender[rand];
                    entityAttacksEntity(attackingEntity, defendingEntity);
                    rapidFire = shouldItShootAgain(attackingEntity, defendingEntity);

                } while (rapidFire);
            }
            /*

            Parallel.For(0, attackerCount, i =>
            {
                var attackingEntity = attacker[i];
                bool rapidFire = false;

                do
                {
                    var defendingEntity = defender[R.Next(defenderCount)];
                    entityAttacksEntity(attackingEntity, defendingEntity);
                    rapidFire = shouldItShootAgain(attackingEntity, defendingEntity);

                } while (rapidFire);
            });
            */

        }

        private bool shouldItShootAgain(Entity attackingEntity, Entity defendingEntity)
        {
            float rapidFire = 0;

            switch (attackingEntity.EntityType)
            {
                case EntityType.LightFighter:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;

                case EntityType.HeavyFighter:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.SmallCargo:
                            rapidFire = 3;
                            break;
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;
                case EntityType.Cruiser:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.LightFighter:
                            rapidFire = 6;
                            break;
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;
                case EntityType.Bomber:
                case EntityType.Recycler:
                case EntityType.ColonyShip:
                case EntityType.SmallCargo:
                case EntityType.LargeCargo:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;
                case EntityType.Battleship:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                        case EntityType.Pathfinder:
                            rapidFire = 5;
                            break;
                    }
                    break;
                case EntityType.Destroyer:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                        case EntityType.Battlecruiser:
                            rapidFire = 2;
                            break;
                    }
                    break;
                case EntityType.Deathstar:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.SmallCargo:
                            rapidFire = 250;
                            break;
                        case EntityType.LargeCargo:
                            rapidFire = 250;
                            break;
                        case EntityType.LightFighter:
                            rapidFire = 200;
                            break;
                        case EntityType.HeavyFighter:
                            rapidFire = 100;
                            break;
                        case EntityType.Cruiser:
                            rapidFire = 33;
                            break;
                        case EntityType.Battleship:
                            rapidFire = 30;
                            break;
                        case EntityType.ColonyShip:
                            rapidFire = 250;
                            break;
                        case EntityType.Recycler:
                            rapidFire = 250;
                            break;
                        case EntityType.EspionageProbe:
                            rapidFire = 1250;
                            break;
                        case EntityType.Bomber:
                            rapidFire = 25;
                            break;
                        case EntityType.Destroyer:
                            rapidFire = 5;
                            break;
                        case EntityType.Deathstar:
                            break;
                        case EntityType.Battlecruiser:
                            rapidFire = 15;
                            break;
                        case EntityType.Reaper:
                            rapidFire = 10;
                            break;
                        case EntityType.Pathfinder:
                            rapidFire = 30;
                            break;
                    }
                    break;
                case EntityType.Battlecruiser:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.SmallCargo:
                            rapidFire = 3;
                            break;
                        case EntityType.LargeCargo:
                            rapidFire = 3;
                            break;
                        case EntityType.HeavyFighter:
                            rapidFire = 4;
                            break;
                        case EntityType.Cruiser:
                            rapidFire = 4;
                            break;
                        case EntityType.Battleship:
                            rapidFire = 7;
                            break;
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;
                case EntityType.Reaper:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.Destroyer:
                            rapidFire = 3;
                            break;
                        case EntityType.Bomber:
                            rapidFire = 4;
                            break;
                        case EntityType.Battleship:
                            rapidFire = 7;
                            break;
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;
                case EntityType.Pathfinder:
                    switch (defendingEntity.EntityType)
                    {
                        case EntityType.HeavyFighter:
                            rapidFire = 2;
                            break;
                        case EntityType.LightFighter:
                            rapidFire = 3;
                            break;
                        case EntityType.Cruiser:
                            rapidFire = 3;
                            break;
                        case EntityType.EspionageProbe:
                            rapidFire = 5;
                            break;
                    }
                    break;
            }

            if (rapidFire > 0)
            {
                var rapidRatio = (rapidFire - 1) * 1.0 / (rapidFire) * 100;
                var ratio = R.Next(100) * 1.0;
                if (ratio <= rapidRatio)
                {
                    return true;
                }
            }
            return false;
        }

        private void entityAttacksEntity(Entity attackingEntity, Entity defendingEntity)
        {
            if (attackingEntity.WeaponPower < defendingEntity.ShieldPower * 0.01)
            {
                // bounced
                return;
            }

            /*
            if (defendingEntity.HullPoint == 0)
            {
                // do not calculate again if already exploded
                defendingEntity.ExplodeMe = true;
            }*/

            var defenderShieldPoint = defendingEntity.ShieldPower;
            defendingEntity.ShieldPower -= attackingEntity.WeaponPower;
            if (defendingEntity.ShieldPower <= 0)
            {
                // shield is not enough to defend, hull points get damage also
                defendingEntity.ShieldPower = 0;
                defendingEntity.HullPoint -= (attackingEntity.WeaponPower - defenderShieldPoint);

                if (defendingEntity.HullPoint <= 0)
                {
                    defendingEntity.HullPoint = 0;
                    defendingEntity.ExplodeMe = true;
                }
                else
                {
                    checkExploding(defendingEntity);
                }
            }
        }

        private void checkExploding(Entity defendingEntity)
        {

            var checkExplodeRatio = defendingEntity.HullPoint * 1.0 / defendingEntity.HullPointInit;
            if (checkExplodeRatio <= 0.7)
            {
                var explodeRatio = (1 - (defendingEntity.HullPoint * 1.0 / defendingEntity.HullPointInit)) * 100;
                var ratio = R.Next(100);

                if (ratio <= explodeRatio)
                {
                    defendingEntity.ExplodeMe = true;
                }
            }
        }
    }
}
