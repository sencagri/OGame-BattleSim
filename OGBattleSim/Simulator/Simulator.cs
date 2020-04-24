using System;
using System.Collections.Generic;
using System.Linq;

namespace OGBattleSim
{
    public static class Simulator
    {
        public static Random R { get; set; } = new Random();


        public static BattleResult RunBattle(List<Group> attackerGroup, List<Group> defenderGroup, Settings settings)
        {

            // Rounds
            for (int i = 0; i < 6; i++)
            {
                // attack phase
                groupFire(attackerGroup, defenderGroup);
                groupFire(defenderGroup, attackerGroup);

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

            return new BattleResult();
        }

        private static void removeExplodedEntities(List<Group> groupList)
        {
            foreach (var group in groupList)
            {
                var explodedEntities = group.Entities.Where(r => r.ExplodeMe).ToList();
                for (int i = 0; i < explodedEntities.Count; i++)
                {
                    var explodedEntity = explodedEntities[i];
                    group.Entities.Remove(explodedEntity);
                }
            }
        }

        private static void resetShields(List<Group> groupList)
        {
            foreach (var group in groupList)
            {
                foreach (var entity in group.Entities)
                {
                    entity.ShieldPower = entity.ShieldPowerInit;
                }
            }
        }

        private static void groupFire(List<Group> attackerGroup, List<Group> defenderGroup)
        {

            var attacker = attackerGroup.FirstOrDefault().Entities;
            var defender = defenderGroup.FirstOrDefault().Entities;

            var attackerCount = attacker.Count;
            var defenderCount = defender.Count;

            for (int i = 0; i < attacker.Count; i++)
            {
                var attackingEntity = attacker[i];
                bool rapidFire = false;

                do
                {
                    var defendingEntity = defender[R.Next(defenderCount)];
                    entityAttacksEntity(attackingEntity, defendingEntity);
                    rapidFire = shouldItShootAgain(attackingEntity, defendingEntity);

                } while (rapidFire);
            }
        }

        private static bool shouldItShootAgain(Entity attackingEntity, Entity defendingEntity)
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
                var rapidRatio = (rapidFire - 1) / (rapidFire) * 100;
                var ratio = R.Next(100);
                if (ratio <= rapidRatio)
                {
                    return true;
                }
            }
            return false;
        }

        private static void entityAttacksEntity(Entity attackingEntity, Entity defendingEntity)
        {
            if (attackingEntity.WeaponPower < defendingEntity.ShieldPower * 0.01)
            {
                // bounced
                return;
            }

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

        private static void checkExploding(Entity defendingEntity)
        {
            var checkExplodeRatio = defendingEntity.HullPoint / defendingEntity.HullPointInit;
            if (checkExplodeRatio <= 0.7)
            {
                var explodeRatio = (1 - (defendingEntity.HullPoint / defendingEntity.HullPointInit))*100;
                var ratio = R.Next(100);
                
                if (ratio <= explodeRatio)
                {
                    defendingEntity.ExplodeMe = true;
                }
            }
        }
    }
}
