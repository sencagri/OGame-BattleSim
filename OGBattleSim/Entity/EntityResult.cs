using System;
using System.Collections.Generic;
using System.Text;

namespace OGBattleSim
{
    public class EntityResult
    {
        public EntityType EntityType { get; set; }
        public long QuantityBefore { get; set; }
        public long QuantityAfter { get; set; }
        public long Difference
        {
            get
            {
                return (QuantityBefore - QuantityAfter);
            }
        }

        public long TotalLoss
        {
            get
            {
                return MetalLoss + CrystalLoss + DeuteriumLoss;
            }
        }

        public long MetalLoss
        {
            get
            {
                var p = Entity.GetPrice(EntityType).Item1;
                return Difference * p;
            }
        }

        public long CrystalLoss
        {
            get
            {
                var p = Entity.GetPrice(EntityType).Item2;
                return Difference * p;
            }
        }
        public long DeuteriumLoss
        {
            get
            {
                var p = Entity.GetPrice(EntityType).Item3;
                return Difference * p;
            }
        }
    }
}
