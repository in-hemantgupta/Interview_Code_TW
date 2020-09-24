using System.Collections.Generic;
using Battleship.Models;

namespace Battleship.Services
{
    public interface IPlayerService
    {
        List<Player> Players { get; set; }
        void CreatePlayer(string Name, IEnumerable<IShip> ships, string[] targets);
        string Play();
        void PlayerTurn(Player player, int maxTargets, IEnumerable<IShip> targetShips);
    }
}