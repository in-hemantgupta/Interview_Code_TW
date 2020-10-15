using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public class ShipQ : ShipBase
    {
        public ShipQ()
        {
            this.Ship = new Ship(Constraints.ShipType.Q)
            {
                Life = 2,
                Coordinates = new List<Coordinates>()
            };
        }
    }
}