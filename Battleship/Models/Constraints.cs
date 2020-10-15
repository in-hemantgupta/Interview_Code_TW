using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Constraints
    {
        public enum ShipType { Q, P }
        public enum MissileType { Standard, Special, Range }

        public enum MissileTargetType { Cell, Box, Range }
    }
}
