using System.Collections.Generic;
using System.Linq;

namespace OGBattleSim
{
    public class BattleResult
    {
        public BattleResult()
        {
            AttackerResults = new List<EntityResult>();
            DefenderResults = new List<EntityResult>();
        }

        public List<EntityResult> AttackerResults { get; set; }
        public List<EntityResult> DefenderResults { get; set; }

        public long AttackerLoss
        {
            get
            {
                return AttackerResults.Sum(r => r.TotalLoss);
            }
        }
        public long DefenderLoss
        {
            get
            {
                return DefenderResults.Sum(r => r.TotalLoss);
            }
        }
    }
}