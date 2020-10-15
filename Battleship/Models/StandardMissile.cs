using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Models
{
    public class StandardMissile : IMissile
    {
        MissileType missileType;
        short[,] targetArea;
        public StandardMissile()
        {
            missileType = MissileType.Standard;
            targetArea = new short[,] { { 0, 0 } };
        }

        public Constraints.MissileType MissileType { get => missileType; }
        public short[,] TargetArea => TargetArea;
    }
}
