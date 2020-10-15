using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Coordinates
    {
        public char X { get; set; }
        public short Y { get; set; }
        public Coordinates(char x, short y)
        {
            X = x;
            Y = y;
        }
    }
}
