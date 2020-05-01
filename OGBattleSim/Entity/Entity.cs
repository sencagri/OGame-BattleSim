using System;
using System.Collections.Generic;
using System.Text;

namespace OGBattleSim
{
    [Serializable]
    public class Entity
    {

        public Entity(EntityType entityType, Tech tech)
        {
            EntityType = entityType;
            
            var values = GetBaseValues(entityType);
            WeaponPower = (int)((1 + 0.1 * tech.Weapon) * values.Item2);
            ShieldPower = (int)((1 + 0.1 * tech.Shield) * values.Item3);
            HullPoint = (int)(values.Item1 / 10 * (1 + 0.1 * tech.Armor));
            ShieldPowerInit = ShieldPower;
            HullPointInit = HullPoint;
            ExplodeMe = false;
        }

        public EntityType EntityType { get; set; }
        public int WeaponPower { get; set; }
        public int ShieldPower { get; set; }
        public int ShieldPowerInit { get; set; }
        public int HullPoint { get; set; }
        public int HullPointInit { get; set; }
        public bool ExplodeMe { get; set; }

        public static Tuple<int, int, int> GetPrice(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.SmallCargo:
                    return new Tuple<int, int, int>(2000, 2000, 0);
                case EntityType.LargeCargo:
                    return new Tuple<int, int, int>(6000, 6000, 0);
                case EntityType.LightFighter:
                    return new Tuple<int, int, int>(3000, 1000, 0);
                case EntityType.HeavyFighter:
                    return new Tuple<int, int, int>(6000, 4000, 0);
                case EntityType.Cruiser:
                    return new Tuple<int, int, int>(20000, 7000, 2000);
                case EntityType.Battleship:
                    return new Tuple<int, int, int>(45000, 15000, 0);
                case EntityType.ColonyShip:
                    return new Tuple<int, int, int>(10000, 20000, 10000);
                case EntityType.Recycler:
                    return new Tuple<int, int, int>(10000, 6000, 2000);
                case EntityType.EspionageProbe:
                    return new Tuple<int, int, int>(0, 1000, 0);
                case EntityType.Bomber:
                    return new Tuple<int, int, int>(50000, 25000, 15000);
                case EntityType.Destroyer:
                    return new Tuple<int, int, int>(60000, 50000, 15000);
                case EntityType.Deathstar:
                    return new Tuple<int, int, int>(5000000, 4000000, 1000000);
                case EntityType.Battlecruiser:
                    return new Tuple<int, int, int>(30000, 40000, 15000);
                case EntityType.Reaper:
                    return new Tuple<int, int, int>(85000, 55000, 20000);
                case EntityType.Pathfinder:
                    return new Tuple<int, int, int>(8000, 15000, 8000);
                default:
                    return null;
            }
        }

        public static Tuple<int, int, int> GetBaseValues(EntityType entityType)
        {
            int price = 0;
            int weaponPower = 0;
            int shieldPower = 0;

            switch (entityType)
            {
                case EntityType.LightFighter:
                    price = 4000;
                    weaponPower = 50;
                    shieldPower = 10;
                    break;
                case EntityType.HeavyFighter:
                    price = 10000;
                    weaponPower = 150;
                    shieldPower = 25;
                    break;
                case EntityType.Cruiser:
                    price = 27000;
                    weaponPower = 400;
                    shieldPower = 50;
                    break;
                case EntityType.Battlecruiser:
                    price = 70000;
                    weaponPower = 700;
                    shieldPower = 400;
                    break;
                case EntityType.SmallCargo:
                    price = 4000;
                    weaponPower = 5;
                    shieldPower = 10;
                    break;
                case EntityType.LargeCargo:
                    price = 12000;
                    weaponPower = 5;
                    shieldPower = 25;
                    break;
                case EntityType.Battleship:
                    price = 60000;
                    weaponPower = 1000;
                    shieldPower = 200;
                    break;
                case EntityType.ColonyShip:
                    price = 30000;
                    weaponPower = 50;
                    shieldPower = 100;
                    break;
                case EntityType.Recycler:
                    price = 16000;
                    weaponPower = 1;
                    shieldPower = 10;
                    break;
                case EntityType.EspionageProbe:
                    price = 1000;
                    weaponPower = 0;
                    shieldPower = 0;
                    break;
                case EntityType.Bomber:
                    price = 75000;
                    weaponPower = 1000;
                    shieldPower = 500;
                    break;
                case EntityType.Destroyer:
                    price = 110000;
                    weaponPower = 2000;
                    shieldPower = 500;
                    break;
                case EntityType.Deathstar:
                    price = 9000000;
                    weaponPower = 200000;
                    shieldPower = 50000;
                    break;
                case EntityType.Reaper:
                    price = 140000;
                    weaponPower = 2800;
                    shieldPower = 700;
                    break;
                case EntityType.Pathfinder:
                    price = 23000;
                    weaponPower = 200;
                    shieldPower = 100;
                    break;
                default:
                    break;
            }

            return new Tuple<int, int, int>(price, weaponPower, shieldPower);
        }
    }
}
