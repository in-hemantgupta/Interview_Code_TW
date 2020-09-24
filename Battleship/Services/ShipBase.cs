using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public abstract class ShipBase : IShip
    {
        public Ship Ship { get; set; }
        /// <summary>
        /// Attack the ship with the target, if target matches the ship coordinates then set the life to -1
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool Attack(string coordinates)
        {
            if (this.Ship.Coordinates.Any(s => s == coordinates))
            {
                this.Ship.Life -= 1;
                return true;
            }
            return false;
        }
    }
}
