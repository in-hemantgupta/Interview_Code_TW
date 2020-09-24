using Battleship.Models;

namespace Battleship.Services
{
    public interface IShip
    {
        Ship Ship { get; set; }
        bool Attack(string coordinates);
    }
}