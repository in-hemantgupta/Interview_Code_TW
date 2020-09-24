using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public class ShipP : ShipBase
    {
        public ShipP()
        {
            this.Ship = new Ship(Constraints.ShipType.P)
            {
                Life = 1,
                Coordinates = new List<string>()
            };
        }
    }
}
