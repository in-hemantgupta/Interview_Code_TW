using Battleship.Models;

namespace Battleship.Services
{
    public interface IShipFactory
    {
        IShip Create(Constraints.ShipType type);
    }
}