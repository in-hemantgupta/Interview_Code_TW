using Battleship.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Player
    {
        public string Name { get; set; }
        public IEnumerable<IShip> Ships { get; set; }
        public string[] Targets{ get; set; }

        public Player(string name)
        {
            this.Name = name;
        }
    }
}
