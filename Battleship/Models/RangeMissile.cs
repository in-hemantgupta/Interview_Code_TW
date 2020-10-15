using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Models
{
    public class RangeMissile : IMissile
    {
        MissileType missileType;
        short[,] targetArea;
        public RangeMissile()
        {
            missileType = MissileType.Range;
            targetArea = new short[,] { { 9, 9 } };
        }

        public Constraints.MissileType MissileType { get => missileType; }
        short[,] IMissile.TargetArea => throw new NotImplementedException();
    }
}
