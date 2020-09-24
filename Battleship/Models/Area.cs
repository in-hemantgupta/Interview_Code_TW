using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Area
    {
        public int Width { get; set; }
        public char Height { get; set; }

        public Area(int width, char height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
