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
        public bool Attack(string Target, IMissile missile)
        {
            //-1,0,1 LTR
            //-1,0,1 UTD
            //LEFT -1,0, right 1,0, down = 0,-1, up = 0,1, CURRENT = 0,0 //SPECIAL MISSILE
            //RANGE = 9,9
            //CELL 0,0

            //foreach (var item in missile.TargetArea)
            //{
            //    this.Ship.Coordinates
            //}

            //foreach (int item in missile.TargetType)
            //{
            //    //
            //}
            //if (this.Ship.Coordinates.Any(s => s == coordinates))
            //{
            //    this.Ship.Life -= 1;
            //    return true;
            //}
            return false;
        }
    }
}
