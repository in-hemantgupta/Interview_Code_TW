using System.Collections.Generic;
using Battleship.Models;

namespace Battleship.Services
{
    public interface IBattleshipService
    {
        Area BattleshipArea { get; set; }
        List<Player> Players { get; set; }
        string StartTheGame(string[] args);
    }
}