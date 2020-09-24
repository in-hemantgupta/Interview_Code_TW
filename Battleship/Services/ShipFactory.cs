using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Services
{
    public class ShipFactory : IShipFactory
    {
        readonly Func<ShipType, IShip> shipFactory;

        public ShipFactory(Func<ShipType, IShip> shipFactory)
        {
            this.shipFactory = shipFactory;
        }
        public IShip Create(ShipType type)
        {
            return shipFactory(type);
        }
    }
}
