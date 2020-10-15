using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Models
{
    public interface IMissile
    {
        MissileType MissileType { get; }
        short[,] TargetArea { get; }
    }
}
