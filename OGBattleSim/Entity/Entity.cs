using System;
using System.Collections.Generic;
using System.Text;

namespace OGBattleSim
{
    public class Entity
    {
        public Entity(EntityType entityType, Tech tech)
        {
            EntityType = entityType;

            switch (entityType)
            {
                case EntityType.LightFighter:
                    Price = 4000;
                    WeaponPower = 50 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 10 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.HeavyFighter:
                    Price = 10000;
                    WeaponPower = 150 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 25 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Cruiser:
                    Price = 27000;
                    WeaponPower = 400 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 50 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Battlecruiser:
                    Price = 70000;
                    WeaponPower = 700 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 400 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.SmallCargo:
                    Price = 4000;
                    WeaponPower = 5 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 10 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.LargeCargo:
                    Price = 12000;
                    WeaponPower = 5 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 25 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Battleship:
                    Price = 60000;
                    WeaponPower = 1000 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 200 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.ColonyShip:
                    Price = 30000;
                    WeaponPower = 50 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 100 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Recycler:
                    Price = 12000;
                    WeaponPower = 1 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 10 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.EspionageProbe:
                    Price = 1000;
                    WeaponPower = 0 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 0 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Bomber:
                    Price = 75000;
                    WeaponPower = 1000 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 500 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Destroyer:
                    Price = 110000;
                    WeaponPower = 2000 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 500 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Deathstar:
                    Price = 9000000;
                    WeaponPower = 200000 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 50000 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Reaper:
                    Price = 140000;
                    WeaponPower = 2800 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 700 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                case EntityType.Pathfinder:
                    Price = 23000;
                    WeaponPower = 200 * (1 + 0.1 * tech.Weapon);
                    ShieldPower = 100 * (1 + 0.1 * tech.Shield);
                    ShieldPowerInit = ShieldPower;
                    HullPoint = (Price / 10) * (1 + 0.1 * tech.Armor);
                    HullPointInit = HullPoint;
                    break;
                default:
                    break;
            }
        }

        public EntityType EntityType { get; set; }
        public double WeaponPower { get; set; }
        public double ShieldPower { get; set; }
        public double ShieldPowerInit { get; set; }
        public double HullPoint { get; set; }
        public double HullPointInit { get; set; }
        public int Price { get; set; }
        public bool ExplodeMe { get; set; }
    }
}
