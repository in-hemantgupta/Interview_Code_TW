using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship.Models.Constraints;

namespace Battleship.Services.Contracts
{
    public interface IMissileFactory
    {
        IMissile CreateMissile(MissileType missileType);
    }
}
