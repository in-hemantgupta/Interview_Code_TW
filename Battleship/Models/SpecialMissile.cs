using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Models
{
    public class SpecialMissile : IMissile
    {
        MissileType missileType;
        short[,] targetArea;
        public SpecialMissile()
        {  
            missileType = MissileType.Standard;
            targetArea = new short[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { 0, 0 } };
            //A3
        }

        public Constraints.MissileType MissileType { get => missileType; }
        public short[,] TargetArea => targetArea;
    }
}
